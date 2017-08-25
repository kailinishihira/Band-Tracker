using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using BandTracker.Models;


namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

//Venues

    [HttpGet("/venues/all")]
    public ActionResult Venues()
    {
      List<Venue> allVenues = Venue.GetAll();
      return View(allVenues);
    }

    [HttpPost("/venues/all/add")]
    public ActionResult AddVenue()
    {
      string name = Request.Form["venue-name"];
      string city = Request.Form["venue-city"];
      string nameUpper = char.ToUpper(name[0]) + name.Substring(1);
      string cityUpper = char.ToUpper(city[0]) + city.Substring(1);

      Venue newVenue = new Venue(nameUpper, cityUpper);
      newVenue.Save();
      List<Venue> allVenues = Venue.GetAll();
      return View("Venues", allVenues);
    }

    [HttpGet("/venues/{id}/details")]
    public ActionResult VenueDetails(int id)
    {
      Venue thisVenue = Venue.Find(id);
      List<Band> venueBands = thisVenue.GetBands();
      List<Band> allBands = Band.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisVenue", thisVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View(model);
    }

    [HttpPost("/venues/select_band")]
    public ActionResult SelectBand()
    {
      Venue thisVenue = Venue.Find(int.Parse(Request.Form["venue-id"]));
      Band thisBand = Band.Find(int.Parse(Request.Form["band-id"]));
      thisVenue.AddBandToVenueJoinTable(thisBand);
      List<Band> venueBands = thisVenue.GetBands();
      List<Band> allBands = Band.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisVenue", thisVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View("VenueDetails", model);
    }

    [HttpPost("/venues/new_band")]
    public ActionResult AddBandToVenue()
    {
      Venue thisVenue = Venue.Find(int.Parse(Request.Form["venue-id"]));
      Band thisBand = new Band(Request.Form["band-name"]);
      thisBand.Save();
      thisVenue.AddBandToVenueJoinTable(thisBand);
      List<Band> venueBands = thisVenue.GetBands();
      List<Band> allBands = Band.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisVenue", thisVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View("VenueDetails", model);
    }

    [HttpGet("/venues/{id}/edit")]
    public ActionResult VenueEdit(int id)
    {
      Venue thisVenue = Venue.Find(id);
      return View(thisVenue);
    }

    [HttpPost("/venues/{id}/edit-name/details")]
    public ActionResult EditVenueName(int id)
    {
      Venue thisVenue = Venue.Find(id);
      thisVenue.UpdateName(Request.Form["venue-name"]);
      List<Band> venueBands = thisVenue.GetBands();
      List<Band> allBands = Band.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisVenue", thisVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View("VenueDetails", model);
    }

    [HttpPost("/venues/{id}/edit-city/details")]
    public ActionResult EditVenueCity(int id)
    {
      Venue thisVenue = Venue.Find(id);
      thisVenue.UpdateCity(Request.Form["venue-city"]);
      List<Band> venueBands = thisVenue.GetBands();
      List<Band> allBands = Band.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisVenue", thisVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View("VenueDetails", model);
    }

    [HttpGet("/venues/{id}/delete")]
    public ActionResult DeleteVenue(int id)
    {
      Venue thisVenue = Venue.Find(id);
      thisVenue.Delete();
      List<Venue> allVenues = Venue.GetAll();
      return View("Venues", allVenues);
    }

    [HttpGet("/venues/{id}/delete/{id2}")]
    public ActionResult DeleteBandFromVenue(int id, int id2)
    {
      Venue thisVenue = Venue.Find(id);
      Band thisBand = Band.Find(id2);
      thisVenue.DeleteBandFromVenue(thisBand);
      List<Band> venueBands = thisVenue.GetBands();
      List<Band> allBands = Band.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisVenue", thisVenue);
      model.Add("venueBands", venueBands);
      model.Add("allBands", allBands);
      return View("VenueDetails", model);
    }

//Bands

    [HttpGet("/bands/all")]
    public ActionResult Bands()
    {
      List<Band> allBands = Band.GetAll();
      return View(allBands);
    }

    [HttpPost("/bands/all/add")]
    public ActionResult AddBand()
    {
      string name = Request.Form["band-name"];
      string nameUpper = char.ToUpper(name[0]) + name.Substring(1);

      Band newBand = new Band(nameUpper);
      newBand.Save();
      List<Band> allBands = Band.GetAll();
      return View("Bands", allBands);
    }

    [HttpGet("/bands/{id}/details")]
    public ActionResult BandDetails(int id)
    {
      Band thisBand = Band.Find(id);
      List<Venue> bandVenues = thisBand.GetVenues();
      List<Venue> allVenues = Venue.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisBand", thisBand);
      model.Add("bandVenues", bandVenues);
      model.Add("allVenues", allVenues);
      return View(model);
    }

    [HttpPost("/bands/select_venue")]
    public ActionResult SelectVenue()
    {
      Band thisBand = Band.Find(int.Parse(Request.Form["band-id"]));
      Venue thisVenue = Venue.Find(int.Parse(Request.Form["venue-id"]));
      thisBand.AddVenueToBandJoinTable(thisVenue);
      List<Venue> bandVenues = thisBand.GetVenues();
      List<Venue> allVenues = Venue.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisBand", thisBand);
      model.Add("bandVenues", bandVenues);
      model.Add("allVenues", allVenues);
      return View("BandDetails", model);
    }

    [HttpPost("/bands/new_venue")]
    public ActionResult AddVenueToBand()
    {
      Band thisBand = Band.Find(int.Parse(Request.Form["band-id"]));
      Venue newVenue = new Venue(Request.Form["venue-name"], Request.Form["venue-city"]);
      newVenue.Save();
      thisBand.AddVenueToBandJoinTable(newVenue);
      List<Venue> bandVenues = thisBand.GetVenues();
      List<Venue> allVenues = Venue.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisBand", thisBand);
      model.Add("bandVenues", bandVenues);
      model.Add("allVenues", allVenues);
      return View("BandDetails", model);
    }

    [HttpGet("/bands/{id}/edit")]
    public ActionResult BandEdit(int id)
    {
      Band thisBand = Band.Find(id);
      return View(thisBand);
    }

    [HttpPost("/bands/{id}/edit-name/details")]
    public ActionResult EditBandName(int id)
    {
      Band thisBand = Band.Find(id);
      thisBand.UpdateName(Request.Form["band-name"]);
      List<Venue> bandVenues = thisBand.GetVenues();
      List<Venue> allVenues = Venue.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisBand", thisBand);
      model.Add("bandVenues", bandVenues);
      model.Add("allVenues", allVenues);
      return View("BandDetails", model);
    }

    [HttpGet("/bands/{id}/delete")]
    public ActionResult DeleteBand(int id)
    {
      Band thisBand = Band.Find(id);
      thisBand.Delete();
      List<Band> allBands = Band.GetAll();
      return View("Bands", allBands);
    }

    [HttpGet("/bands/{id}/delete/{id2}")]
    public ActionResult DeleteVenueFromBand(int id, int id2)
    {
      Band thisBand = Band.Find(id);
      Venue thisVenue = Venue.Find(id2);
      thisBand.DeleteVenueFromBand(thisVenue);
      List<Venue> bandVenues = thisBand.GetVenues();
      List<Venue> allVenues = Venue.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("thisBand", thisBand);
      model.Add("bandVenues", bandVenues);
      model.Add("allVenues", allVenues);
      return View("BandDetails", model);
    }

  }
}
