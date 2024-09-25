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

    [HttpGet("form")]
    public ViewResult FirstForm()
    {
        return View();
    }

    // [HttpPost("process")]
    // public string ProcessForm(int id, string name)
    // {
    //     return $"{name} supplied id {id}";
    // }.

    // [HttpPost("process")]
    // public RedirectResult ProcessForm(int id, string name)
    // {
    //     return Redirect($"view/{id}/{name}");
    // }

    // [HttpPost("process")]
    // public RedirectToActionResult ProcessForm(int id, string name)
    // {
    //     return RedirectToAction("ViewParams",new {id,name=name});
    // }

    [HttpPost("process")]
    public IActionResult ProcessForm(int id, string name)
    {
        if (id == 123)
        {
            return View("SecretPage");
        }
        return RedirectToAction("ViewParams",new {id,name=name});
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