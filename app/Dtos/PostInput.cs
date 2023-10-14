using System.ComponentModel.DataAnnotations;

namespace quiz.Dtos
{
    public class PostInput
    {
        public int IdOfRepliedPost { get; set; } = 0;
        [Required]
        public string Message { get; set; }
    }
}
