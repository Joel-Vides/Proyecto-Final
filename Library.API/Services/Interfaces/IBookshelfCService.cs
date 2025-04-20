using Library.API.Database.Entities.Common;
using Library.API.Dtos.BookshelfC;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface IBookshelfCService
    {
        Task<ResponseDto<BookshelfCActionResponseDto>> CreateAsync(BookshelfCCreateDto dto);
        Task<ResponseDto<BookshelfCActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<BookshelfCDto>>>> GetListAsync(int page = 1, int pageSize = 0);
    }
}
