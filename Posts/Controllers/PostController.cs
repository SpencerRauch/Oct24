using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.Controllers;


[SessionCheck]
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
        newPost.UserId = (int)HttpContext.Session.GetInt32("UserId");
        _context.Add(newPost);
        _context.SaveChanges();
        // _context.Posts.Add(newPost);
        return RedirectToAction("AllPosts");
    }

    [HttpGet("posts")]
    public ViewResult AllPosts()
    {
        List<Post> Posts = _context.Posts
                                    .Include(p=>p.Poster)
                                    .Include(p => p.UserLikes)
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Take(100)
                                    .ToList();
        return View(Posts);
    }

    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        Post? SinglePost = _context.Posts
                                    .Include(p => p.UserLikes)
                                    .ThenInclude(upl => upl.LikingUser)
                                    .Include(p => p.Poster)
                                    .Include(p => p.UserComments)
                                    .ThenInclude(uc => uc.CommentingUser)
                                    .FirstOrDefault(p => p.PostId == postId);
        if (SinglePost == null)
        {
            return RedirectToAction("AllPosts");
        }
        return View(SinglePost);
    }


    [HttpPost("posts/{postId}/delete")]
    public RedirectToActionResult DeletePost(int postId)
    {
        Post? SinglePost = _context.Posts.SingleOrDefault(p => p.PostId == postId);
        if (SinglePost != null && SinglePost.UserId == (int)HttpContext.Session.GetInt32("UserId"))
        {
            _context.Remove(SinglePost);
            _context.SaveChanges();
        }
        return RedirectToAction("AllPosts");
    }

    [HttpGet("posts/{postId}/edit")]
    public IActionResult EditPost(int postId)
    {
        Post? ToBeEdited = _context.Posts.FirstOrDefault(p => p.PostId == postId);  
        if (ToBeEdited == null)
        {
            return RedirectToAction("AllPosts");
        }
        return View(ToBeEdited);
    }

    [HttpPost("posts/{postId}/update")]
    public IActionResult UpdatePost(int postId, Post editedPost)
    {
        Post? OldPost = _context.Posts.FirstOrDefault(p => p.PostId == postId);
        if (!ModelState.IsValid || OldPost == null)
        {
            if (OldPost == null)
            {
                ModelState.AddModelError("Title","Post not found, what did you do?!?");
            }
            return View("EditPost",OldPost);
        } 
        OldPost.Title = editedPost.Title;
        OldPost.Body = editedPost.Body;
        OldPost.ImgURL = editedPost.ImgURL;
        OldPost.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("ViewPost", new{postId});

    }

    [HttpPost("posts/{postId}/like")]
    public IActionResult ToggleLike(int postId)
    {
        int UserId = (int)HttpContext.Session.GetInt32("UserId");
        UserPostLike? existingLike = _context.UserPostLikes.FirstOrDefault(upl => upl.UserId == UserId && upl.PostId == postId);
        if (existingLike == null)
        {
            UserPostLike newLike = new(){UserId=UserId,PostId=postId};
            _context.Add(newLike);
        }
        else
        {
            _context.Remove(existingLike);
        }
        _context.SaveChanges();
        Console.WriteLine(HttpContext.Request.Headers.Referer);
        
        return RedirectToAction("AllPosts");
    }
}
