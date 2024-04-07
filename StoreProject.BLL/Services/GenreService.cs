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
            var genre = await _unitOfWork.Genres.FindAsync(g => g.Name == newGenreDto.Name);
            if (genre.Any())
            {
                throw new ArgumentException($"Genre with the same name ({newGenreDto.Name}) already exists.");
            }
            //if the genre doesn't exist, create new product in db
            var newGenre = _mapper.Map<Genre>(newGenreDto);
            await _unitOfWork.Genres.AddAsync(newGenre);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<GenrePartialDto>(newGenre);
        }
        public async Task<bool> UpdateGenre(GenreCreateDto genreToUpdate, string id)
        {
            var existingGenre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (existingGenre == null)
            {
                throw new NotFoundException($"Genre with ID {id} not found.");
            }
            //check if the genre with the same name already exists in db
            var genreWithNameDuplicate = await _unitOfWork.Genres.FindAsync(p => p.Name == genreToUpdate.Name && p.Id != id);
            if (genreWithNameDuplicate.Any())
            {
                throw new ArgumentException($"Genre with the same name ({genreToUpdate.Name}) already exists.");
            }
            //if the product exists, update it in db
            _mapper.Map(genreToUpdate, existingGenre);
            await _unitOfWork.Genres.UpdateAsync(existingGenre);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteGenre(string id)
        {
            //check if the genre exists in db
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
            {
                throw new NotFoundException($"Genre with ID {id} not found.");
            }
            //if the genre exists, delete it from db
            await _unitOfWork.Genres.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        

        
    }
}
