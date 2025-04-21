using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
using Library.API.Database.Entities.Common;
using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class BookshelfAService : IBookshelfAService
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        // Para la Pagination
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public BookshelfAService(LibraryDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;

            // Para La Pagination
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }
        public async Task<ResponseDto<PaginationDto<List<BookshelfADto>>>> GetListAsync(
            string searchTerm = "" ,int page = 1, int pageSize = 0)
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<BookshelfAEntity> bookshelfAQuery = _context.BookshelfA;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookshelfAQuery = bookshelfAQuery
                    .Where(x => (x.BooksName)
                    .Contains(searchTerm));
            }

            int totalRows = await bookshelfAQuery.CountAsync();

            var bookshelfAEntity = await bookshelfAQuery
                .OrderBy(x => x.BooksName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var bookshelfADto = _mapper.Map<List<BookshelfADto>>(bookshelfAEntity);

            return new ResponseDto<PaginationDto<List<BookshelfADto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = bookshelfAEntity.Count() > 0
                    ? "Registros Encontrados"
                    : "No se Encontraron Registros",
                Data = new PaginationDto<List<BookshelfADto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = bookshelfADto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                        .Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };

        } 

        public async Task<ResponseDto<BookshelfAActionResponseDto>> CreateAsync(BookshelfACreateDto dto)
        {
            // Verificar la cantidad actual de libros en la estantería A
            var booksCount = await _context.BookshelfA.CountAsync();
            if (booksCount >= 50)
            {
                return new ResponseDto<BookshelfAActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "La estantería A está llena (Máximo 50 registros)."
                };
            }

            // Mapear el DTO a la entidad correspondiente
            var bookShelfEntity = _mapper.Map<BookshelfAEntity>(dto);

            // Verificar si el libro existe en la base de datos de la biblioteca
            var libraryEntity = await _context.Library.FirstOrDefaultAsync(b => b.Id == dto.BookId);

            if (libraryEntity is null)
            {
                return new ResponseDto<BookshelfAActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Libro no Existe en la Base de Datos."
                };
            }

            // Agregar el libro a la estantería A
            _context.BookshelfA.Add(bookShelfEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfAActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente.",
                Data = _mapper.Map<BookshelfAActionResponseDto>(bookShelfEntity)
            };
        }


        public async Task<ResponseDto<BookshelfAActionResponseDto>> DeleteAsync(Guid id)
        {
            var bookshelfAEntity = await _context.BookshelfA.FirstOrDefaultAsync(x => x.Id == id);

            if (bookshelfAEntity is null)
            {
                return new ResponseDto<BookshelfAActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };
            }

            _context.BookshelfA.Remove(bookshelfAEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfAActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = _mapper.Map<BookshelfAActionResponseDto>(bookshelfAEntity)
            };
        }
    }
}
