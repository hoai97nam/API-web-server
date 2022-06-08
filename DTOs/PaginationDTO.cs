using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordPerPage { get; set; }
        private readonly int maxRecordPerPage = 50;
        public int RecordPerPage { 
            get
            {
                return recordPerPage;
            }
            set
            {
                recordPerPage = (value > maxRecordPerPage ? recordPerPage : value);
            }
        }
    }
}
