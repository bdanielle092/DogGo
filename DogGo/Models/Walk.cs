using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walk
    {
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date {get; set;}
        [Required]
        public int Duration { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }

        public Dog Dog { get; set; }
        public Owner Owner { get; set;}
        public Walker walker { get; set; }
    }
}
