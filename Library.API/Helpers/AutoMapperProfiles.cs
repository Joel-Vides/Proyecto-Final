using AutoMapper;
using Library.API.Database.Entities;
using Library.API.Dtos.Books;

namespace Library.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<LibraryEntity, BookDto>();
            CreateMap<LibraryEntity, BooksActionResponseDto>();
            CreateMap<BookCreateDto, LibraryEntity>();
        }
    }
}
