using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckAndCo_YelpApp.DBEntities
{
    public class Review
    {
        public string userName { get; set; }
        public string businessName { get; set; }
        public string businessCity { get; set; }
        public string text { get; set; }
        public string date { get; set; }
    }
}
