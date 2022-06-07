using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class PersonPatchDTO
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string Biograhpy { get; set; }
        public DateTime DateofBirth { get; set; }
    }
}
