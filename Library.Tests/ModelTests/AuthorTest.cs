using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class AuthorTest : IDisposable
  {
    public void Dispose()
    {
      Book.DeleteAll();
      // Author.DeleteAll();
    }

    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void Getters_FetchElements_StringInt()
    {
      //arrange
      Author newAuthor = new Author("Kayla", "Ondracek");
      string testFirstName = "Kayla";
      string testLastName = "Ondracek";
      int testId = 0;

      //act
      string resultFirstName = newAuthor.GetFirstName();
      string resultLastName = newAuthor.GetLastName();
      int resultId = newAuthor.GetId();

      //assert
      Assert.AreEqual(resultFirstName, testFirstName);
      Assert.AreEqual(resultLastName, testLastName);
      Assert.AreEqual(resultId, testId);
    }


  }
}
