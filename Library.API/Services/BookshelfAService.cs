using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
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

        public BookshelfAService(LibraryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<BookshelfAActionResponseDto>> CreateAsync(BookshelfACreateDto dto)
        {

            var bookShelfEntity = _mapper.Map<BookshelfAEntity>(dto);

            var libraryEntity = await _context.Library.FirstOrDefaultAsync(b => b.Id == dto.BookId);

            if (libraryEntity is null)
            {
                return new ResponseDto<BookshelfAActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Libro no Existe en la Base de Datos"
                };
            }

            _context.BookshelfA.Add(bookShelfEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfAActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
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
