using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Checkout
  {
    private int _id;
    private int _copyId;
    private int _patronId;
    private DateTime _checkoutDate;
    private DateTime _dueDate;
    private bool _returned;


    public Checkout(DateTime checkoutDate, DateTime dueDate, int Id = 0, int copyId = 0, int patronId = 0, bool returned = false)
    {
      _checkoutDate = checkoutDate;
      _dueDate = dueDate;
      _id = Id;
      _copyId = copyId;
      _patronId = patronId;
      _returned = returned;
    }

    public DateTime GetCheckout() { return _checkoutDate;}

    public DateTime GetDueDate() { return _dueDate;}

    public int GetId() { return _id; }

    public int GetPatronId() { return _patronId; }

    public int GetCopyId() { return _copyId; }
    }
}
