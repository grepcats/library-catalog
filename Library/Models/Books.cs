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

    public void SetTitle(string newTitle) { _title = newTitle; }

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
      cmd.CommandText = @"SELECT * FROM `books`";

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

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM books WHERE id = @BookId;";

      MySqlParameter bookId = new MySqlParameter();
      bookId.ParameterName = "@BookId";
      bookId.Value = this._id;
      cmd.Parameters.Add(bookId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Book Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `books` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bookId = 0;
      string bookTitle = "";

      while (rdr.Read())
      {
        bookId = rdr.GetInt32(0);
        bookTitle = rdr.GetString(1);
      }

      Book foundBook = new Book(bookTitle, bookId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundBook;
    }

    public void Update(string title)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE books SET title = @NewTitle WHERE id = @BookId;";

      MySqlParameter newTitle = new MySqlParameter();
      newTitle.ParameterName = "@NewTitle";
      newTitle.Value = title;
      cmd.Parameters.Add(newTitle);

      MySqlParameter bookId = new MySqlParameter();
      bookId.ParameterName = "@BookId";
      bookId.Value = this._id;
      cmd.Parameters.Add(bookId);

      cmd.ExecuteNonQuery();
      _title = title;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }

  }
}
