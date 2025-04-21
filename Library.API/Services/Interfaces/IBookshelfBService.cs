using Library.API.Database.Entities.Common;
using Library.API.Dtos.BookshelfB;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface IBookshelfBService
    {
        Task<ResponseDto<BookshelfBActionResponseDto>> CreateAsync(BookshelfBCreateDto dto);
        Task<ResponseDto<BookshelfBActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<BookshelfBDto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0);
    }
}
