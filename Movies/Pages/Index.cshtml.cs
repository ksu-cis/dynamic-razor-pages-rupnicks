using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The current search terms
        /// </summary>
        public string SearchTerms { get; set; }

        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        public string[] MPAARatings { get; set; }

        public string[] Genres { get; set; }

        public void OnGet()
        {
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
        }
    }
}
