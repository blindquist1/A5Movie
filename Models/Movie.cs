using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5Movie.Models
{
    //Class to keep track of a single movie
    public class Movie
    {
        //Properties
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
    }
}
