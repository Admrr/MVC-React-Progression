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
            
            // InsertResults();
            // GetAllResults();
            // GetMovies();
            // GetActors();
            CountMovieActors();

        }

        static void InsertResults() {
            // Inserting entries into our database from here on, creating an instance of our context (MovieContext) with the keyword new (C# syntax & semantics.)
            using (var _context = new MovieContext())
            {
                // Inserting a new entry of Movie
                // Movie m = new Movie {
                //     Title = "Fight Club",
                //     ReleaseYear = "1999",
                //     Actors = new List<Actor> {
                //         new Actor {
                //             Name = "Brad Pitt",
                //             Gender = "Male",
                //             Birthdate = "25-05-1990"
                //         },
                //         new Actor {
                //             Name = "Leonardo di Caprio",
                //             Gender = "Female",
                //             Birthdate = "20-09-2018"
                //         }
                //     }
                // };

                // _context.Movies.Add(m); // add the results from the m variable to the database.
                _context.SaveChanges(); // save the changes.

                // For-loop to read what we have currently in our database.
                foreach (var movie in _context.Movies)
                {
                    Console.WriteLine("Movie: " + movie.Title + ", Release: " + movie.ReleaseYear);
                    foreach (var actor in _context.Actors)
                    {
                        Console.WriteLine("Actor: " + actor.Name + ", Gender: " + actor.Gender + ", Birthdate: " + actor.Birthdate);
                    }
                }
            }
        }

        // PROJECTION
        static void GetMovies() { // For this method, we want to show only the movies, so we need to use a Projection here (select keyword.)
            using (var _context = new MovieContext()) {
                var results = from m in _context.Movies
                            select m;

                foreach (var movie in results)
                    {
                        Console.WriteLine("Movie: " + movie.Title + ", Release: " + movie.ReleaseYear);
                    }
            }   
        }

        static void GetActors() { // For this method, we want to show only the actors, so we need to use a Projection here (select keyword.)
            using (var _context = new MovieContext()) {
                var results = from a in _context.Actors
                            select a;

                foreach (var actor in results)
                    {
                        Console.WriteLine("Actor: " + actor.Name + ", Birthdate: " + actor.Birthdate + ", Gender: " + actor.Gender);
                    }
            }   
        }

    	// JOINING
        static void GetAllResults() { // For this method, we want to show all the results in our database, so we need to use a Join here (join keyword).
            using (var _context = new MovieContext()) {
                var results = from m in _context.Movies
                            join a in _context.Actors on m.Id equals a.MovieId // Explicit Joining: This way of joining the tables is very good for our performance, since it will only load the movies table for now. It will only load in the necessary results from the actor table.
                            select new {
                                Title = m.Title, ReleaseYear = m.ReleaseYear, Actor = a.Name, Birthdate = a.Birthdate, Gender = a.Gender
                            };

                foreach (var movie in results)
                    {
                        Console.WriteLine("Movie: " + movie.Title + ", Release: " + movie.ReleaseYear + ", Actor: " + movie.Actor + ", Birthdate: " + movie.Birthdate + ", Gender: " + movie.Gender);
                    }
            }   
        }

        // SUBQUERY AND AGGREGATION

        static void CountMovieActors() { // For this method, we will use Projection, Join and Aggregation all in one with the help of a subquery and a query. We will show the number of actors in a movie.
            using(var _context = new MovieContext()) {
                var subquery = from m in _context.Movies // Subquery
                                join a in _context.Actors on m.Id equals a.MovieId into Actor // The "on" keyword correlates the attributes of 2 entities in our case, so we will correlate the 2 ID keys (which are our primary keys here.)
                                select new {                                                      
                                    Title = m.Title,
                                    ActorNumber = Actor.Count() // If we don't use the "into" keyword here, we can't use the Count() method on our actors.
                                };
                
                var results = from m in subquery // Query
                            where m.ActorNumber > 1
                            select m;

                foreach (var movie in results)
                    {
                        Console.WriteLine("Movie: " + movie.Title + ", Number of Actors: " + movie.ActorNumber);
                    }
            }
        }
    }
}
