#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetParty.Models;

public class Pet
{
    // public string? Name { get;set; }
    // public string Name { get;set; } =null!;
    [DisplayName("Pet Name")]
    [Required(ErrorMessage = "wat Iz ur Naem?")]
    [MinLength(3)]
    public string Name { get;set; }
    // [DataType(DataType.Password)]
    [Required]
    public string Species { get;set; }
    public bool DoesItBite { get;set; }
    [Required]
    public int? Age { get;set; }

}