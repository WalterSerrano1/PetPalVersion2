using System.ComponentModel.DataAnnotations;
using PetPal.Models;

public class User
{
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    // Navigation property - if you need it
    public List<Pet> Pets { get; set; } = new List<Pet>();
}