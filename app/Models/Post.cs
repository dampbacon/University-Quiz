using System.ComponentModel.DataAnnotations;

namespace quiz.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }
        public string Time { get; set; }
        public string? Likes { get; set; }
        public int IdOfRepliedPost { get; set; }
    }
}
