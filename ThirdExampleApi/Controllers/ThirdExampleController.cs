using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ThirdExampleApi.Contract;
using ThirdExampleApi.Data;

namespace ThirdExampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ThirdExampleController : ControllerBase
{
    private readonly IExampleMongoDatabase _mongoDatabase;

    public ThirdExampleController(IExampleMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ThirdExampleDto>))]
    public async Task<IActionResult> GetAll()
    {
        var items = (await _mongoDatabase.GetCollection<ThirdExampleEntity>().Find(x => true).ToListAsync()).Select(x =>
            new ThirdExampleDto
            {
                Id = x.Id,
                ThirdName = x.ThirdName
            });

        return Ok(items);
    }

    [HttpGet("/{id:guid}")]
    [ProducesResponseType(typeof(ThirdExampleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var item = await _mongoDatabase.GetCollection<ThirdExampleEntity>().Find(x => x.Id == id).FirstOrDefaultAsync();

        var response = item != null ? new ThirdExampleDto {Id = item.Id, ThirdName = item.ThirdName} : null;

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post([FromBody] ThirdExampleDto request)
    {
        await _mongoDatabase.GetCollection<ThirdExampleEntity>().InsertOneAsync(new ThirdExampleEntity
            {Id = request.Id != default ? request.Id : Guid.NewGuid(), ThirdName = request.ThirdName});

        return Accepted();
    }
}