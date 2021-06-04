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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


using System.Web;
//using System.W;



using System.IO;




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
        public async Task<IActionResult> Index(string movieDistrict, string movieRating, int moviePrice, int moviePriceL, 
            int movieFloor,int movieFloorL, DateTime movieReleaseDate,  string searchString, string sortOrder)
        {
            


            //var students = from s in _context.Students
            //               select s;
           




            // Use LINQ to get list of genres.
            IQueryable<string> districtQuery = from m in _context.Movie
                                            orderby m.District
                                            select m.District;

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


            IQueryable<string> fonNamberQuery = from m in _context.Movie
                                                orderby m.FonNamber
                                                select m.FonNamber;

            var movies = from m in _context.Movie
                         select m;

           

           
      




            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Title.Contains(searchString)
                                         || m.District.Contains(searchString)
                                         || m.FonNamber.Contains(searchString)
                                      );

                //movies = movies.Where(m => m.District.Contains(searchString)
                //                       || m.Title.Contains(searchString)
                //                       || m.Name.Contains(searchString)
                //                       || m.FonNamber.ToString().Contains(searchString)

                //                       );


            }

            /*
             
            || s.FonNamber.Contains(searchString)

             if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            */

            if (!string.IsNullOrEmpty(movieDistrict))
            {
                movies = movies.Where(x => x.District == movieDistrict);
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

            //sort tach url
            //sort by clic colum


            // ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewData["StritSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            ViewData["DistrictSortParm"] = sortOrder == "District" ? "district_desc" : "District";

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

                case "District":
                    movies = movies.OrderBy(m => m.District);
                    break;

                case "district_desc":
                    movies = movies.OrderByDescending(m => m.District);
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
                District = new SelectList(await districtQuery.Distinct().ToListAsync()),

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
        public IActionResult CreateF()
        {
            return View();
        }

        public IActionResult CreateK()
        {
            return View();
        }
        






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateF([Bind("Id,Title, Foto")] Movie movie, MovieGenreViewModel movieGenreViewModel, IFormFile Foto)


        {




            //if (ModelState.IsValid)
            //{

                if (movieGenreViewModel.Foto[1] != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(movieGenreViewModel.Foto[1].OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)movieGenreViewModel.Foto[1].Length);
                    }
                    // установка массива байтов
                    movie.Foto = imageData;
                }

                //if (Foto != null)

                //{
                //    if (Foto.Length > 0)

                //    //Convert Image to byte and save to database

                //    {

                //        byte[] p1 = null;
                //        using (var fs1 = Foto.OpenReadStream())
                //        using (var ms1 = new MemoryStream())
                //        {
                //            fs1.CopyTo(ms1);
                //            p1 = ms1.ToArray();
                //        }
                //        movie.Foto = p1;

                //    }
                //}






                // byte[] imageData = null;
                // // считываем переданный файл в массив байтов
                // using (var binaryReader = new BinaryReader(Foto.OpenReadStream()))
                // {
                //     imageData = binaryReader.ReadBytes((int)Foto.Length);
                // }
                // // установка массива байтов
                //movie.Foto = imageData;

                //using (var ms = new MemoryStream())
                //{
                //    Foto.CopyTo(ms);
                //    movie.Foto = ms.ToArray();
                //}


                _context.Add(movie);
                //_context.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

           // return View(movie);
        }




        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,District,Price,Rating,Floor,Name," +
            "FonNamber,Floors,Qf,About, Name,Nstreat,Foto")] Movie movie ,MovieGenreViewModel movieGenreViewModel)
        {




            if (ModelState.IsValid)
            {
                //if (movieGenreViewModel.Foto == null || movieGenreViewModel.Foto[0].Length == 0)
                //if (movieGenreViewModel.Foto == null)
                //{

                //    byte[] p1 = null;


                //    movie.Foto = p1;

                //    return View(movie);
                //}
                //if (movieGenreViewModel.Foto[1] == null)
                //{

                //    byte[] p1 = null;


                //    movie.Foto1 = p1;

                //}
                //if (movieGenreViewModel.Foto[2] == null)
                //{

                //    byte[] p1 = null;


                //    movie.Foto2 = p1;

                //}

                ////return View(movie);;
                //if (movieGenreViewModel.Foto[1] == null || movieGenreViewModel.Foto[1].Length == 0)
                //{

                //    byte[] p2 = null;


                //    movie.Foto1 = p2;

                //}


                //if (movieGenreViewModel.Foto[2] == null || movieGenreViewModel.Foto[0].Length == 0)
                //{

                //    byte[] p3 = null;


                //    movie.Foto2 = p3;

                //}

                //List<IFormFile> Foto = new List<IFormFile>();


                //var F = movieGenreViewModel?.Foto[0];
                //var F1 = movieGenreViewModel?.Foto[1];
                //var F2 = movieGenreViewModel?.Foto[2];


                //var F = movie?.Foto;
                //var F1 = movie?.Foto;
                //var F2 = movie?.Foto;


                //var F = movieGenreViewModel?.Foto;
                //var F1 = movieGenreViewModel?.Foto;
                //var F2 = movieGenreViewModel?.Foto;

                for (int i = 0; i < movieGenreViewModel?.Foto?.Count; i++)
                {
                    //if (movieGenreViewModel?.Foto[i] != null )

                    {


                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(movieGenreViewModel.Foto[i].OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)movieGenreViewModel.Foto[i].Length);
                        }
                        // установка массива байтов

                        if (i == 0)
                        {
                            movie.Foto = imageData;
                        }

                        if (i == 1)
                        {
                            movie.Foto1 = imageData;
                        }

                        if (i == 2)
                        {
                            movie.Foto2 = imageData;
                        }

                    }
                }


                /*

                if (movieGenreViewModel?.Foto[0] != null && movieGenreViewModel?.Foto!=null)                //if (F!=null)


                {


                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(movieGenreViewModel.Foto[0].OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)movieGenreViewModel.Foto[0].Length);
                    }
                    // установка массива байтов
                    movie.Foto = imageData;

                }


                
                if (movieGenreViewModel?.Foto[1] != null)

               // if (F1 != null)
                {


                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(movieGenreViewModel.Foto[1].OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)movieGenreViewModel.Foto[1].Length);
                    }
                    // установка массива байтов
                    movie.Foto1 = imageData;

                }
                //if (movieGenreViewModel?.Foto[2]!=null )
                //{

                //    movie.Foto2 = null;

                //}


                if (movieGenreViewModel?.Foto[2] != null || movieGenreViewModel.Foto[2].Length != 0)

                //if (F2 != null)
                {


                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(movieGenreViewModel.Foto[2].OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)movieGenreViewModel.Foto[2].Length);
                    }
                    // установка массива байтов
                    movie.Foto2 = imageData;

                }

                else
                {

                    movie.Foto2 = null;

                }

                */

                //else
                //{
                //    //does not update the image
                //}


                //if (movieGenreViewModel.Foto[2] == null || movieGenreViewModel.Foto[2].Length == 0)
                //{
                //    ModelState.AddModelError("", "No file selected");
                //    return View(movie);
                //}

                //if (movieGenreViewModel.Foto[2].Length < 0)

                //{



                //    byte[] p1 = null;


                //    movie.Foto2 = p1;



                //}
                //if (Foto != null)

                //{
                //    if (Foto.Length > 0)

                //    //Convert Image to byte and save to database

                //    {

                //        byte[] p1 = null;
                //        using (var fs1 = Foto.OpenReadStream())
                //        using (var ms1 = new MemoryStream())
                //        {
                //            fs1.CopyTo(ms1);
                //            p1 = ms1.ToArray();
                //        }
                //        movie.Foto = p1;

                //    }
                //}






                // byte[] imageData = null;
                // // считываем переданный файл в массив байтов
                // using (var binaryReader = new BinaryReader(Foto.OpenReadStream()))
                // {
                //     imageData = binaryReader.ReadBytes((int)Foto.Length);
                // }
                // // установка массива байтов
                //movie.Foto = imageData;

                //using (var ms = new MemoryStream())
                //{
                //    Foto.CopyTo(ms);
                //    movie.Foto = ms.ToArray();
                //}


                _context.Add(movie);
                //_context.SaveChanges();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReleaseDate,District,Title,Nstreat,Rating,Qf,Floor,Floors" +
            " Price,About,Name,FonNamber")] Movie movie)

        //int id, [Bind("Id,Title,ReleaseDate,District,Price,Rating,Fllor")] Movie movie)
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
