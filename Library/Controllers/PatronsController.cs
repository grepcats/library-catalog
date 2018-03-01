using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Controllers
{
  public class PatronsController : Controller
  {
    [HttpGet("/patrons")]
    public ActionResult Index()
    {
      List<Patron> allPatrons = Patron.GetAll();
      return View("Index", allPatrons);
    }

    [HttpGet("/patrons/new")]
    public ActionResult CreatePatronForm()
    {
      return View("CreatePatronForm");
    }

    [HttpPost("/patrons/new")]
    public ActionResult CreateNewPatron()
    {
      Patron newPatron = new Patron(Request.Form["first-name"], Request.Form["last-name"], Request.Form["email"]);
      newPatron.Save();
      return RedirectToAction("Index", "patrons");
    }

    [HttpGet("/patrons/{id}/details")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string,object>();
      Patron foundPatron = Patron.Find(id);
      List<Checkout> books = foundPatron.GetCheckedOutBooks();
      model.Add("patron", foundPatron);
      model.Add("books", books);

      return View("Details", model);
    }
  }
}
