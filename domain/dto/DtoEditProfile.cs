using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.dto
{
    public class DtoEditProfile
    {
        public string Doc { get; set; } 
        public int? GenderId { get; set; }
        public int? CityId { get; set; }
        public int? CareerId { get; set; }

        // Profiles
        public string Phrase { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
    }
}