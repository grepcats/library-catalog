using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Patron
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _email;
    private int _fines;

    public Patron (string firstName, string lastName, string email, int fines = 0, int id = 0)
    {
      _firstName = firstName;
      _lastName = lastName;
      _fines = fines;
      _id = id;
      _email = email;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetFirstName()
    {
      return _firstName;
    }

    public string GetLastName()
    {
      return _lastName;
    }

    public int GetFines()
    {
      return _fines;
    }

    public string GetEmail()
    {
      return _email;
    }

    public override int GetHashCode()
    {
      return this.GetFirstName().GetHashCode();
    }

    public static List<Patron> GetAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `patrons`";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Patron> allPatrons = new List<Patron>{};
      while(rdr.Read())
      {
        int patronId = rdr.GetInt32(0);
        string patronFirstName = rdr.GetString(1);
        string patronLastName = rdr.GetString(2);
        string patronEmail = rdr.GetString(3);
        int patronFines = rdr.GetInt32(4);
        Patron newPatron = new Patron(patronFirstName, patronLastName, patronEmail, patronFines, patronId);
        allPatrons.Add(newPatron);
      }
      return allPatrons;
    }

    public override bool Equals(System.Object otherPatron)
    {
      if(!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool patronIdEquality = (this.GetId() == newPatron.GetId());
        bool patronFirstNameEquality = (this.GetFirstName() == newPatron.GetFirstName());
        bool patronLastNameEquality = (this.GetLastName() == newPatron.GetLastName());
        bool patronEmailEquality = (this.GetEmail() == newPatron.GetEmail());
        bool patronFinesEquality = (this.GetFines() == newPatron.GetFines());
        return (patronIdEquality && patronFirstNameEquality && patronLastNameEquality && patronEmailEquality && patronFinesEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `patrons` (`first_name`, `last_name`, `email`) VALUES (@FirstName, @LastName, @email);";

      MySqlParameter firstName = new MySqlParameter();
      firstName.ParameterName = "@FirstName";
      firstName.Value = this._firstName;
      cmd.Parameters.Add(firstName);

      MySqlParameter lastName = new MySqlParameter();
      lastName.ParameterName = "@LastName";
      lastName.Value = this._lastName;
      cmd.Parameters.Add(lastName);

      MySqlParameter email = new MySqlParameter();
      email.ParameterName = "@email";
      email.Value = this._email;
      cmd.Parameters.Add(email);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM patrons";

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Patron Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `patron` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int patronId = 0;
      string patronFirstName = "";
      string patronLastName = "";
      string patronEmail = "";
      int patronFines = 0;

      while (rdr.Read())
      {
        patronId = rdr.GetInt32(0);
        patronFirstName = rdr.GetString(1);
        patronLastName = rdr.GetString(2);
        patronEmail = rdr.GetString(3);
        patronFines = rdr.GetInt32(4);
      }

      Patron foundPatron = new Patron(patronFirstName, patronLastName, patronEmail, patronFines, patronId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundPatron;
    }
  }
}
