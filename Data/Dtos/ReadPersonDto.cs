using System.ComponentModel.DataAnnotations;

namespace Data.Dtos;
public class ReadPersonDto
{
    public int Id { get; set;}
    public string? Name {get; set;}
    public DateOnly BirthDate {get; set;}
    public string? Gender {get; set;}
}