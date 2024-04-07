using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Interfaces
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenrePartialDto>> GetGenres();
        public Task<GenreDto> GetGenre(string id);
        public Task<GenrePartialDto> AddGenre(GenreCreateDto newGenreDto);
        public Task UpdateGenre(GenreCreateDto genreToUpdate, string id);
        public Task DeleteGenre(string id);
    }
}
