﻿using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walk walk { get; set; }
        public Walker walker { get; set; }
        public List <Walk> walks { get; set; }
        
    }
}