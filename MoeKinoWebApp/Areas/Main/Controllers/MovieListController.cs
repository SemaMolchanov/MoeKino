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

        public MovieListController(ApplicationDbContext db)
        {
            _db = db;
        } 

        public IActionResult Index(string? lang)
        {   
            lang = string.IsNullOrEmpty(lang) ? "en" : lang;


            var moviesWithDetails = _db.Movies
                .Select(movie => new
                {
                    MovieId = movie.Id,
                    MovieReleaseYear = movie.ReleaseYear,
                    MovieTitle = lang == "ru" ? movie.TitleRu : movie.TitleEn,
                    MovieDescription = lang == "ru" ? movie.DescriptionRu : movie.DescriptionEn,
                    MovieTrailerLink =  lang == "ru" ? movie.TrailerLinkRu : movie.TrailerLinkEn,
                    Genres = movie.MovieGenres.Select(mg => new
                    {
                        GenreId = mg.Genre.Id,
                        Name = lang == "ru" ? mg.Genre.NameRu : mg.Genre.NameEn,
                    }).ToList(),
                    Countries = movie.MovieCountries.Select(mc => new
                    {
                        CountryID = mc.Country.Id,
                        Name = lang == "ru" ? mc.Country.NameRu : mc.Country.NameEn,
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

        public IActionResult MovieList(int? releaseYear, int? genreId, int? countryId, string? search, string? lang)
        {
            lang = string.IsNullOrEmpty(lang) ? "en" : lang;

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

            if (genreId.HasValue)
            {
                movieListQuery = movieListQuery.Where(m => m.MovieGenres.Any(mg => mg.Genre.Id == genreId));
            }

            if (countryId.HasValue)
            {
                movieListQuery = movieListQuery.Where(m => m.MovieCountries.Any(mc => mc.Country.Id == countryId));
            }

            if (!string.IsNullOrEmpty(search))
            {
                movieListQuery = movieListQuery.Where(m => lang == "ru" ? m.TitleRu.Contains(search) : m.TitleEn.Contains(search));
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
                    Title = lang == "ru" ? movie.TitleRu : movie.TitleEn,
                    Poster = Convert.ToBase64String(movie.MovieImages.FirstOrDefault(mi => mi.IsPoster)?.Image),
                    Description = lang == "ru" ? movie.DescriptionRu : movie.DescriptionEn,
                    ReleaseYear = movie.ReleaseYear,
                    Genres = movie.MovieGenres.Select(mg => new GenreMovieListViewModel
                    {
                    Id = mg.Genre.Id,
                    Name = lang == "ru" ? mg.Genre.NameRu : mg.Genre.NameEn
                    }).ToList(),
                    Countries = movie.MovieCountries.Select(mc => new CountryMovieListViewModel
                    {
                    Id = mc.Country.Id,
                    Name = lang == "ru" ? mc.Country.NameRu : mc.Country.NameEn
                    }).ToList(),
                }).ToList(),

                Genres = _db.Genres.Select(g => new GenreMovieListViewModel
                {
                    Id = g.Id,
                    Name = lang == "ru" ? g.NameRu : g.NameEn
                }).ToList(),
                Countries = _db.Countries.Select(c => new CountryMovieListViewModel
                {
                    Id = c.Id,
                    Name = lang == "ru" ? c.NameRu : c.NameEn
                }).ToList(),
                Years = _db.Movies.Select(m => m.ReleaseYear).Distinct().OrderBy(y => y).ToList()
            };

            return View(viewModel);
        }


        public IActionResult MovieDetails(int? id, string lang = "en")
        {

            lang = string.IsNullOrEmpty(lang) ? "en" : lang;

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
                Title = lang == "ru" ? movie.TitleRu : movie.TitleEn,
                Description = lang == "ru" ? movie.DescriptionRu : movie.DescriptionEn,
                TrailerLink = lang == "ru" ? movie.TrailerLinkRu : movie.TrailerLinkEn,
                Poster = movie.MovieImages.FirstOrDefault(mi => mi.IsPoster) != null 
                    ? Convert.ToBase64String(movie.MovieImages.FirstOrDefault(mi => mi.IsPoster).Image) 
                    : null,
                Genres = movie.MovieGenres.Select(mg => new GenreViewModel
                {
                    Id = mg.Genre.Id,
                    Name = lang == "ru" ? mg.Genre.NameRu : mg.Genre.NameEn
                }).ToList(),
                Countries = movie.MovieCountries.Select(mc => new CountryViewModel
                {
                    Id = mc.Country.Id,
                    Name = lang == "ru" ? mc.Country.NameRu : mc.Country.NameEn
                }).ToList(),
                Participants = movie.MovieParticipants
                    .GroupBy(mp => lang == "ru" ? mp.MovieParticipantCategory.NameRu : mp.MovieParticipantCategory.NameEn)
                    .ToDictionary(
                        grp => grp.Key,
                        grp => grp.Select(mp => new PersonViewModel
                        {
                            Id = mp.Person.Id,
                            BirthDate = mp.Person.BirthDate,
                            FullName = lang == "ru" ? mp.Person.FullNameRu : mp.Person.FullNameEn,
                            ShortBio = lang == "ru" ? mp.Person.ShortBioRu : mp.Person.ShortBioEn,
                        }).ToList()
                    ),
                Images = movie.MovieImages.Where(mi => !mi.IsPoster).Select(mi => new ImageViewModel
                {
                    Id = mi.Id,
                    Image = Convert.ToBase64String(mi.Image)
                }).ToList()
            };
            return View(movieDetails);
        }
    }
}