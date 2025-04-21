using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
using Library.API.Database.Entities.Common;
using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.BookshelfB;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.API.Services
{
    public class BookshelfBService : IBookshelfBService
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        // Para la Pagination
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public BookshelfBService(LibraryDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;

            // Para La Pagination
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<BookshelfBDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0)
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<BookshelfBEntity> bookshelfBQuery = _context.BookshelfB;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookshelfBQuery = bookshelfBQuery
                    .Where(x => (x.BooksName)
                    .Contains(searchTerm));
            }

            int totalRows = await bookshelfBQuery.CountAsync();

            var bookshelfBEntity = await bookshelfBQuery
                .OrderBy(x => x.BooksName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var bookshelfBDto = _mapper.Map<List<BookshelfBDto>>(bookshelfBEntity);

            return new ResponseDto<PaginationDto<List<BookshelfBDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = bookshelfBEntity.Count() > 0
                    ? "Registros Encontrados"
                    : "No se Encontraron Registros",
                Data = new PaginationDto<List<BookshelfBDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = bookshelfBDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                        .Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };

        }

        public async Task<ResponseDto<BookshelfBActionResponseDto>> CreateAsync(BookshelfBCreateDto dto)
        {
            // Verificar la cantidad actual de libros en la estantería A
            var booksCount = await _context.BookshelfB.CountAsync();
            if (booksCount >= 50)
            {
                return new ResponseDto<BookshelfBActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "La estantería A está llena (Máximo 50 registros)."
                };
            }

            // Mapear el DTO a la entidad correspondiente
            var bookShelfEntity = _mapper.Map<BookshelfBEntity>(dto);

            // Verificar si el libro existe en la base de datos de la biblioteca
            var libraryEntity = await _context.Library.FirstOrDefaultAsync(b => b.Id == dto.BookId);

            if (libraryEntity is null)
            {
                return new ResponseDto<BookshelfBActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Libro no Existe en la Base de Datos."
                };
            }

            // Agregar el libro a la estantería A
            _context.BookshelfB.Add(bookShelfEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfBActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente.",
                Data = _mapper.Map<BookshelfBActionResponseDto>(bookShelfEntity)
            };
        }

        public async Task<ResponseDto<BookshelfBActionResponseDto>> DeleteAsync(Guid id)
        {
            var bookshelfBEntity = await _context.BookshelfB.FirstOrDefaultAsync(x => x.Id == id);

            if (bookshelfBEntity is null)
            {
                return new ResponseDto<BookshelfBActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };
            }

            _context.BookshelfB.Remove(bookshelfBEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfBActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = _mapper.Map<BookshelfBActionResponseDto>(bookshelfBEntity)
            };
        }
    }
}
