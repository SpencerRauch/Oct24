namespace Posts.Models;
using System.ComponentModel.DataAnnotations;

public class UserPostLike
{
    [Key]
    public int UserPostLikeId { get;set; }

    public DateTime CreatedAt { get;set; } = DateTime.Now;
    public DateTime UpdatedAt { get;set; } = DateTime.Now;

    //fk
    public int UserId { get;set; }
    public int PostId { get;set; }

    //nav props
    public User? LikingUser { get;set; }
    public Post? LikedPost { get;set; }


}