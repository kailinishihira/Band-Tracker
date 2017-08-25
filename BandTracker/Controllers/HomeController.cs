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

    [HttpGet("/venues/all")]
    public ActionResult Venues()
    {
      List<Venue> allVenues = Venue.GetAll();
      return View();
    }

  }
}
