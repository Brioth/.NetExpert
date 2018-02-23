using System;
using System.Collections.Generic;
using System.Text;

namespace Eventaris.Domain
{
    public class Participation
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId  { get; set; }
        public Event Event { get; set; }
    }
}
