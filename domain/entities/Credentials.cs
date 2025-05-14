using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.entities
{
    public class Credentials
    {
        public int id { get; set; }
        public string docUser { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}