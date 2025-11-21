using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Shared;
// using StackExchange.Redis;   // redis desligado
// using System.Text.Json;

[ApiController]
[Route("avisos")]
public class AvisoController : ControllerBase
{
    private readonly IAvisoService _service;

    public AvisoController(IAvisoService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var aviso = await _service.GetByIdAsync(id);
        if (aviso == null) return NotFound();
        return Ok(aviso);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AvisoDto dto)
    {
        var criado = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAvisoDto dto)
    {
        var atualizado = await _service.UpdateMensagemAsync(dto);
        return Ok(atualizado);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cache = await TryGetFromRedisAsync();
        if (cache != null)
            return Ok(cache);

        var avisos = await _service.GetAtivosAsync();

        return Ok(avisos);
    }

    private async Task<List<Aviso>?> TryGetFromRedisAsync()
    {
        // var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
        // var db = redis.GetDatabase();

        // const string key = "avisos:getall";
        // var content = await db.StringGetAsync(key);

        // if (!content.HasValue)
        //     return null;

        // return JsonSerializer.Deserialize<List<Aviso>>(content);

        return null; // redis desativado
    }

    // -------------------------------------------------------
    // Método privado → salva no Redis
    // -------------------------------------------------------
    private async Task SaveToRedisAsync(List<Aviso> avisos)
    {
        // var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
        // var db = redis.GetDatabase();

        // const string key = "avisos:getall";

        // await db.StringSetAsync(
        //     key,
        //     JsonSerializer.Serialize(avisos),
        //     TimeSpan.FromMinutes(5)
        // );
    }
}
