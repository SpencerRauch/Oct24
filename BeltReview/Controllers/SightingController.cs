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
        // ! -- update to redirect to view one once view one is created
        return RedirectToAction("Dashboard");

    }

}
