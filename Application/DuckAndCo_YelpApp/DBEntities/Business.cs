using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckAndCo_YelpApp.DBEntities
{
    public class Business
    {
        public string bid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double distance { get; set; }
        public double  stars { get; set; }
        public int    reviewCount { get; set; }
        public double  reviewRating { get; set; }
        public int    numCheckins { get; set; }
    }
}
