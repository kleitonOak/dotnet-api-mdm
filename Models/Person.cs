using System.ComponentModel.DataAnnotations;

namespace Models;

public class Person
{   
    [Key]
    [Required]
    public int Id { get; set;}
    [Required(ErrorMessage = "The field Name is required")]
    public string? Name {get; set;}
    [Required(ErrorMessage = "The field BirthDate is required")]
    public DateOnly BirthDate {get; set;}
    [Required(ErrorMessage = "The field Gender is required by OAK")]
    public string? Gender {get; set;}
}