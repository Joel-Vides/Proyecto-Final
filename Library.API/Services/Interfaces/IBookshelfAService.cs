using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface IBookshelfAService
    {
        Task<ResponseDto<BookshelfAActionResponseDto>> CreateAsync(BookshelfACreateDto dto);
        Task<ResponseDto<BookshelfAActionResponseDto>> DeleteAsync(Guid id);
    }
}
