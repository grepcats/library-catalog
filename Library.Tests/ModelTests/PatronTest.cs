using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class PatronTest : IDisposable
  {
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
      Patron.DeleteAll();
    }

    public PatronTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void Getters_FetchElements_StringInt()
    {
      Patron newPatron = new Patron("Kayla", "Ondracek", "test@test.com");
      string testFirstName = "Kayla";
      string testLastName = "Ondracek";
      string testEmail = "test@test.com";
      int testId = 0;
      int testFine = 0;

      string resultFirstName = newPatron.GetFirstName();
      string resultLastName = newPatron.GetLastName();
      string resultEmail = newPatron.GetEmail();
      int resultId = newPatron.GetId();
      int resultFine = newPatron.GetFines();

      Assert.AreEqual(resultFirstName, testFirstName);
      Assert.AreEqual(resultLastName, testLastName);
      Assert.AreEqual(resultEmail, testEmail);
      Assert.AreEqual(resultId, testId);
      Assert.AreEqual(resultFine, testFine);
    }

    [TestMethod]
    public void GetAll_DatebaseEmptyAtFirst_0()
    {
      int result = Patron.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfAllSame_Patron()
    {
      Patron firstPatron = new Patron("Kayla", "Ondracek", "test@test.com");
      Patron secondPatron = new Patron("Kayla", "Ondracek", "test@test.com");

      Assert.AreEqual(firstPatron, secondPatron);
    }

    [TestMethod]
    public void Save_SavePatronToDatabase_PatronList()
    {
      Patron testPatron = new Patron("Kayla", "Ondracek", "test@test.com");

      testPatron.Save();
      List<Patron> result = Patron.GetAll();
      List<Patron> testList = new List<Patron>{testPatron};

      CollectionAssert.AreEqual(result, testList);

    }
  }
}
