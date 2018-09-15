using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using Practice1.Model;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Inserting entries into our database from here on, creating an instance of our context (MovieContext) with the keyword new (C# syntax & semantics.)
            using(var _context = new MovieContext()) {
                // Inserting a new entry of Movie
                    // MovieContext.Movie m = new MovieContext.Movie {
                    //     Title = "Fight Club",
                    //     ReleaseYear = "1999",
                    // };

                // _context.Movies.Add(m);
                // _context.SaveChanges();

                // For-loop to read what we have currently in our database.
                foreach(var movie in _context.Movies) {
                    Console.WriteLine("Movie: " + movie.Title + ", Release: " + movie.ReleaseYear);
                }
            }
        }
    }
}
