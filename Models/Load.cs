namespace LibraryApp.Models;

public class Loan
{
    public int Id { get; set; }
  
    public int UserID { get; set; }
    public User User { get; set; }
  
    public int BookID { get; set; }
    public Book Book { get; set; }
  
    public DateTime BorrowDate { get; set; }
}