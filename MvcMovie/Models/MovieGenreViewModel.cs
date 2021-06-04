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
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }
        public SelectList District { get; set; }

        public SelectList Rating { get; set; }

        public SelectList Price { get; set; }

        public SelectList Floor { get; set; }

       public SelectList ReleaseDate { get; set; }

        
        public string MovieDistrict { get; set; }

        public string MovieRating { get; set; }

        public int MoviePrice { get; set; }

        public int MoviePriceL { get; set; }
        public int MovieFloor { get; set; }

        public string Title { get; set; }

        public string Nstreat { get; set; }

        public int Qf { get; set; }
        public int MovieFloorL { get; set; }

        public int Floors { get; set; }

        public string About { get; set; }

        public string Name { get; set; }

        public string FonNamber { get; set; }

        public List<IFormFile> Foto { get; set; }

        public DateTime MovieReleaseDate { get; set; }
        public string SearchString { get; set; }

        //public IFormFile Avatar { get; set; }





    }

    //public class BufferedSingleFileUploadDbModel : Movie
    //{
   

    //   [BindProperty]
    //    public BufferedSingleFileUploadDb FileUpload { get; set; }

    
    // }

    //public class BufferedSingleFileUploadDb
    //{
    //    [Required]
    //    [Display(Name = "File")]
    //    public IFormFile FormFile { get; set; }
    //}

}
