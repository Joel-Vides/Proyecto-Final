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
