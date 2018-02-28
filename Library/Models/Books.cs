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

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `books` (`title`) VALUES (@BookTitle);";

      MySqlParameter bookTitle = new MySqlParameter();
      bookTitle.ParameterName = "@BookTitle";
      bookTitle.Value = this._title;
      cmd.Parameters.Add(bookTitle);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Book> GetAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM books";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Book> allBooks = new List<Book>{};
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookTitle = rdr.GetString(1);
        Book newBook = new Book(bookTitle, bookId);
        allBooks.Add(newBook);
      }

      return allBooks;
    }

    public override bool Equals(System.Object otherBook)
    {
      if(!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool bookIdEquality = (this.GetId() == newBook.GetId());
        bool bookTitleEquality = (this.GetTitle() == newBook.GetTitle());
        return (bookIdEquality && bookTitleEquality);
      }
    }

    public override int GetHashCode()
     {
          return this.GetTitle().GetHashCode();
     }

  }
}
