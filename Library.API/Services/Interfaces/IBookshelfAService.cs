using Library.API.Database.Entities.Common;
using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.Common;

namespace Library.API.Services.Interfaces
{
    public interface IBookshelfAService
    {
        Task<ResponseDto<BookshelfAActionResponseDto>> CreateAsync(BookshelfACreateDto dto);
        Task<ResponseDto<BookshelfAActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PaginationDto<List<BookshelfADto>>>> GetListAsync(string searchTerm = "", int page = 1, int pageSize = 0);
    }
}
