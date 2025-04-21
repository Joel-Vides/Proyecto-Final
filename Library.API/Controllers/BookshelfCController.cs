using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.BookshelfC;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/bookshelfc")]
    public class BookshelfCController : ControllerBase
    {
        private readonly IBookshelfCService _bookshelfCService;

        public BookshelfCController(IBookshelfCService bookshelfCService)
        {
            _bookshelfCService = bookshelfCService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<BookshelfCCreateDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0)
        {
            var response = await _bookshelfCService.GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BookshelfCActionResponseDto>>> Post([FromBody] BookshelfCCreateDto dto)
        {
            var response = await _bookshelfCService.CreateAsync(dto);

            return StatusCode(Response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<BookshelfCActionResponseDto>>> Delete(Guid id)
        {
            var response = await _bookshelfCService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
