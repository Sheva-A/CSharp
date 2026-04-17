using Library.Application.DTOs;

namespace Library.Application.Interfaces;

public interface IBookService
{
    IEnumerable<BookResponseDto> GetAllBooks();
    BookResponseDto? GetBookById(int id);
    BookResponseDto AddBook(BookDto bookDto);
    bool UpdateBook(int id, BookDto bookDto);
    bool DeleteBook(int id);
}

