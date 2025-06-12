using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetAllAsync(string? fraza = null);
        Task<Film?> GetByIdAsync(int id);
        Task<Film> CreateAsync(FilmBodyDto filmBody);
        Task<bool> UpdateAsync(int id, FilmBodyDto filmBody);
        Task<bool> DeleteAsync(int id);
    }

    public class FilmService : IFilmService
    {
        private static int _idGenerator = 1;
        private static readonly List<Film> _filmy = new()
        {
            new Film { Id = _idGenerator++, Tytul = "Incepcja", Rezyser = "Christopher Nolan", Gatunek = "Sci-Fi", RokWydania = 2010 },
            new Film { Id = _idGenerator++, Tytul = "Parasite", Rezyser = "Bong Joon-ho", Gatunek = "Dramat", RokWydania = 2019 },
            new Film { Id = _idGenerator++, Tytul = "Skazani na Shawshank", Rezyser = "Frank Darabont", Gatunek = "Dramat", RokWydania = 1994 },
            new Film { Id = _idGenerator++, Tytul = "Matrix", Rezyser = "Lana i Lilly Wachowski", Gatunek = "Sci-Fi", RokWydania = 1999 },
            new Film { Id = _idGenerator++, Tytul = "Gladiator", Rezyser = "Ridley Scott", Gatunek = "Historyczny", RokWydania = 2000 }
        };

        public async Task<IEnumerable<Film>> GetAllAsync(string? fraza = null)
        {
            var filmy = _filmy.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(fraza))
            {
                filmy = filmy.Where(f => f.Tytul.Contains(fraza, StringComparison.OrdinalIgnoreCase));
            }

            return await Task.FromResult(filmy);
        }

        public async Task<Film?> GetByIdAsync(int id)
        {
            var film = _filmy.FirstOrDefault(f => f.Id == id);
            return await Task.FromResult(film);
        }

        public async Task<Film> CreateAsync(FilmBodyDto filmBody)
        {
            var nowyFilm = new Film
            {
                Id = _idGenerator++,
                Tytul = filmBody.Tytul,
                Rezyser = filmBody.Rezyser,
                Gatunek = filmBody.Gatunek,
                RokWydania = filmBody.RokWydania
            };

            _filmy.Add(nowyFilm);
            return await Task.FromResult(nowyFilm);
        }

        public async Task<bool> UpdateAsync(int id, FilmBodyDto filmBody)
        {
            var film = _filmy.FirstOrDefault(f => f.Id == id);
            if (film == null)
                return await Task.FromResult(false);

            film.Tytul = filmBody.Tytul;
            film.Rezyser = filmBody.Rezyser;
            film.Gatunek = filmBody.Gatunek;
            film.RokWydania = filmBody.RokWydania;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var film = _filmy.FirstOrDefault(f => f.Id == id);
            if (film == null)
                return await Task.FromResult(false);

            _filmy.Remove(film);
            return await Task.FromResult(true);
        }
    }
}
