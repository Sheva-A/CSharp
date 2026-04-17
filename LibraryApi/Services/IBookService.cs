using LibraryApi.DTOs;

namespace LibraryApi.Service;

public interface IBookService
{
    IEnumerable<BookResponseDto> GetAllBooks();
    BookResponseDto? GetBookById(int id);
    BookResponseDto AddBook(BookDto bookDto);
    bool UpdateBook(int id, BookDto bookDto);
    bool DeleteBook(int id);
}

