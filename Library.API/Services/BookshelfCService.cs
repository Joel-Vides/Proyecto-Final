using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
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

        public BookshelfCService(LibraryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
