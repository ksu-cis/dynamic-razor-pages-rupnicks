using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// Gets and sets the search terms
        /// </summary>
        public string SearchTerms { get; set; }

        /// <summary>
        /// Gets and sets the MPAA rating filters
        /// </summary>
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered Genres
        /// </summary>
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets and sets the IMDB minimium rating
        /// </summary>
        public double? IMDBMin { get; set; }

        /// <summary>
        /// Gets and sets the IMDB maximum rating
        /// </summary>
        public double? IMDBMax { get; set; }

        public double? RottenTomatoesMin { get; set; }

        public double? RottenTomatoesMax { get; set; }

        /// <summary>
        /// Does the response initialization for incoming GET requests
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, double? RottenTomatoesMin, double? RottenTomatoesMax)
        {
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            // Nullable conversion workaround
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RottenTomatoesMax = RottenTomatoesMax;
            this.RottenTomatoesMin = RottenTomatoesMin;

            Movies = MovieDatabase.All;
            //Search movie titles for search terms
            if(SearchTerms != null)
            {
                //Movies = MovieDatabase.All.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
                Movies = from movie in Movies
                         where movie.Title != null &&
                         movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase)
                         select movie;
            }
            //Filter by MPAARating
            if(MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie =>
                movie.MPAARating != null &&
                MPAARatings.Contains(movie.MPAARating)
                );
            }
            //Filter by Genre
            if(Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                movie.MajorGenre != null &&
                Genres.Contains(movie.MajorGenre)
                );
            }
            //Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            //Filter by IMDBRating
            if(IMDBMin != null)
            {
                Movies = Movies.Where(movie =>
                movie.IMDBRating >= IMDBMin);
            }
            if(IMDBMax != null)
            {
                Movies = Movies.Where(movie =>
                movie.IMDBRating <= IMDBMax);
            }
            //Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            //Filter by Rotten Tomatoes
            if(RottenTomatoesMax != null)
            {
                Movies = Movies.Where(movie =>
                movie.RottenTomatoesRating <= RottenTomatoesMax);
            }
            if(RottenTomatoesMin != null)
            {
                Movies = Movies.Where(movie =>
                movie.RottenTomatoesRating >= RottenTomatoesMin);
            }
            //Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenTomatoesMin, RottenTomatoesMax);
        }
    }
}
