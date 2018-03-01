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
    private bool _checkedOut;


    public Checkout(DateTime checkoutDate, DateTime dueDate, int Id = 0, int copyId = 0, int patronId = 0, bool checkedOut = true)
    {
      _checkoutDate = checkoutDate;
      _dueDate = dueDate;
      _id = Id;
      _copyId = copyId;
      _patronId = patronId;
      _checkedOut = checkedOut;
    }

    public DateTime GetCheckout() { return _checkoutDate;}

    public DateTime GetDueDate() { return _dueDate;}

    public int GetId() { return _id; }

    public int GetPatronId() { return _patronId; }

    public int GetCopyId() { return _copyId; }

    public int FindCopyId(int bookId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM copies WHERE book_id = @BookId AND checked_out = 0 LIMIT 1;";

      MySqlParameter bookIdParameter = new MySqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = bookId;
      cmd.Parameters.Add(bookIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int foundCopyId = 0;
      while(rdr.Read())
      {
        foundCopyId = rdr.GetInt32(0);
      }

      return foundCopyId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void SetCopyId(int copyId)
    {
      _copyId = copyId;
    }

    public void DoCheckout()
    {
      //TODO add functionality that updates copies table "checkedout" value
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `checkouts` (`patron_id`, `copy_id`, `date_checked`, `date_due`, `returned`) VALUES (@PatronId, @CopyId, @DateCheckout, @DateDue, @CheckedOut);";

      MySqlParameter patronId = new MySqlParameter();
      patronId.ParameterName = "@PatronId";
      patronId.Value = this._patronId;
      cmd.Parameters.Add(patronId);

      MySqlParameter copyId = new MySqlParameter();
      copyId.ParameterName = "@CopyId";
      copyId.Value = this._copyId;
      cmd.Parameters.Add(copyId);

      MySqlParameter dateCheckout = new MySqlParameter();
      dateCheckout.ParameterName = "@DateCheckout";
      dateCheckout.Value = this._checkoutDate;
      cmd.Parameters.Add(dateCheckout);

      MySqlParameter dateDue = new MySqlParameter();
      dateDue.ParameterName = "@DateDue";
      dateDue.Value = this._dueDate;
      cmd.Parameters.Add(dateDue);

      MySqlParameter checkedOut = new MySqlParameter();
      checkedOut.ParameterName = "@CheckedOut";
      checkedOut.Value = this._checkedOut;
      cmd.Parameters.Add(checkedOut);

      cmd.ExecuteNonQuery();

      var cmd2 = conn.CreateCommand() as MySqlCommand;
      cmd2.CommandText = @"UPDATE copies SET checked_out = 1 WHERE id = @CopyId;";
      cmd2.Parameters.Add(copyId);
      cmd2.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
