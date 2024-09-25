using Microsoft.AspNetCore.Mvc;
// Be sure to use your own project's namespace here! 
namespace FirstWeb.Controllers;  


// [Route("pizza")]
public class FirstController : Controller   // Remember inheritance?    
{      
    [HttpGet] // We will go over this in more detail on the next page    
    [Route("")] // We will go over this in more detail on the next page //pizza/page
    public string Index()        
    {            
    	return "Hello World from HelloController!";        
    }

    [HttpGet("two")]
    public string PageTwo()
    {
        return "<h1>This is page two</h1>";
    }

    [HttpGet("params/{id}/{name}")]
    public string Params(int id, string name)
    {
        return $"{name} supplied id {id}";
    }

    [HttpGet("view/{id}/{name}")]
    public ViewResult ViewParams(int id, string name)
    {
        ViewBag.id = id;
        ViewBag.name = name;
        return View();
    }

    [HttpGet("view")]
    public ViewResult FirstView()
    {
        return View("RazorPage");
    }
    

    [HttpGet("{**path}")]
    public string Lost()
    {
        return "You are lost";
    }

}