using System;
using System.Linq;
using LibraryApp.Models;
using LibraryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp;

internal class Program
{
   private static void Main(string[] args)
   {
       Console.OutputEncoding = System.Text.Encoding.UTF8;

       using var db = new AppDbContext();

       db.Loans.RemoveRange(db.Loans);
       db.Books.RemoveRange(db.Books);
       db.Users.RemoveRange(db.Users);
       db.SaveChanges();

       Console.WriteLine("Створюємо дані\n");
       var user1 = new User { Name = "Max", Email = "max@gmail.com" };
       var user2 = new User { Name = "Mia", Email = "dan@gmail.com" };
       db.Users.AddRange(user1, user2);


       var book1 = new Book { Title = "A Man Called Ove", Author = "Fredrik Backman" };
       var book2 = new Book { Title = "The Kite Runner", Author = "Khaled Hosseini" };
       db.Books.AddRange(book1, book2);

       db.SaveChanges();
       Console.WriteLine("Зберігаємо дані\n");

       Console.WriteLine("Створюємо позики\n");
       var loan1 = new Loan { UserID = user1.Id, BookID = book1.Id, BorrowDate =  DateTime.Now };
       var loan2 = new Loan { UserID = user2.Id, BookID = book2.Id, BorrowDate = DateTime.Now };
       db.Loans.AddRange(loan1, loan2);

       db.SaveChanges();
       Console.WriteLine("Зберігаємо позики\n");

       Console.WriteLine("Всі позики з користувачами та книгами");
       var loansWithDetails = db.Loans
           .Include(l => l.User)
           .Include(l => l.Book)
           .ToList();
       
       foreach (var loan in loansWithDetails)
       {
           Console.WriteLine($"{loan.User.Name} взяв книгу '{loan.Book.Title}' {loan.BorrowDate:d}");
       }

       Console.WriteLine("\nОновлення пошти користувача");
       var userToUpdate = db.Users.FirstOrDefault(u => u.Name == "Max");
       if (userToUpdate != null)
       {
           userToUpdate.Email = "max1@gmail.com";
           db.SaveChanges();
           Console.WriteLine($"Користувач '{userToUpdate.Name}' оновлений: Email = {userToUpdate.Email}");
       }

       Console.WriteLine("\nВидалення користувача");
       var userToDelete = db.Users.FirstOrDefault(u => u.Name == "Mia");
       if (userToDelete != null)
       { 
           db.Users.Remove(userToDelete);
           db.SaveChanges();
           Console.WriteLine($"Користувача {userToDelete.Name} видалено.");
       }

       Console.WriteLine("\nПоточні користувачі");
       foreach (var user in db.Users.ToList())
       {
           Console.WriteLine($"- {user.Name}");
       }


       Console.WriteLine("\nПоточні книги");
       foreach (var book in db.Books.ToList())
       {
           Console.WriteLine($"- {book.Title} ({book.Author})");
       }


       Console.WriteLine("\nПоточні позики");
       foreach (var loan in db.Loans.Include(l => l.User).Include(l => l.Book))
       { 
           Console.WriteLine($"- {loan.User.Name} → {loan.Book.Title}");
       }


       Console.WriteLine("\nДемонстрація завершена");
   }
}