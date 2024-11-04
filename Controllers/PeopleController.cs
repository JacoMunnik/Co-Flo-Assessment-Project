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

    /// <summary>
    /// Retrieves all people.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        try
        {
            var people = await _context.People.ToListAsync();
            return Ok(value: people);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Error occurred while fetching people.");
            return StatusCode(statusCode: 500, value: "Internal server error");
        }
    }

    /// <summary>
    /// Retrieves a person by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(keyValues: id);
            if (person == null)
            {
                _logger.LogWarning(message: "Person with ID {PersonId} not found.", args: id);
                return NotFound();
            }
            return Ok(value: person);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Error occurred while fetching person with ID {PersonId}.", args: id);
            return StatusCode(statusCode: 500, value: "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new person.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] Person person)
    {
        if (!IsValidPerson(person: person))
        {
            return BadRequest(modelState: ModelState);
        }

        try
        {
            person.DateCreated = DateTime.UtcNow;
            _context.People.Add(entity: person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(actionName: nameof(GetPerson), routeValues: new { id = person.Id }, value: person);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Error occurred while creating a new person.");
            return StatusCode(statusCode: 500, value: "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
    {
        if (!IsValidPerson(person: person) || person.Id != id)
        {
            return BadRequest(modelState: ModelState);
        }

        try
        {
            var existingPerson = await _context.People.FindAsync(keyValues: id);
            if (existingPerson == null)
            {
                _logger.LogWarning(message: "Person with ID {PersonId} not found.", args: id);
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
            _logger.LogError(exception: ex, message: "Error occurred while updating person with ID {PersonId}.", args: id);
            return StatusCode(statusCode: 500, value: "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a person by ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(keyValues: id);
            if (person == null)
            {
                _logger.LogWarning(message: "Person with ID {PersonId} not found.", args: id);
                return NotFound();
            }

            _context.People.Remove(entity: person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message: "Error occurred while deleting person with ID {PersonId}.", args: id);
            return StatusCode(statusCode: 500, value: "Internal server error");
        }
    }

    /// <summary>
    /// Validates the person object.
    /// </summary>
    private bool IsValidPerson(Person person)
    {
        if (person == null)
        {
            _logger.LogWarning(message: "Received null person object.");
            ModelState.AddModelError(key: "Person", errorMessage: "Person object is null");
            return false;
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning(message: "Invalid person object received.");
            return false;
        }

        return true;
    }
}
