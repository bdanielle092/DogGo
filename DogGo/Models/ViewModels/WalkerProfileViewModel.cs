using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walk Walk { get; set; }
        public Walker Walker { get; set; }
        public List <Walk> Walks { get; set; }
        
    }
}
