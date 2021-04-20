using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatRoom.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string  Name { get; set; }
        public bool IsFull { get; set; }
        public List<Participant> Participants { get; set; }
        
    }
}
