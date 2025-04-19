using Library.API.Dtos.BookshelfB;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface IBookshelfBService
    {
        Task<ResponseDto<BookshelfBActionResponseDto>> CreateAsync(BookshelfBCreateDto dto);
        Task<ResponseDto<BookshelfBActionResponseDto>> DeleteAsync(Guid id);
    }
}
