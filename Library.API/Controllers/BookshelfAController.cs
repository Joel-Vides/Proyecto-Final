﻿using Library.API.Dtos.BookshelfA;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/bookshelfa")]
    public class BookshelfAController : ControllerBase
    {
        private readonly IBookshelfAService _bookshelfAService;

        public BookshelfAController(IBookshelfAService bookshelfAService)
        {
            _bookshelfAService = bookshelfAService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BookshelfAActionResponseDto>>> Post([FromBody] BookshelfACreateDto dto)
        {
            var response = await _bookshelfAService.CreateAsync(dto);

            return StatusCode(Response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<BookshelfAActionResponseDto>>> Delete(Guid id)
        {
            var response = await _bookshelfAService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
