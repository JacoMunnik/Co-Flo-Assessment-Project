using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly PeopleContext _context;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(PeopleContext context, ILogger<PeopleController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        try
        {
            var people = await _context.People.ToListAsync();
            return Ok(people);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching people.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found.", id);
                return NotFound();
            }
            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching person with ID {PersonId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] Person person)
    {
        try
        {
            if (person == null)
            {
                _logger.LogWarning("Received null person object.");
                return BadRequest("Person object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid person object received.");
                return BadRequest("Invalid model object");
            }

            person.DateCreated = DateTime.UtcNow;
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new person.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
    {
        try
        {
            if (person == null || person.Id != id)
            {
                _logger.LogWarning("Invalid person object or ID mismatch.");
                return BadRequest("Invalid person object or ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid person object received.");
                return BadRequest("Invalid model object");
            }

            var existingPerson = await _context.People.FindAsync(id);
            if (existingPerson == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found.", id);
                return NotFound();
            }

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.DateOfBirth = person.DateOfBirth;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating person with ID {PersonId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found.", id);
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting person with ID {PersonId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
