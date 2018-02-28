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
  }
}
