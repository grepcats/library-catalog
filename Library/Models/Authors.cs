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

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM authors;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Author> GetAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `authors`";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Author> allAuthors = new List<Author>{};
      while(rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        string authorFirstName = rdr.GetString(1);
        string authorLastName = rdr.GetString(2);
        Author newAuthor = new Author(authorFirstName, authorLastName, authorId);
        allAuthors.Add(newAuthor);
      }

      return allAuthors;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `authors` (`first_name`, `last_name`) VALUES (@FirstName, @LastName);";

      MySqlParameter firstName = new MySqlParameter();
      firstName.ParameterName = "@FirstName";
      firstName.Value = this._firstName;
      cmd.Parameters.Add(firstName);

      MySqlParameter lastName = new MySqlParameter();
      lastName.ParameterName = "@LastName";
      lastName.Value = this._lastName;
      cmd.Parameters.Add(lastName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherAuthor)
    {
      if(!(otherAuthor is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthor;
        bool authorIdEquality = (this.GetId() == newAuthor.GetId());
        bool authorFirstNameEquality = (this.GetFirstName() == newAuthor.GetFirstName());
        bool authorLastNameEquality = (this.GetLastName() == newAuthor.GetLastName());
        return (authorIdEquality && authorFirstNameEquality && authorLastNameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetFirstName().GetHashCode();
    }

    public static bool CheckDuplicate(Author checkAuthor)
    {
      List<Author> allAuthors = Author.GetAll();
      foreach (Author author in allAuthors)
      {
        if (author.GetFirstName() == checkAuthor.GetFirstName() && author.GetLastName() == checkAuthor.GetLastName())
        {
          return true;
        }
      }
      return false;
    }

  }
}
