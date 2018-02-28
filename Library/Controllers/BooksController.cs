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

    [HttpGet("/books/{id}/delete")]
    public ActionResult DeleteBook(int id)
    {
      Book foundBook = Book.Find(id);
      foundBook.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/books/{id}/update")]
    public ActionResult CreateUpdateForm(int id)
    {
      Book foundBook = Book.Find(id);
      return View("CreateUpdateForm", foundBook);
    }

    [HttpPost("/books/{id}/update")]
    public ActionResult UpdateBook(int id)
    {
      Book foundBook = Book.Find(id);
      foundBook.Update(Request.Form["book-title"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/books/{id}/details")]
    public ActionResult Details(int id)
    {
      Book foundBook = Book.Find(id);
      List<Author> authors = foundBook.GetAuthors();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("book", foundBook);
      model.Add("authors", authors);

      return View("Details", model);
    }

    [HttpGet("/books/{id}/authors/new")]
    public ActionResult CreateAuthorForm(int id)
    {
      Book foundBook = Book.Find(id);
      return View("CreateAuthorForm", foundBook);
    }

    [HttpPost("/books/{id}/authors/new")]
    public ActionResult CreateAuthor(int id)
    {
      Book foundBook = Book.Find(id);
      Author newAuthor = new Author(Request.Form["author-first"], Request.Form["author-last"]);
      Author.CheckDuplicate(newAuthor);
      foundBook.AddAuthor(newAuthor);

      return RedirectToAction("Details", new {Id = id});

    }
  }
}
