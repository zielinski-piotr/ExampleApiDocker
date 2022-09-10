using Microsoft.AspNetCore.Mvc;
using SecondExampleApi.Contract;
using SecondExampleApi.Redis;

namespace SecondExampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SecondExampleController : ControllerBase
{
    private readonly Task<RedisConnection> _redisConnectionFactory;

    public SecondExampleController(Task<RedisConnection> redisConnectionFactory)
    {
        _redisConnectionFactory = redisConnectionFactory;
    }
    
    [HttpGet("/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SecondExampleDto))]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var redisConnection = await _redisConnectionFactory;
        var result = await redisConnection.BasicRetryAsync(async (db) => await db.StringGetAsync(id.ToString()));

        return Ok(new SecondExampleDto
        {
            Id = id,
            SecondName = result
        });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Post([FromBody] SecondExampleDto request)
    {
        var redisConnection = await _redisConnectionFactory;
        await redisConnection.BasicRetryAsync(async (db) => await db.StringSetAsync(request.Id.ToString(), request.SecondName));
        
        return Accepted();
    }
}