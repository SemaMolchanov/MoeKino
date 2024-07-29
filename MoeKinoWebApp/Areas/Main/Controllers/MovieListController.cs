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

        [HttpGet("MovieList/Index")]
        public IActionResult Index()
        {
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
                genre => new MovieViewModel
                {
                    GenreName = genre.Name,
                    Movies = moviesWithDetails
                        .Where(movie => movie.Genres.Any(mg => mg.GenreId == genre.GenreId))
                        .Select(movie => new MovieDetail
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

        
    }
}