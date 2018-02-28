using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Author
  {
    private int _id;
    private string _firstName;
    private string _lastName;

    public Author (string firstName, string lastName, int Id = 0)
    {
      _firstName = firstName;
      _lastName = lastName;
      _id = Id;
    }

    public string GetFirstName() { return _firstName; }

    public string GetLastName() { return _lastName; }

    public int GetId() { return _id; }
  }
}
