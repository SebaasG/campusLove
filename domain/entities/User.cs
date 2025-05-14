using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.entities
{
    public class User
    {
        public string doc { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public int genderId { get; set; }
        public int cityId { get; set; }
        public int careerId { get; set; }
 
    }
}