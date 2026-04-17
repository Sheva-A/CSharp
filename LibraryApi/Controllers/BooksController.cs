using LibraryApi.Models;
using LibraryApi.DTOs; 
using LibraryApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll() 
    {
        return Ok(_bookService.GetAllBooks());
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null) return NotFound(new { message = $"Книгу з ID {id} не знайдено." });
        
        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> Create([FromBody] BookDto bookDto)
    {
        if (string.IsNullOrWhiteSpace(bookDto.Title)) 
            return BadRequest(new { error = "Назва книги (Title) є обов'язковою." });

        var createdBook = _bookService.AddBook(bookDto);
        
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }
}