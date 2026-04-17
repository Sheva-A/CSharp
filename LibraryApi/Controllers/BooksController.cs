using Library.Application.DTOs;
using Library.Application.Interfaces; 
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService; 
    public BooksController(IBookService bookService) { _bookService = bookService; }

    [HttpGet]
    public ActionResult<IEnumerable<BookResponseDto>> GetAll() => Ok(_bookService.GetAllBooks());

    [HttpGet("{id}")]
    public ActionResult<BookResponseDto> GetById(int id)
    {
        var book = _bookService.GetBookById(id);
        return book == null ? NotFound(new { message = "Книгу не знайдено" }) : Ok(book);
    }

    [HttpPost]
    public ActionResult<BookResponseDto> Create([FromBody] BookDto bookDto)
    {
        var result = _bookService.AddBook(bookDto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BookDto bookDto)
    {
        var success = _bookService.UpdateBook(id, bookDto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _bookService.DeleteBook(id);
        return success ? NoContent() : NotFound();
    }
}