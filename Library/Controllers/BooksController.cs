using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;

namespace Library.Controllers
{
  public class BooksController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Book> allBooks = Book.GetAll();
      return View("Index", allBooks);
    }

    [HttpGet("/books/new")]
    public ActionResult CreateForm()
    {
      return View("CreateForm");
    }

    [HttpPost("/books/new")]
    public ActionResult CreateNewBook()
    {
      Book newBook = new Book(Request.Form["book-title"]);
      newBook.Save();
      return RedirectToAction("Index");
    }
  }
}
