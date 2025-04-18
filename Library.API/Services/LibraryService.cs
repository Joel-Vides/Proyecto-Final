﻿using AutoMapper;
using Library.API.Constants;
using Library.API.Database;
using Library.API.Database.Entities;
using Library.API.Dtos.Books;
using Library.API.Dtos.Common;
using Library.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        public LibraryService(LibraryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Para Crear Libros
        public async Task<ResponseDto<BooksActionResponseDto>> CreateAsync(BookCreateDto dto)
        {
            var bookEntity = _mapper.Map<LibraryEntity>(dto);

            _context.Library.Add(bookEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BooksActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = _mapper.Map<BooksActionResponseDto>(bookEntity)
            };
        }

        //Para Eliminar Libros
        public async Task<ResponseDto<BooksActionResponseDto>> DeleteAsync(Guid id)
        {
            var bookEntity = await _context.Library.FirstOrDefaultAsync(x => x.Id == id);

            if (bookEntity is null)
            {
                return new ResponseDto<BooksActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no Encontrado!"
                };
            }

            _context.Library.Remove(bookEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<BooksActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado Correctamente",
                Data = _mapper.Map<BooksActionResponseDto>(bookEntity)
            };
        }
    }
}
