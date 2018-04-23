using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckAndCo_YelpApp.DBEntities
{
    public class Review
    {
        public string bid { get; set; }
        public string userName { get; set; }
        public string businessName { get; set; }
        public string businessCity { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public int    stars { get; set; }
        public int    funny { get; set; }
        public int    useful { get; set; }
        public int    cool { get; set; }
    }
}
