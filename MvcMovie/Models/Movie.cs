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
using Microsoft.AspNetCore.Mvc;

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


        [Display(Name = "District")]
        [Required(ErrorMessage = "you need enter District")]
        // [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]

        [StringLength(30)]

        public string District { get; set; }
        //[Column(TypeName = "decimal(18000, 2)")]

        public string Ganre { get; set; }
        //[DataType(DataType.Currency)]
        public int Price { get; set; }



        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(20)]
        [Required]
        public string Rating { get; set; }


        public int Qf { get; set; }
        public int Floor { get; set; }
        public int Floors { get; set; }



        public string FonNamber { get; set; }


        public string Name { get; set; }

        public string About { get; set; }

        public string GK { get; set; }
        public string Nstreat { get; set; }





        public byte[] Foto { get; set; }
        public byte[] Foto1 { get; set; }

        public byte[] Foto2 { get; set; }

        // public byte[]

        // public IFormFile Foto2 { get; set; }
    }

   
}
