using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View("Index", allAuthors);
    }
  }
}
