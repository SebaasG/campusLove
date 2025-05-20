using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.entities
{
    public class Stats
    {
       public string UserId { get; set; } 
        public int LikesReceived { get; set; } = 0;
        public int LikesGiven { get; set; } = 0;
        public int MatchesCount { get; set; } = 0;
        public DateTime? LastLikeDate { get; set; }
    }
}