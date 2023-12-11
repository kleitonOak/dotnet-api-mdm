using AutoMapper;
using Data.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController: ControllerBase
{
    private PersonContext _context;
    private IMapper _mapper;

    public PersonController(PersonContext context, IMapper mapper)
    {   
        _context = context;
        _mapper  = mapper;
    }

    /// <summary>
    /// Add Person to MDM
    /// </summary>
    /// <param name="createPersonDto">Person Data</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddPerson([FromBody] CreatePersonDto createPersonDto)
    {
        
        Person person = _mapper.Map<Person>(createPersonDto);
        _context.people.Add(person);
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(RetrievePersonById), new {id = person.Id}, person);
    }

    /// <summary>
    /// Update Person in MDM
    /// </summary>
    /// <param name="id">Person Id</param>
    /// <param name="updatePersonDto">Person Data</param>
    /// <returns>IActionResult</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public IActionResult UpdatePerson(int id, [FromBody]UpdatePersonDto updatePersonDto)
    {
        var person = _context.people.FirstOrDefault(person => person.Id == id);
        if(person == null) return NotFound();
        
        _mapper.Map(updatePersonDto, person);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Update Partial Person in MDM
    /// </summary>
    /// <param name="id">Person Id</param>
    /// <param name="updatePersonDto">Person Data</param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePersonPartial(int id, JsonPatchDocument<UpdatePersonDto> patchDocument)
    {
        var person = _context.people.FirstOrDefault(person => person.Id == id);
        if(person == null) return NotFound();
        
        var personToBeUpdate = _mapper.Map<UpdatePersonDto>(person);
        patchDocument.ApplyTo(personToBeUpdate, ModelState);

        if(!TryValidateModel(personToBeUpdate)){
            return ValidationProblem(ModelState);
        }

        _mapper.Map(personToBeUpdate, person);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    ///    Find all records in MDM
    /// </summary>
    /// <param name="skip">How many records must be skiped</param>
    /// <param name="take">How many records should be taken</param>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadPersonDto> RetrievePeople([FromQuery]int skip = 0, [FromQuery]int take = 50){

        return _mapper.Map<List<ReadPersonDto>>(_context.people.Skip(skip).Take(take));
    }

    /// <summary>
    /// Find a record in MDM by Id
    /// </summary>
    /// <param name="id">Person Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RetrievePersonById(int id){
        var person =  _context.people.FirstOrDefault(person => person.Id == id);
        if(person  == null){
            return NotFound();
        }

        var personDto = _mapper.Map<ReadPersonDto>(person);
        return Ok(personDto);
    }

    /// <summary>
    ///     Remove a record in MDM by ID
    /// </summary>
    /// <param name="id">Person Id</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePerson(int id)
    {
        var person = _context.people.FirstOrDefault(person => person.Id == id);
        if(person == null) return NotFound();

        _context.Remove(person);
        _context.SaveChanges();

        return NoContent();
    }
}