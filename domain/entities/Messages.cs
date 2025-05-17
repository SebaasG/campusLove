using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.entities
{
    public class Messages
    {
        public int Id { get; set; }
        public string fromUser { get; set; }
        public string toUser { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
    }
}