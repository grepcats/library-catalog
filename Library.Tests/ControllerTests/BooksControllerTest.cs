using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;
using Library.Controllers;

namespace Library.Tests
{
  [TestClass]
  public class BooksControllerTest
  {
    [TestMethod]
    public void Index_ReturnIfTrue_View()
    {
      //arrange
      BooksController controller = new BooksController();

      //act
      IActionResult indexView = controller.Index();
      ViewResult result = indexView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Index_HasCorrectModelType_True()
    {
      //arrange
      ViewResult indexView = new BooksController().Index() as ViewResult;

      //act
      var result = indexView.ViewData.Model;

      //assert
      Assert.IsInstanceOfType(result, typeof(List<Book>));
    }

    [TestMethod]
    public void CreateForm_ReturnIfTrue_View()
    {
      //arrange
      BooksController controller = new BooksController();

      //act
      IActionResult createFormView = controller.Index();
      ViewResult result = createFormView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void CreateUpdateForm_ReturnIfTrue_View()
    {
      //arrange
      BooksController controller = new BooksController();

      //act
      IActionResult createUpdateFormView = controller.Index();
      ViewResult result = createUpdateFormView as ViewResult;

      //assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void CreateUpdateForm_HasCorrectModelType_True()
    {
      //arrange
      ViewResult createUpdateFormView = new BooksController().CreateUpdateForm(1) as ViewResult;

      //act
      var result = createUpdateFormView.ViewData.Model;

      //assert
      Assert.IsInstanceOfType(result, typeof(Book));
    }
  }
}
