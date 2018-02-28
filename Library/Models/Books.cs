using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Book
  {
    private int _id;
    private string _title;

    public Book (string title, int Id = 0)
    {
      _title = title;
      _id = Id;
    }

    public string GetTitle() { return _title; }

    public int GetId() { return _id; }

  }
}
