using System.ComponentModel.DataAnnotations;

namespace quiz.Models
{
    public class SuspendedUser
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
