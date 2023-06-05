using System.ComponentModel.DataAnnotations;

namespace projectNotes.Models
{
    public class LoginSuccessViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public bool Login { get; set; }

        public string Message()
        {
            if (Login)
            {
                return $"Login successful!\n Welcome back {Username}";
            }
            else
            {
                return $"Registration successful!\n Welcome {Username}, let's get started with your first note!";
            }
        }
    }
}
