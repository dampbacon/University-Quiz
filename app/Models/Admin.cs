using System.ComponentModel.DataAnnotations;

namespace quiz.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
