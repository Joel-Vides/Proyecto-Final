using Library.API.Dtos.BookshelfA;
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

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<BookshelfBCreateDto>>>> GetList(
            int page = 1, int pageSize = 0)
        {
            var response = await _bookshelfBService.GetListAsync(page, pageSize);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
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
