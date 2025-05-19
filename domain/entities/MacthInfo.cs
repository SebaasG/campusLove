using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.entities
{
    public class MacthInfo
    {
          public string MatchedUserDoc { get; set; }
        public string MatchedUserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}