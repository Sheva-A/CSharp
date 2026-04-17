using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.DTOs;

namespace LibraryApi.Service;

public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _context.Books.ToList();
    }

    public Book? GetBookById(int id)
    {
        return _context.Books.Find(id);
    }

    public Book AddBook(BookDto bookDto)
    {
        var newBook = new Book
        {
            Title = bookDto.Title,
            Author = bookDto.Author
        };

        _context.Books.Add(newBook);
        _context.SaveChanges();
        
        return newBook;
    }
}