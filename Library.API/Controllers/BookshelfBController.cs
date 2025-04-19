using Library.API.Dtos.BookshelfB;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/bookshelfb")]
    public class BookshelfBController : ControllerBase
    {
        private readonly IBookshelfBService _bookshelfBService;

        public BookshelfBController(IBookshelfBService bookshelfBService)
        {
            _bookshelfBService = bookshelfBService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BookshelfBActionResponseDto>>> Post([FromBody] BookshelfBCreateDto dto)
        {
            var response = await _bookshelfBService.CreateAsync(dto);

            return StatusCode(Response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<BookshelfBActionResponseDto>>> Delete(Guid id)
        {
            var response = await _bookshelfBService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
