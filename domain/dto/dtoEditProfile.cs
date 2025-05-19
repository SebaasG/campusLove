using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.dto
{
    public class dtoEditProfile
    {
        public int genderId { get; set; }
        public int cityId { get; set; }
        public int careerId { get; set; }
        public string phrase { get; set; }
        public Boolean isActive { get; set; }
        public int interestId { get; set; }

    }
}