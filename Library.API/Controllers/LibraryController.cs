using Library.API.Dtos.Books;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/library")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BooksActionResponseDto>>> Post([FromBody] BookCreateDto dto)
        {
            var response = await _libraryService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<BooksActionResponseDto>>> Delete(Guid id)
        {
            var response = await _libraryService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<BooksActionResponseDto>>> Edit([FromBody] BookEditDto dto, Guid id)
        {
            var response = await _libraryService.EditAsync(id, dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("location/{bookId}")]
        public async Task<ActionResult<ResponseDto<string>>> GetBookLocation(Guid bookId)
        {
            var response = await _libraryService.GetBookLocationAsync(bookId);
            return StatusCode(response.StatusCode, response);
        }

    }
}
