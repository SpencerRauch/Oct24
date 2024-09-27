using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetParty.Models;

namespace PetParty.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private static List<Pet> FakePetDb = [];

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<SelectListItem> Options = 
        [
            new SelectListItem(" --- please choose ---","",true,true),
            new("Bear","Bear"),
            new("Deer","Deer"),
            new("Bobcat","Bobcat"),
            new("Coyote","Coyote"),
            new("Raccoon","Raccoon")

        ];
        ViewBag.Options = Options;
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("vmfun")]
    public ViewResult VMFun()
    {
        // int MyNumToPass = 5959344;
        List<string> Names = ["Bob","Alice"];
        return View("vmfun",Names);
    }

    [HttpPost("process")]
    public IActionResult Process(Pet newPet)
    {
        if (!ModelState.IsValid)
        {
            string message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            Console.WriteLine(message);
        }        
        if (!ModelState.IsValid)
        {
            return Index();
        }
        Console.WriteLine($"{newPet.Name} is a {newPet.Age} year old {newPet.Species}");
        Console.WriteLine($"{(newPet.DoesItBite ? "They are a biter!" : "What a sweet pet")}");

        FakePetDb.Add(newPet);
        HttpContext.Session.SetString("LastPet",newPet.Name);
        // FakePetDb.SaveChanges();
        
        return RedirectToAction("Results");
    }

    [HttpGet("results")]
    public IActionResult Results()
    {
        string? LastPet = HttpContext.Session.GetString("LastPet");
        if (LastPet == null)
        {
            return RedirectToAction("Index");
        }
        return View(FakePetDb);
    }

    [HttpPost("set")]
    public RedirectToActionResult SetFilter(int limit)
    {
        HttpContext.Session.SetInt32("Limit",limit);
        return RedirectToAction("Results");
    }

    [HttpPost("clear")]
    public RedirectToActionResult ClearFilter()
    {
        // HttpContext.Session.Clear();
        HttpContext.Session.Remove("Limit");
        return RedirectToAction("Results");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
