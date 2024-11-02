using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly PeopleContext _context;

    public PeopleController(PeopleContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all people.
    /// </summary>
    /// <returns>A list of people.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all people", Description = "Gets a list of all people in the database.")]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        return await _context.People.ToListAsync();
    }

    /// <summary>
    /// Retrieves a person by ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>The person with the specified ID.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieves a person by ID", Description = "Gets a person from the database by their ID.")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        var person = await _context.People.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="person">The person to create.</param>
    /// <returns>The created person.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new person", Description = "Adds a new person to the database.")]
    public async Task<ActionResult<Person>> PostPerson(Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        person.DateCreated = DateTime.Now;
        _context.People.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
    }

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The ID of the person to update.</param>
    /// <param name="person">The updated person data.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Updates an existing person", Description = "Updates the details of an existing person in the database.")]
    public async Task<IActionResult> PutPerson(int id, Person person)
    {
        if (id != person.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a person by ID.
    /// </summary>
    /// <param name="id">The ID of the person to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletes a person by ID", Description = "Removes a person from the database by their ID.")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return _context.People.Any(e => e.Id == id);
    }
}
