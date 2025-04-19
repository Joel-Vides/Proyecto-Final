using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
using Library.API.Dtos.BookshelfB;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class BookshelfBService : IBookshelfBService
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        public BookshelfBService(LibraryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<BookshelfBActionResponseDto>> CreateAsync(BookshelfBCreateDto dto)
        {

            var bookShelfEntity = _mapper.Map<BookshelfBEntity>(dto);

            var libraryEntity = await _context.Library.FirstOrDefaultAsync(b => b.Id == dto.BookId);

            if (libraryEntity is null)
            {
                return new ResponseDto<BookshelfBActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El Libro no Existe en la Base de Datos"
                };
            }

            _context.BookshelfB.Add(bookShelfEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BookshelfBActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
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
