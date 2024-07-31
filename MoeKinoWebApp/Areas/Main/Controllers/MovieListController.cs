using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Identity.Client;
using MoeKinoWebApp.Data;
using MoeKinoWebApp.Models;

namespace MvcApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class MovieListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieListController(ApplicationDbContext db){
            _db = db;
        } 

        public IActionResult Index()
        {
            ViewData["Genres"] = _db.Genres.ToList();
            ViewData["Countries"] = _db.Countries.ToList();
            ViewData["Years"] = _db.Movies.Select(m => m.ReleaseYear).Distinct().OrderByDescending(y => y).ToList();

            var moviesWithDetails = _db.Movies
                .Select(movie => new
                {
                    MovieId = movie.Id,
                    MovieReleaseYear = movie.ReleaseYear,
                    MovieTitle = movie.TitleEn,
                    MovieDescription = movie.DescriptionEn,
                    MovieTrailerLink =  movie.TrailerLinkEn,
                    Genres = movie.MovieGenres.Select(mg => new
                    {
                        GenreId = mg.Genre.Id,
                        Name = mg.Genre.NameEn,
                    }).ToList(),
                    Countries = movie.MovieCountries.Select(mc => new
                    {
                        CountryID = mc.Country.Id,
                        Name = mc.Country.NameEn,
                    }).ToList(),
                    Images = movie.MovieImages.Where(mi => mi.IsPoster).Select(mi => new
                    {
                        ImageId = mi.Id,
                        ImageData = mi.Image != null ? Convert.ToBase64String(mi.Image) : null,
                        ImageIsPoster = mi.IsPoster
                    }).ToList()
                })
                .ToList();

            var groupedMovies = moviesWithDetails
                .SelectMany(movie => movie.Genres)
                .Distinct()
                .ToDictionary(
                genre => genre.GenreId,
                genre => new MovieCarouselViewModel
                {
                    GenreName = genre.Name,
                    Movies = moviesWithDetails
                        .Where(movie => movie.Genres.Any(mg => mg.GenreId == genre.GenreId))
                        .Select(movie => new MovieThumbnail
                        {
                            Id = movie.MovieId,
                            Title = movie.MovieTitle,
                            ReleaseYear = movie.MovieReleaseYear,
                            Poster = movie.Images.FirstOrDefault()?.ImageData 
                        })
                        .ToList()
                }
            );

            return View(groupedMovies);
        }

        public IActionResult MovieList(int? releaseYear, string? genre, string? country, string? search)
        {
            IQueryable<Movie> movieListQuery = _db.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCountries)
                    .ThenInclude(mc => mc.Country)
                .Include(m => m.MovieImages);

            if (releaseYear.HasValue)
            {
                movieListQuery = movieListQuery.Where(m => m.ReleaseYear == releaseYear.Value);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                movieListQuery = movieListQuery.Where(m => m.MovieGenres.Any(mg => mg.Genre.NameEn == genre));
            }

            if (!string.IsNullOrEmpty(country))
            {
                movieListQuery = movieListQuery.Where(m => m.MovieCountries.Any(mc => mc.Country.NameEn == country));
            }

            if (!string.IsNullOrEmpty(search))
            {
                movieListQuery = movieListQuery.Where(m => m.TitleEn.Contains(search));
            }

            var movieList = movieListQuery.ToList();

            if (movieList == null || !movieList.Any())
            {
                return NotFound();
            }

            var viewModel = new MovieListViewModel
            {
                Movies = movieList.Select(movie => new MovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.TitleEn,
                    Poster = Convert.ToBase64String(movie.MovieImages.FirstOrDefault(mi => mi.IsPoster)?.Image),
                    Description = movie.DescriptionEn,
                    ReleaseYear = movie.ReleaseYear,
                    Genres = movie.MovieGenres.Select(mg => mg.Genre).ToList(),
                    Countries = movie.MovieCountries.Select(mc => mc.Country).ToList(),
                }).ToList(),

                Genres = _db.Genres.ToList(),
                Countries = _db.Countries.ToList(),
                Years = _db.Movies.Select(m => m.ReleaseYear).Distinct().OrderBy(y => y).ToList()
            };

            return View(viewModel);
        }


        public IActionResult MovieDetails(int? id)
        {
            var movie = _db.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCountries)
                .ThenInclude(mc => mc.Country)
                .Include(m => m.MovieImages)
                .Include(m => m.MovieParticipants)
                .ThenInclude(mp => mp.Person)
                .Include(m => m.MovieParticipants)
                .ThenInclude(mp => mp.MovieParticipantCategory)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }


            var movieDetails = new MovieDetailsViewModel
            {
                Title = movie.TitleEn,
                Description = movie.DescriptionEn,
                TrailerLink = movie.TrailerLinkEn,
                Poster = movie.MovieImages.FirstOrDefault(mi => mi.IsPoster) != null 
                ? Convert.ToBase64String(movie.MovieImages.FirstOrDefault(mi => mi.IsPoster).Image) 
                : null,
                Genres = movie.MovieGenres.Select(mg => mg.Genre.NameEn).ToList(),
                Countries = movie.MovieCountries.Select(mc => mc.Country.NameEn).ToList(),
                Participants = movie.MovieParticipants
                    .GroupBy(mp => mp.MovieParticipantCategory.NameEn)
                    .ToDictionary(
                        grp => grp.Key,
                        grp => grp.Select(mp => mp.Person).ToList()
                    ),
                Images = movie.MovieImages.Where(mi => !mi.IsPoster).ToDictionary(mi => mi.Id, mi => Convert.ToBase64String(mi.Image))
            };


            return View(movieDetails);
        }
    }
}