﻿using System;
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

                // _context.Movies.Add(m);
                _context.SaveChanges();

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
    }
}
