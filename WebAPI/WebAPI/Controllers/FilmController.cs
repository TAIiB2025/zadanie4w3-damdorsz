using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? fraza = null)
        {
            var filmy = await _filmService.GetAllAsync(fraza);
            return Ok(filmy);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var film = await _filmService.GetByIdAsync(id);
            if (film == null)
                return NotFound($"Film o ID {id} nie został znaleziony");

            return Ok(film);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FilmBodyDto filmBody)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nowyFilm = await _filmService.CreateAsync(filmBody);
            return CreatedAtAction(nameof(GetById), new { id = nowyFilm.Id }, nowyFilm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FilmBodyDto filmBody)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var zaktualizowano = await _filmService.UpdateAsync(id, filmBody);
            if (!zaktualizowano)
                return NotFound($"Film o ID {id} nie został znaleziony");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usunieto = await _filmService.DeleteAsync(id);
            if (!usunieto)
                return NotFound($"Film o ID {id} nie został znaleziony");

            return NoContent();
        }
    }
}
