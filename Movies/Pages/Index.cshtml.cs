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

        public void OnGet()
        {
            SearchTerms = Request.Query["SearchTerms"];
            Movies = MovieDatabase.Search(SearchTerms);
        }
    }
}
