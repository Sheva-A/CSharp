using LibraryApi.Models;
using LibraryApi.DTOs;

namespace LibraryApi.Service;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks();
    Book? GetBookById(int id);
    Book AddBook(BookDto bookDto);
}