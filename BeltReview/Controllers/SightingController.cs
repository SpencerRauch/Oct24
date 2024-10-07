using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeltReview.Models;

namespace BeltReview.Controllers;

[SessionCheck]
public class SightingController : Controller
{
    private readonly ILogger<SightingController> _logger;

    private MyContext _context;

    public SightingController(ILogger<SightingController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("dashboard")]
    public ViewResult Dashboard()
    {
        List<Sighting> AllSightings = _context.Sightings
                                            .Include(s => s.ReportingUser)
                                            .Include(s => s.UserBeliefs)
                                            .ToList();
        return View(AllSightings);
    }

    [HttpGet("sightings/new")]
    public ViewResult NewSighting()
    {
        return View();
    }

    [HttpPost("sightings/create")]
    public IActionResult CreateSighting(Sighting newSighting)
    {
        if (!ModelState.IsValid)
        {
            return View("NewSighting");
        }
        newSighting.UserId = (int)HttpContext.Session.GetInt32("UserId");
        _context.Add(newSighting);
        _context.SaveChanges();


        return RedirectToAction("ViewSighting",new{sightingId = newSighting.SightingId});

    }

    [HttpGet("sightings/{sightingId}")]
    public IActionResult ViewSighting(int sightingId)
    {
        Sighting? OneSighting = _context.Sightings
                                        .Include(s => s.ReportingUser)
                                        .Include(s => s.UserBeliefs)
                                        .ThenInclude(usb => usb.BelievingUser)
                                        .FirstOrDefault(s => s.SightingId == sightingId);
        if (OneSighting == null)
        {
            return RedirectToAction("Dashboard");
        }
        return View("ViewSighting",OneSighting);
    }

    [HttpPost("sightings/{sightingId}/delete")]
    public IActionResult DeleteSighting(int sightingId)
    {
        Sighting? ToBeDeleted = _context.Sightings.FirstOrDefault(s => s.SightingId == sightingId);
        if (ToBeDeleted != null)
        {
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction("Dashboard");
    }

    [HttpPost("sightings/{sightingId}/toggle_belief")]
    public RedirectToActionResult ToggleBelief(int sightingId)
    {
        int UserId = (int)HttpContext.Session.GetInt32("UserId");
        UserSightingBelief? ExistingBelief = _context.UserSightingBeliefs
                                    .FirstOrDefault(usb => usb.SightingId == sightingId && usb.UserId == UserId);
        if (ExistingBelief == null)
        {
            UserSightingBelief newBelief = new(){UserId = UserId, SightingId = sightingId};
            _context.Add(newBelief);
        }
        else
        {
            _context.Remove(ExistingBelief);
        }
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

}
