using AutoMapper;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Validators;
using StoreProject.Common.Exceptions;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GenrePartialDto>> GetGenres()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            var genresDto = _mapper.Map<IEnumerable<GenrePartialDto>>(genres);
            return genresDto;
        }
        public async Task<GenreDto> GetGenre(string id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            var genreDto = _mapper.Map<GenreDto>(genre);
            return genreDto;
        }

        public async Task<GenrePartialDto> AddGenre(GenreCreateDto newGenreDto)
        {
            await CheckIfDuplicateNameExists(newGenreDto.Name);
            //if the genre doesn't exist, create new product in db
            var newGenre = _mapper.Map<Genre>(newGenreDto);
            await _unitOfWork.Genres.AddAsync(newGenre);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<GenrePartialDto>(newGenre);
        }
        public async Task UpdateGenre(GenreCreateDto genreToUpdate, string id)
        {
            var existingGenre = await CheckIfGenreExists(id);
            //check if the genre with the same name already exists in db
            await CheckIfDuplicateNameExists(genreToUpdate.Name, id);
            //if the product exists, update it in db
            _mapper.Map(genreToUpdate, existingGenre);
            await _unitOfWork.Genres.UpdateAsync(existingGenre);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGenre(string id)
        {
            //check if the genre exists in db
            var genre = await CheckIfGenreExists(id);
            //if the genre exists, delete it from db
            await _unitOfWork.Genres.DeleteAsync(genre);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Genre> CheckIfGenreExists(string id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
            {
                // Genre does not exist
                throw new ArgumentException($"Genre with the ID ({id}) doesn't exist.");
            }
            return genre;
        }
        private async Task CheckIfDuplicateNameExists(string name, string id = null)
        {
            var productsWithSameName = await _unitOfWork.Genres.FindAsync(p => p.Name == name && (id == null || p.Id != id));
            if (productsWithSameName.Any())
            {
                throw new ArgumentException($"Genre with the same name ({name}) already exists.");
            }
        }
    }
}
