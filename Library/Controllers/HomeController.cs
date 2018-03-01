using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View("Index");
    }
  }
}
