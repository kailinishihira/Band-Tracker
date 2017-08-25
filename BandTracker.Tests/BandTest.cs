using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Band()
    {
      Band firstBand = new Band("The Beatles");
      Band secondBand = new Band("The Beatles");
      Assert.AreEqual(firstBand, secondBand);
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Band.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_BandList()
    {
      Band testBand = new Band("Beatles");
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Band testBand = new Band("Beatles");
      testBand.Save();
      Band savedBand = Band.GetAll()[0];
      int result = savedBand.GetId();
      int testId = testBand.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsRecipeInDatabase_Recipe()
    {
      Band testBand = new Band("Beatles");
      testBand.Save();
      Band foundBand = Band.Find(testBand.GetId());
      Assert.AreEqual(testBand, foundBand);
    }

    [TestMethod]
    public void UpdateName_UpdatesBandNameInDatabase_String()
    {
      string name = "The Beatles";
      Band testBand = new Band(name);
      testBand.Save();
      string newName = "Rilo Kiley";
      testBand.UpdateName(newName);
      string result = Band.Find(testBand.GetId()).GetName();
      Assert.AreEqual(newName, result);
    }

    [TestMethod]
    public void Delete_DeletesBandFromDatabase_BandList()
    {
      Band testBand1 = new Band("The Beatles");
      testBand1.Save();
      Band testBand2 = new Band("Rilo Kiley");
      testBand2.Save();
      testBand1.Delete();
      List<Band> resultBands = Band.GetAll();
      List<Band> testBandList = new List<Band> {testBand2};
      CollectionAssert.AreEqual(testBandList, resultBands);
    }

    [TestMethod]
    public void AddVenueToBandJoinTable_AddsToJoinTable_VenueList()
    {
      Band testBand = new Band("The Beatles");
      testBand.Save();
      Venue testVenue1 = new Venue("Red Rocks Amphitheatre", "Morrison");
      testVenue1.Save();
      Venue testVenue2 = new Venue("Waikiki Shell", "Honolulu");
      testVenue2.Save();
      testBand.AddVenueToBandJoinTable(testVenue1);
      testBand.AddVenueToBandJoinTable(testVenue2);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue1, testVenue2};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Delete_DeletesBandAssociationsFromDatabase_BandList()
    {
      Venue testVenue = new Venue("Red Rocks Amphitheatre", "Morrison");
      testVenue.Save();
      Band testBand = new Band("The Beatles");
      testBand.Save();
      testBand.AddVenueToBandJoinTable(testVenue);
      testBand.Delete();
      List<Band> resultVenueBands = testVenue.GetBands();
      List<Band> testVenueBands = new List<Band> {};
      CollectionAssert.AreEqual(testVenueBands, resultVenueBands);
    }

    [TestMethod]
    public void GetVenues_ReturnsAllVenueBands_VenueList()
    {
      Band testBand = new Band("The Beatles");
      testBand.Save();
      Venue testVenue1 = new Venue("Red Rocks Amphitheatre", "Morrison");
      testVenue1.Save();
      Venue testVenue2 = new Venue("Waikiki Shell", "Honolulu");
      testVenue2.Save();
      testBand.AddVenueToBandJoinTable(testVenue1);
      testBand.AddVenueToBandJoinTable(testVenue2);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1, testVenue2};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void DeleteBandFromVenue_DeletesOneVenueFromBandJoinTable_VenueList()
    {
      Band testBand = new Band("The Beatles");
      testBand.Save();
      Venue testVenue1 = new Venue("Waikiki Shell", "Honolulu");
      testVenue1.Save();
      Venue testVenue2 = new Venue("Red Rocks Amphitheatre", "Morrison");
      testVenue2.Save();
      testBand.AddVenueToBandJoinTable(testVenue1);
      testBand.AddVenueToBandJoinTable(testVenue2);
      testBand.DeleteVenueFromBand(testVenue1);
      List<Venue> expectedList = new List<Venue> {testVenue2};
      List<Venue> actualList = testBand.GetVenues();
      CollectionAssert.AreEqual(expectedList, actualList);
    }
  }
}
