using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

using System;

using System.Diagnostics;


using Microsoft.Extensions.Logging;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using MvcMovie.Controllers;



namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }
        /*
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }
        */
        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string movieRating, int moviePrice, int moviePriceL, 
            int movieFloor,int movieFloorL, DateTime movieReleaseDate,  string searchString, string sortOrder)
        {
            


            //var students = from s in _context.Students
            //               select s;
           




            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            IQueryable<string> ratingQuery = from m in _context.Movie
                                            orderby m.Rating
                                            select m.Rating;


            IQueryable<int> floorQuery = from m in _context.Movie
                                             orderby m.Floor
                                             select m.Floor;



           IQueryable<DateTime> ReleaseDateQuerty = from m in _context.Movie
                                                    orderby m.ReleaseDate
                                                    select m.ReleaseDate;






            IQueryable<int> priceQuery = from m in _context.Movie
                                            orderby m.Price
                                            select m.Price;


            var movies = from m in _context.Movie
                         select m;

            //sort tach url

           
      




            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            if (!string.IsNullOrEmpty(movieRating))
            {
                movies = movies.Where(x => x.Rating == movieRating);
            }


            if (movieFloor!=0 || movieFloorL != 0)
            {
                movies = movies.Where(x => x.Floor >= movieFloor&& x.Floor <=movieFloorL );
            }





            if (moviePrice != 0 & moviePriceL != 0)
            {
                movies = movies.Where(x => x.Price >= moviePrice&& x.Price<= moviePriceL);
            }

            if (moviePrice != 0 & moviePriceL== 0)
            {
                movies = movies.Where(x => x.Price >= moviePrice );
            }

            if (moviePrice == 0 & moviePriceL != 0)
            {
                movies = movies.Where(x => x.Price <= moviePriceL);
            }







            if (movieReleaseDate!= DateTime.MinValue)
            {
                movies = movies.Where(x => x.ReleaseDate == movieReleaseDate);
            }


            //sort by clic colum


            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            ViewData["GanreSortParm"] = sortOrder == "Ganre" ? "ganre_desc" : "Ganre";

            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";



            switch (sortOrder)
            {
                case "name_desc":
                   
                    movies = movies.OrderByDescending(m => m.Title);
                    break;

                case "Price":
                    movies = movies.OrderBy(m => m.Price);
                    break;

                case "price_desc":
                    movies = movies.OrderByDescending(m => m.Price);
                    break;

                case "Date":
                    movies = movies.OrderBy(m => m.ReleaseDate);
                    break;

                case "date_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseDate);
                    break;

                case "Ganre":
                    movies = movies.OrderBy(m => m.Genre);
                    break;

                case "ganre_desc":
                    movies = movies.OrderByDescending(m => m.Genre);
                    break;





                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }





            /*
            var moviesP = from n in _context.Movie
                         select n;
            if (moviePrice!=0)
            {
                moviesP = moviesP.Where(x => x.Price == moviePrice);
            }
            */


            var movieFilterVM = new MovieGenreViewModel

            //var movieRatingVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),

                Rating = new SelectList(await ratingQuery.Distinct().AsNoTracking().ToListAsync()),

                Price = new SelectList(await priceQuery.Distinct().ToListAsync()),

                Floor = new SelectList(await floorQuery.Distinct().ToListAsync()),

                ReleaseDate = new SelectList(await ReleaseDateQuerty.Distinct().ToListAsync()),

                //Movies = await movies.ToListAsync().AsNoTracking())


                Movies =await movies.AsNoTracking().ToListAsync()
        };

            return View(movieFilterVM);

            //return View(movieRatingVM);

        }

        /*
        public async Task<IActionResult> Index( string movieRating)
        {
            // Use LINQ to get list of genres.
            

            IQueryable<string> ratingQuery = from m in _context.Movie
                                             orderby m.Rating
                                             select m.Rating;


            var movies = from m in _context.Movie
                         select m;


            if (!string.IsNullOrEmpty(movieRating))
            {
                movies = movies.Where(x => x.Genre == movieRating);
            }



            //
            var movieRatingVM = new MovieGenreViewModel
            {
                Rating = new SelectList(await ratingQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieRatingVM);


        }




        */




        // GET: Movies
        /*
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }
        */
        // GET: Movies/Details/5


        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating,Floor")] Movie movie)
        {
            if (ModelState.IsValid)
            {
               

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating,Fllor")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /*
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        */

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
