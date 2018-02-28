using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class BookTest
  {
    public BookTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void GetTitle_FetchTitle_String()
    {
      //arrange
      Book testBook = new Book("Consider Phlebas");
      string testTitle = "Consider Phlebas";
      int testId = 0;

      //act
      string resultTitle = testBook.GetTitle();
      int resultId = testBook.GetId();

      //assert
      Assert.AreEqual(testTitle, resultTitle);
      Assert.AreEqual(testId, resultId);
    }
  }
}
