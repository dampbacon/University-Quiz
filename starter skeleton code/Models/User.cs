using System.ComponentModel.DataAnnotations;

namespace quiz.Models
{
    public class User
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Following { get; set; }
    }
}
