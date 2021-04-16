using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [StringLength(60, MinimumLength = 3)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [Required(ErrorMessage = "you need enter Titel")]
        public string Title { get; set; }

       // public int score { get; set; }

        [DataType(DataType.Date)]
        
        public DateTime ReleaseDate { get; set; }
        
        
        [Display(Name = "Ganre")]
        [Required(ErrorMessage = "you need enter Ganre")]
       // [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        
        [StringLength(30)]

        public string Genre { get; set; }
        //[Column(TypeName = "decimal(18000, 2)")]


        //[DataType(DataType.Currency)]
        public int Price { get; set; }



        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(20)]
        [Required]
        public string Rating { get; set; }

       public int Floor { get; set; }

       //public int FonNamber { get; set; }

       //public string About { get; set; }
      // public string Nstreat { get; set; }


       // public IFormFile Avatar { get; set; }
    }
   
}
