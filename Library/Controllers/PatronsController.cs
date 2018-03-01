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
  }
}
