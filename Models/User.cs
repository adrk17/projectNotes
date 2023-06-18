using System.ComponentModel.DataAnnotations;

namespace projectNotes.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
