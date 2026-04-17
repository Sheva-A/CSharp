using AutoMapper;
using Library.Domain.Models; 
using Library.Application.DTOs;      

namespace Library.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookResponseDto>();
        CreateMap<BookDto, Book>();
    }
}