using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using StoreProject.Common.Constants;

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
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<GenrePartialDto>> PostGenre(GenreCreateOrUpdateDto newGenre)
        {
            var createdGenreDto = await _genreService.AddGenre(newGenre);
            return Created("", createdGenreDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<GenrePartialDto>> PutGenre(GenreCreateOrUpdateDto genre, string id)
        {
            await _genreService.UpdateGenre(genre, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteGenre(string id)
        {
            await _genreService.DeleteGenre(id);
            return NoContent();
        }
    }
}
