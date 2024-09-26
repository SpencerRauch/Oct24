using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
        return View();
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
            return View("Index");
        }
        Console.WriteLine($"{newPet.Name} is a {newPet.Age} year old {newPet.Species}");
        Console.WriteLine($"{(newPet.DoesItBite ? "They are a biter!" : "What a sweet pet")}");

        FakePetDb.Add(newPet);
        // FakePetDb.SaveChanges();
        
        return RedirectToAction("Results");
    }

    [HttpGet("results")]
    public ViewResult Results()
    {
        return View(FakePetDb);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
