using AutoMapper;
using Library.Domain.Models;
using Library.Application.Interfaces;
using Library.Infrastructure.Data;
using Library.Application.DTOs;

namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BookService(AppDbContext context, IMapper mapper) 
    { 
        _context = context; 
        _mapper = mapper;
    }

    public IEnumerable<BookResponseDto> GetAllBooks()
    {
        var books = _context.Books.ToList();
        return _mapper.Map<IEnumerable<BookResponseDto>>(books);
    }

    public BookResponseDto? GetBookById(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return null;

        return _mapper.Map<BookResponseDto>(book);
    }

    public BookResponseDto AddBook(BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        _context.Books.Add(book);
        _context.SaveChanges();

        return _mapper.Map<BookResponseDto>(book);
    }

    public bool UpdateBook(int id, BookDto bookDto)
    {
        var book = _context.Books.Find(id);
        if (book == null) return false;

        _mapper.Map(bookDto, book);
        
        _context.SaveChanges();
        return true;
    }

    public bool DeleteBook(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        _context.SaveChanges();
        return true;
    }
}