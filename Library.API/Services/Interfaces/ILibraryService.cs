using Library.API.Dtos.Books;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<ResponseDto<BooksActionResponseDto>> CreateAsync(BookCreateDto dto);
        Task<ResponseDto<BooksActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<BooksActionResponseDto>> EditAsync(Guid id, BookEditDto dto);
        Task<ResponseDto<string>> GetBookLocationAsync(Guid bookId);
    }
}
