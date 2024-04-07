using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenrePartialDto>>> GetGenres()
        {
            var genres = await _genreService.GetGenres();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(string id)
        {
            var genre = await _genreService.GetGenre(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenrePartialDto>> PostGenre(GenreCreateDto newGenre)
        {
            var createdGenreDto = await _genreService.AddGenre(newGenre);
            return Created("", createdGenreDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenrePartialDto>> PutGenre(GenreCreateDto genre, string id)
        {
            await _genreService.UpdateGenre(genre, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(string id)
        {
            await _genreService.DeleteGenre(id);
            return NoContent();
        }
    }
}
