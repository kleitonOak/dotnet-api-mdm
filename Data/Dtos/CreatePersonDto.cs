using System.ComponentModel.DataAnnotations;

namespace Data.Dtos;
public class CreatePersonDto
{
    public int Id { get; set;}

    [Required (ErrorMessage = "The Name is required")]
    [StringLength(60)]
    public string? Name {get; set;}
    [Required(ErrorMessage = "The BirthDate field is required")]
    public DateOnly BirthDate {get; set;}
    [Required(ErrorMessage = "The Gender field is required by OAK")]
    [StringLength(50)]
    public string? Gender {get; set;}
}