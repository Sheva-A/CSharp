using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.DTOs;

namespace LibraryApi.Service;

public class BookService : IBookService
{
    private readonly AppDbContext _context;
    public BookService(AppDbContext context) { _context = context; }

    public IEnumerable<BookResponseDto> GetAllBooks()
    {
        return _context.Books.Select(b => new BookResponseDto 
        { 
            Id = b.Id, Title = b.Title, Author = b.Author 
        }).ToList();
    }

    public BookResponseDto? GetBookById(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return null;

        return new BookResponseDto { Id = book.Id, Title = book.Title, Author = book.Author };
    }

    public BookResponseDto AddBook(BookDto bookDto)
    {
        var book = new Book { Title = bookDto.Title, Author = bookDto.Author };
        _context.Books.Add(book);
        _context.SaveChanges();

        return new BookResponseDto { Id = book.Id, Title = book.Title, Author = book.Author };
    }

    public bool UpdateBook(int id, BookDto bookDto)
    {
        var book = _context.Books.Find(id);
        if (book == null) return false;

        book.Title = bookDto.Title;
        book.Author = bookDto.Author;
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