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
            
            InsertResults();
            // GetAllResults();
            // GetMovies();
            // GetActors();
            // CountMovieActors();

        }

        static void InsertResults() {
            // Inserting entries into our database from here on, creating an instance of our context (MovieContext) with the keyword new (C# syntax & semantics.)
               using (var _context = new MovieContext())
            {
                // Inserting a new entry of Movie
                Movie m = new Movie
                {
                    Title = "Fight Club",
                    ReleaseYear = "1999",
                    Actors = new List<Actor> {
                        new Actor {
                            Name = "Brad Pitt",
                            Gender = "Male",
                            Birthdate = "25-05-1990"
                        },
                        new Actor {
                            Name = "Leonardo di Caprio",
                            Gender = "Female",
                            Birthdate = "20-09-2018"
                        },
                        new Actor {
                            Name = "Tom Hardy",
                            Gender = "Male",
                            Birthdate = "Male"
                        }
                    }
                };
                Movie m1 = new Movie
                {
                    Title = "Forest Gump",
                    ReleaseYear = "2002",
                    Actors = new List<Actor> {
                        new Actor {
                            Name = "Tom Hanks",
                            Gender = "Male",
                            Birthdate = "12-01-1955"
                        },
                        new Actor {
                            Name = "Lebron James",
                            Gender = "Female",
                            Birthdate = "05-05-2030"
                        }
                    }
                };
                Movie m2 = new Movie {
                    Title = "Fast and the Furious",
                    ReleaseYear = "1997",
                    Actors = new List<Actor> {
                        new Actor {
                            Name = "Paul Walker",
                            Gender = "Male",
                            Birthdate = "25-05-1980"
                        },
                        new Actor {
                            Name = "Vin Diesel",
                            Gender = "Male",
                            Birthdate = "02-03-1972"
                        },
                        new Actor { 
                            Name = "Ahmet Demir",
                            Gender = "Male",
                            Birthdate = "25-05-1997"
                        },
                        new Actor {
                            Name = "Vincent Puyat",
                            Gender = "Male",
                            Birthdate = "07-10-1994"
                        },
                        new Actor {
                            Name = "Rahit Tori",
                            Gender = "Male",
                            Birthdate = "25-10-1995"
                        },
                        new Actor {
                            Name = "Anthony Elikwu",
                            Gender = "Male",
                            Birthdate = "06-03-1996"
                        }
                    }
                };

                // _context.Movies.Add(m); // add the results from the m variable to the database.
                // _context.Movies.Add(m1);
                // _context.Movies.Add(m2);
                // _context.SaveChanges(); // save the changes.

                // For-loop to read what we have currently in our database.
                foreach (var movie in _context.Movies)
                {
                    Console.WriteLine("Movie: {0}, Release: {1}", movie.Title, movie.ReleaseYear);
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
                            join a in _context.Actors on m.Id equals a.MovieId 
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

        static void CountMovieActors() { // For this method, we will use Projection, Filter, Join and Aggregation all in one with the help of a subquery and a query. We will show the number of actors in a movie.
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