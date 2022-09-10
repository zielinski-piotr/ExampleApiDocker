using FirstExampleApi.Contract;
using FirstExampleApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstExampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FirstExampleController : ControllerBase
{
    private readonly FirstExampleDbContext _context;

    public FirstExampleController(FirstExampleDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FirstExampleDto>))]
    public async Task<IActionResult> GetAll()
    {
        var items = await _context.FirstExampleEntities.Select(x => new FirstExampleDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();

        return Ok(items);
    }

    [HttpGet("/{id:guid}")]
    [ProducesResponseType(typeof(FirstExampleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var items = await _context.FirstExampleEntities.Select(x => new FirstExampleDto
        {
            Id = x.Id,
            Name = x.Name
        }).FirstOrDefaultAsync(x => x.Id == id);

        return Ok(items);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post([FromBody] FirstExampleDto request)
    {
        await _context.AddAsync(new FirstExampleEntity()
            {Id = request.Id == default ? Guid.NewGuid() : request.Id, Name = request.Name});
        await _context.SaveChangesAsync();

        return Accepted();
    }
}