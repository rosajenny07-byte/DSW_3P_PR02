namespace DSW_3P_PR02.Controllers;

using DSW_3P_PR02.Models;
using DSW_3P_PR02.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EventosController : ControllerBase
{
    private readonly IEvento _eventoService;

    public EventosController(IEvento eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Evento>), StatusCodes.Status200OK)]
    public ActionResult<List<Evento>> GetAll()
    {
        var eventos = _eventoService.GetAll();
        return Ok(eventos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Evento> GetById(int id)
    {
        var evento = _eventoService.GetById(id);
        if (evento == null)
        {
            return NotFound(new { message = $"No se encontró el evento con id {id}." });
        }

        return Ok(evento);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Evento), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Evento> Create([FromBody] Evento evento)
    {
        try
        {
            var created = _eventoService.Add(evento);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] Evento evento)
    {
        try
        {
            _eventoService.Update(id, evento);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        try
        {
            _eventoService.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
