using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class BookTest : IDisposable
  {
    public void Dispose()
    {
      Book.DeleteAll();
    }

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

    [TestMethod]
    public void GetAll_DatebaseEmptyAtFirst_0()
    {
      //arrange, act
      int result = Book.GetAll().Count;

      //assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SaveBookToDatabase_BookList()
    {
      //arrange
      Book testBook = new Book("Consider Phlebas");

      //act
      testBook.Save();
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      //assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Save_AssignedIdToObject_Id()
    {
      //arrange
      Book testBook = new Book("Consider Phlebas");

      //act
      testBook.Save();
      Book savedBook = Book.GetAll()[0];
      int result = savedBook.GetId();
      int testId = testBook.GetId();

      //assert
      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAllSame_Book()
    {
      //arrange, act
      Book firstBook = new Book("Consider Phlebas");
      Book secondBook = new Book("Consider Phlebas");

      //assert
      Assert.AreEqual(firstBook, secondBook);
    }
  }
}
