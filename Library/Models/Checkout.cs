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

    public void DoCheckout()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `checkouts` (`patron_id`, `copy_id`, `date_checked`, `date_due`, `returned`) VALUES (@PatronId, @CopyId, @DateCheckout, @DateDue, @Returned);";

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

      MySqlParameter returned = new MySqlParameter();
      returned.ParameterName = "@Returned";
      returned.Value = this._returned;
      cmd.Parameters.Add(returned);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
