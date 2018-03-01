using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library.Models;
using System;

namespace Library.Tests
{
  [TestClass]
  public class CheckoutTest : IDisposable
  {
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
      Patron.DeleteAll();
    }

    public CheckoutTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=library_test;";
    }

    [TestMethod]
    public void Getters_FetchElements_StringInt()
    {
      DateTime testCheckoutDate = DateTime.Parse("2009-01-01");
      DateTime testDueDate = testCheckoutDate.AddDays(14);
      int testId = 0;
      int testPatronId = 0;
      int testCopyId = 0;

      Checkout newCheckout = new Checkout(testCheckoutDate, testDueDate);

      Assert.AreEqual(testCheckoutDate, newCheckout.GetCheckout());
      Assert.AreEqual(testDueDate, newCheckout.GetDueDate());
      Assert.AreEqual(0, newCheckout.GetId());
      Assert.AreEqual(0, newCheckout.GetPatronId());
      Assert.AreEqual(0, newCheckout.GetCopyId());
    }
  }
}
