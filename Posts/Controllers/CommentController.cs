using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.Controllers;


[SessionCheck]
public class CommentController : Controller
{
    private MyContext _context; //dbConnection

    private readonly ILogger<CommentController> _logger;

    public CommentController(ILogger<CommentController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("comments/create")]
    public IActionResult AddComment(UserPostComment newComment)
    {
        if (!ModelState.IsValid)
        {
            Post? SinglePost = _context.Posts
                                    .Include(p => p.UserLikes)
                                    .ThenInclude(upl => upl.LikingUser)
                                    .Include(p => p.Poster)
                                    .Include(p => p.UserComments)
                                    .ThenInclude(uc => uc.CommentingUser)
                                    .FirstOrDefault(p => p.PostId == newComment.PostId);
            return View("../Post/ViewPost",SinglePost);
        }
        newComment.UserId = (int)HttpContext.Session.GetInt32("UserId");
        _context.Add(newComment);
        _context.SaveChanges();
        return RedirectToAction("ViewPost","Post",new{postId = newComment.PostId});
    }

    [HttpPost("comments/{commentId}/delete")]
    public RedirectToActionResult DeleteComment(int commentId)
    {
        UserPostComment? CommentInDb = _context.UserPostComments.SingleOrDefault(upc => upc.UserPostCommentId == commentId);
        if (CommentInDb != null)
        {
            _context.Remove(CommentInDb);
            _context.SaveChanges();
        }
        else
        {
            return RedirectToAction("AllPosts","Post");
        }
        return RedirectToAction("ViewPost","Post", new {postId = CommentInDb.PostId});
    }

}
