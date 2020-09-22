using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Owner Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Address { get; set; }
        public int NeighborhoodId { get; set; }
        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
