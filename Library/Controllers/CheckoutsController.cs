using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Controllers
{
  public class CheckoutsController : Controller
  {
    [HttpGet("/checkouts/new")]
    public ActionResult Index()
    {
      List<Patron> allPatrons = Patron.GetAll();
      List<Book> allBooks = Book.GetAll();
      Dictionary<string, object> model = new Dictionary<string,object>();
      model.Add("patrons", allPatrons);
      model.Add("books", allBooks);
      return View("Index", model);
    }

    [HttpPost("/checkouts/new")]
    public ActionResult CreateNewCheckout()
    {
      DateTime checkoutDate = DateTime.Today;
      DateTime dueDate = checkoutDate.AddDays(14);
      int patronId = Int32.Parse(Request.Form["patron"]);
      int copyId = Int32.Parse(Request.Form["book"]);
      Checkout newCheckout = new Checkout(checkoutDate, dueDate, 0, copyId, patronId, false);
      newCheckout.DoCheckout();

      return RedirectToAction("Index");
    }
  }
}
