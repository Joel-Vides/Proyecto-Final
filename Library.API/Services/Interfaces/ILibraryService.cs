using Library.API.Dtos.Books;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<ResponseDto<BooksActionResponseDto>> CreateAsync(BookCreateDto dto);
    }
}
