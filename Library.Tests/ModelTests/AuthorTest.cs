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
      Author.DeleteAll();
      Patron.DeleteAll();
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


    [TestMethod]
    public void GetAll_DatebaseEmptyAtFirst_0()
    {
      //arrange, act
      int result = Author.GetAll().Count;

      //assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAllSame_Book()
    {
      //arrange, act
      Author firstAuthor = new Author("Kayla", "Ondracek");
      Author secondAuthor = new Author("Kayla", "Ondracek");

      //assert
      Assert.AreEqual(firstAuthor, secondAuthor);
    }

    [TestMethod]
    public void Save_SaveAuthorToDatabase_AuthorList()
    {
      //arrange
      Author testAuthor = new Author("Kayla", "Ondracek");

      //act
      testAuthor.Save();
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      //assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Save_AssignedIdToObject_Id()
    {
      //arrange
      Author testAuthor = new Author("Kayla", "Ondracek");

      //act
      testAuthor.Save();
      Author savedAuthor = Author.GetAll()[0];
      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      //assert
      Assert.AreEqual(result, testId);
    }

    // [TestMethod]
    // public void CheckDuplicateAuthor_CheckDuplicateAuthorInDB_True()
    // {
    //   //arrange
    //   List<Author> allAuthors = Author.GetAll();
    //   Author newAuthor = new Author("Kayla", "Ondracek");
    //   newAuthor.Save();
    //
    //   Author newAuthor2 = new Author("Kayla", "Ondracek");
    //
    //   //act
    //   bool isAuthorDuplicate = Author.CheckDuplicate(newAuthor2);
    //
    //   //assert
    //   Assert.AreEqual(isAuthorDuplicate, true);
    // }


  }
}
