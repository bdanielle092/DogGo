﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Dog's Name")]
        public string Name { get; set; }
        public int OwnerId { get; set; }
        [Required]
        public string Breed { get; set; }
        public string Notes { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public Owner Owner { get; set; }

    }
}
