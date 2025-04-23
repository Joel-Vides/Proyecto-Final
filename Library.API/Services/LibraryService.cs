using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
using Library.API.Dtos.Books;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        public LibraryService(LibraryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Para Crear Libros
        public async Task<ResponseDto<BooksActionResponseDto>> CreateAsync(BookCreateDto dto)
        {
            var bookEntity = _mapper.Map<LibraryEntity>(dto);

            _context.Library.Add(bookEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BooksActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = _mapper.Map<BooksActionResponseDto>(bookEntity)
            };
        }

        //Para Eliminar Libros
        public async Task<ResponseDto<BooksActionResponseDto>> DeleteAsync(Guid id)
        {
            var bookEntity = await _context.Library.FirstOrDefaultAsync(x => x.Id == id);

            if (bookEntity is null)
            {
                return new ResponseDto<BooksActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado!"
                };
            }

            _context.Library.Remove(bookEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BooksActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = _mapper.Map<BooksActionResponseDto>(bookEntity)
            };
        }

        //Para editar libros
        public async Task<ResponseDto<BooksActionResponseDto>> EditAsync(Guid id, BookEditDto dto)
        {
            var bookEntity = await _context.Library.FirstOrDefaultAsync(x => x.Id == id);
            if (bookEntity is null)
            {
                return new ResponseDto<BooksActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado!"
                };
            }

            bookEntity.BookName = dto.BookName;
            bookEntity.Author = dto.Author;
            bookEntity.Type = dto.Type;
            bookEntity.Volume = dto.Volume;
            bookEntity.Publisher = dto.Publisher;
            bookEntity.PublicationYear = dto.PublicationYear;

            await _context.SaveChangesAsync();

            return new ResponseDto<BooksActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Actualizado Correctamente",
                Data = _mapper.Map<BooksActionResponseDto>(bookEntity)
            };
        }

        //Funcion para buscar libros
        public async Task<ResponseDto<string>> GetBookLocationAsync(Guid bookId)
        {
            // Buscar el libro en la tabla principal
            var book = await _context.Library.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return new ResponseDto<string>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Libro no encontrado en la base de datos."
                };
            }

            // Verificar en qué estantería está
            string bookshelf = null;
            if (await _context.BookshelfA.AnyAsync(a => a.BookId == bookId))
                bookshelf = "A";
            else if (await _context.BookshelfB.AnyAsync(b => b.BookId == bookId))
                bookshelf = "B";
            else if (await _context.BookshelfC.AnyAsync(c => c.BookId == bookId))
                bookshelf = "C";

            if (bookshelf == null)
            {
                return new ResponseDto<string>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "El libro no está asignado a ninguna estantería."
                };
            }

            // Mensaje exacto solicitado
            return new ResponseDto<string>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = $"El libro '{book.BookName}' se encuentra en la estantería {bookshelf} y su ID es {bookId}.",
                Data = bookshelf // Opcional: enviar la estantería como dato adicional
            };
        }

        // Para bloquar libros
        public async Task<bool> AsignedBookAsync(Guid? IdBook)
        {
            return await _context.BookshelfA.AnyAsync(b => b.BookId == IdBook) ||
           await _context.BookshelfB.AnyAsync(b => b.BookId == IdBook) ||
           await _context.BookshelfC.AnyAsync(b => b.BookId == IdBook);
        }

        // Para Actualizar el Estado de los Libros
        public async Task UpdateBookStateAsync(Guid? IdBook, string newState)
        {
            var book = await _context.Library.FindAsync(IdBook);
            if (book != null)
            {
                book.Estado = newState;
                await _context.SaveChangesAsync();
            }
        }
    }
}
