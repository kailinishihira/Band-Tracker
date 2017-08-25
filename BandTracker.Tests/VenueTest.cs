using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_test;";
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_True()
    {
      Venue firstVenue = new Venue("Waikiki Shell", "Honolulu");
      Venue secondVenue = new Venue("Waikiki Shell", "Honolulu");
      Assert.AreEqual(firstVenue, secondVenue);
    }

    [TestMethod]
    public void GetAll_VenuesEmptyAtFirst_0()
    {
      int result = Venue.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToVenue_Id()
    {
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];
      int result = savedVenue.GetId();
      int testId = testVenue.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesVenueToDatabase_VenueList()
    {
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabase_Venue()
    {
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      Venue foundVenue = Venue.Find(testVenue.GetId());
      Assert.AreEqual(testVenue, foundVenue);
    }

    [TestMethod]
    public void UpdateName_UpdatesVenueNameInDatabase_String()
    {
      string name = "Waikiki Shell";
      Venue testVenue = new Venue(name, "Honolulu");
      testVenue.Save();
      string newName = "Red Rocks Amphitheatre";
      testVenue.UpdateName(newName);
      string result = Venue.Find(testVenue.GetId()).GetName();
      Assert.AreEqual(newName, result);
    }

    [TestMethod]
    public void UpdateCity_UpdatesVenueCityInDatabase_String()
    {
      string city = "Honolulu";
      Venue testVenue = new Venue("Waikiki Shell", city);
      testVenue.Save();
      string newCity = "Seattle";
      testVenue.UpdateCity(newCity);
      string result = Venue.Find(testVenue.GetId()).GetCity();
      Assert.AreEqual(newCity, result);
    }

    [TestMethod]
    public void Delete_DeletesVenueFromDatabase_VenueList()
    {
      Venue testVenue1 = new Venue("Waikiki Shell", "Honolulu");
      testVenue1.Save();
      Venue testVenue2 = new Venue("Red Rocks Amphitheatre", "Morrison");
      testVenue2.Save();
      testVenue1.Delete();
      List<Venue> resultVenues = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue> {testVenue2};
      CollectionAssert.AreEqual(testVenueList, resultVenues);
    }

    [TestMethod]
    public void Delete_DeletesOnlyVenue_VenuesList()
    {
      Band testBand = new Band("Rilo Kiley");
      testBand.Save();
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      testVenue.AddBandToVenueJoinTable(testBand);
      testVenue.Delete();
      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};
      CollectionAssert.AreEqual(testBandVenues, resultBandVenues);
    }

    [TestMethod]
    public void AddBandToVenueJoinTable_AddsToJoinTable_BandList()
    {
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      Band testBand = new Band("Rilo Kiley");
      testBand.Save();
      Band testBand2 = new Band("The Beatles");
      testBand2.Save();
      testVenue.AddBandToVenueJoinTable(testBand);
      testVenue.AddBandToVenueJoinTable(testBand2);
      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetBands_ReturnsAllBandsForVenue_BandList()
    {
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      Band testBand1 = new Band("Rilo Kiley");
      testBand1.Save();
      Band testBand2 = new Band("The Beatles");
      testBand2.Save();
      testVenue.AddBandToVenueJoinTable(testBand1);
      testVenue.AddBandToVenueJoinTable(testBand2);
      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1, testBand2};
      CollectionAssert.AreEqual(testList, savedBands);
    }

    [TestMethod]
    public void DeleteBandFromVenue_DeletesOneBandFromVenueJoinTable_BandList()
    {
      Venue testVenue = new Venue("Waikiki Shell", "Honolulu");
      testVenue.Save();
      Band testBand1 = new Band("The Beatles");
      testBand1.Save();
      Band testBand2 = new Band("Rilo Kiley");
      testBand2.Save();

      testVenue.AddBandToVenueJoinTable(testBand1);
      testVenue.AddBandToVenueJoinTable(testBand2);      testVenue.DeleteBandFromVenue(testBand1);
      List<Band> expectedList = new List<Band> {testBand2};
      List<Band> actualList = testVenue.GetBands();
      CollectionAssert.AreEqual(expectedList, actualList);
    }

  }
}
