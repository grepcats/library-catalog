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
      Checkout newCheckout = new Checkout("1/18/2017 12:00:00 AM")
    }
  }
}
