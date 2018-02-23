using System;
using System.Collections.Generic;
using System.Text;

namespace Eventaris.DAL
{
    internal class ResultPage<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IList<T> Results { get; set; }
    }
}
