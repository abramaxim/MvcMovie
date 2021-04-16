using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
//using PhotoCatalogue.WebApp.Models;
//using PhotoCatalogue.WebApp.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }
        public SelectList Genres { get; set; }

        public SelectList Rating { get; set; }

        public SelectList Price { get; set; }

        public SelectList Floor { get; set; }

        public SelectList ReleaseDate { get; set; }


        public string MovieGenre { get; set; }

        public string MovieRating { get; set; }

        public int MoviePrice { get; set; }

        public int MoviePriceL { get; set; }
        public int MovieFloor { get; set; }


        public int MovieFloorL { get; set; }


        public DateTime MovieReleaseDate { get; set; }
        public string SearchString { get; set; }

        //public IFormFile Avatar { get; set; }




    }

}
