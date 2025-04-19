using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
using Library.API.Database.Entities.Common;
using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.BookshelfC;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class BookshelfCService : IBookshelfCService
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        // Para la Pagination
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public BookshelfCService(LibraryDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;

            // Para La Pagination
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<BookshelfCDto>>>> GetListAsync(
            int page = 1, int pageSize = 0)
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<BookshelfCEntity> bookshelfCQuery = _context.BookshelfC;

            int totalRows = await bookshelfCQuery.CountAsync();

            var bookshelfCEntity = await bookshelfCQuery
                .OrderBy(x => x.BooksName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var bookshelfCDto = _mapper.Map<List<BookshelfCDto>>(bookshelfCEntity);

            return new ResponseDto<PaginationDto<List<BookshelfCDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = bookshelfCEntity.Count() > 0
                    ? "Registros Encontrados"
                    : "No se Encontraron Registros",
                Data = new PaginationDto<List<BookshelfCDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = bookshelfCDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                        .Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };

        }

        public async Task<ResponseDto<BookshelfCActionResponseDto>> CreateAsync(BookshelfCCreateDto dto)
        {
            // Verificar la cantidad actual de libros en la estantería A
            var booksCount = await _context.BookshelfC.CountAsync();
            if (booksCount >= 50)
            {
                return new ResponseDto<BookshelfCActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "La estantería A está llena (Máximo 50 registros)."
                };
            }

            // Mapear el DTO a la entidad correspondiente
            var bookShelfEntity = _mapper.Map<BookshelfCEntity>(dto);

            // Verificar si el libro existe en la base de datos de la biblioteca
            var libraryEntity = await _context.Library.FirstOrDefaultAsync(b => b.Id == dto.BookId);

            if (libraryEntity is null)
            {
                return new ResponseDto<BookshelfCActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Libro no Existe en la Base de Datos."
                };
            }

            // Agregar el libro a la estantería A
            _context.BookshelfC.Add(bookShelfEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfCActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente.",
                Data = _mapper.Map<BookshelfCActionResponseDto>(bookShelfEntity)
            };
        }

        public async Task<ResponseDto<BookshelfCActionResponseDto>> DeleteAsync(Guid id)
        {
            var bookshelfCEntity = await _context.BookshelfC.FirstOrDefaultAsync(x => x.Id == id);

            if (bookshelfCEntity is null)
            {
                return new ResponseDto<BookshelfCActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado"
                };
            }

            _context.BookshelfC.Remove(bookshelfCEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfCActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = _mapper.Map<BookshelfCActionResponseDto>(bookshelfCEntity)
            };
        }
    }
}
