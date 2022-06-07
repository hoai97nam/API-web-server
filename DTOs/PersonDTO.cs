using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biograhpy { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Picture { get; set; }
    }
}
