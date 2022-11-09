using System.ComponentModel.DataAnnotations;

namespace UdvTestTask.Models;

public class UserModel
{
    [Required]
    public string Login { get; set; } = null!;

    
    [Required]
    public string Password { get; set; } = null!;
}