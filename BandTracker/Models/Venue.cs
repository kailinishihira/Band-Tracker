using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _name;
    private string _city;

    public Venue(string name, string city, int id = 0)
    {
      _name = name;
      _city = city;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetCity()
    {
      return _city;
    }

    public override bool Equals(Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        bool cityEquality = (this.GetCity() == newVenue.GetCity());
        return (idEquality && nameEquality && cityEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Venue> GetAll()
    {
     List<Venue> venueList = new List<Venue> {};

     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM venues ORDER BY name ASC;";

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int venueId = rdr.GetInt32(0);
       string name = rdr.GetString(1);
       string city = rdr.GetString(2);
       Venue newVenue = new Venue(name, city, venueId);
       venueList.Add(newVenue);
     }
     conn.Close();
     return venueList;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues  (name, city) VALUES (@name, @city);";
      MySqlParameter nameParameter = new MySqlParameter();
      nameParameter.ParameterName = "@name";
      nameParameter.Value = this._name;
      cmd.Parameters.Add(nameParameter);

      MySqlParameter cityParameter = new MySqlParameter();
      cityParameter.ParameterName = "@city";
      cityParameter.Value = this._city;
      cmd.Parameters.Add(cityParameter);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Venue Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @venueId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@venueId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int venueId = 0;
      string venueName = "";
      string venueCity = "";

      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
        venueCity = rdr.GetString(2);
      }
      Venue foundVenue = new Venue(venueName, venueCity, venueId);
      conn.Close();
      return foundVenue;
    }

    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET name = @newName WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      conn.Close();
      _name = newName;
    }

    public void UpdateCity(string newCity)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET city = @newCity WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter city = new MySqlParameter();
      city.ParameterName = "@newCity";
      city.Value = newCity;
      cmd.Parameters.Add(city);

      cmd.ExecuteNonQuery();
      conn.Close();
      _city = newCity;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void AddBandToVenueJoinTable(Band newBand)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);";

      MySqlParameter bandId = new MySqlParameter();
      bandId.ParameterName = "@BandId";
      bandId.Value = newBand.GetId();
      cmd.Parameters.Add(bandId);

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@VenueId";
      venueId.Value = _id;
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Band> GetBands()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT bands.*
       FROM venues
       JOIN bands_venues ON(venues.id = bands_venues.venue_id)
       JOIN bands ON(bands.id = bands_venues.band_id)
       WHERE venue_id = @venueId
       ORDER BY bands.name ASC;";

     MySqlParameter venueIdParameter = new MySqlParameter();
     venueIdParameter.ParameterName = "@venueId";
     venueIdParameter.Value = _id;
     cmd.Parameters.Add(venueIdParameter);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     List<Band> allBands = new List<Band> {};
       while(rdr.Read())
       {
         int bandId = rdr.GetInt32(0);
         string bandName = rdr.GetString(1);
         Band foundBand = new Band(bandName, bandId);
         allBands.Add(foundBand);
       }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return allBands;
   }

   public void DeleteBandFromVenue(Band newBand)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM bands_venues WHERE band_id = @BandId AND venue_id = @VenueId;";

     MySqlParameter venueId = new MySqlParameter();
     venueId.ParameterName = "@VenueId";
     venueId.Value = _id;
     cmd.Parameters.Add(venueId);

     MySqlParameter bandId = new MySqlParameter();
     bandId.ParameterName = "@BandId";
     bandId.Value = newBand.GetId();
     cmd.Parameters.Add(bandId);

     cmd.ExecuteNonQuery();
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }
  }
}
