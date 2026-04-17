using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public class BookDto
{
    [Required(ErrorMessage = "Назва книги обов'язкова")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Назва має бути від 2 до 100 символів")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Автор обов'язковий")]
    public string Author { get; set; } = string.Empty;
}