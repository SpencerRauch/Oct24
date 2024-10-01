using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Posts.Models;

namespace Posts.Controllers;

public class PostController : Controller
{
    private MyContext _context; //dbConnection

    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("posts/new")]
    public ViewResult NewPost() => View();

    [HttpPost("posts/create")]
    public IActionResult CreatePost(Post newPost)
    {
        if (!ModelState.IsValid)
        {
            return View("NewPost");
        }
        _context.Add(newPost);
        _context.SaveChanges();
        // _context.Posts.Add(newPost);
        return RedirectToAction("AllPosts");
    }

    [HttpGet("posts")]
    public ViewResult AllPosts()
    {
        List<Post> Posts = _context.Posts.OrderByDescending(p => p.CreatedAt).Take(100).ToList();
        return View(Posts);
    }

    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        Post? SinglePost = _context.Posts.FirstOrDefault(p => p.PostId == postId);
        if (SinglePost == null)
        {
            return RedirectToAction("AllPosts");
        }
        return View(SinglePost);
    }


}
