using System.ComponentModel.DataAnnotations;

namespace projectNotes.Models;

public class ProfileModel
{
    public string? Username { get; set; }
    [Required]
    [MinLength(5)]
    public string Password { get; set; }
    [Required]
    [MinLength(5)]
    public string NewPassword { get; set; }
    [Required]
    [MinLength(5)]
    public string NewPasswordRe { get; set; }

    public string? SuccessMsg { get; set;}
}