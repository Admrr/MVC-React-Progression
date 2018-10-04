using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Practice4.Model;
using Practice4.Paginator;

// I have used these websites to study the documentation.
// Post: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-2.1
// Put: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.1
// Delete: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.1

namespace Practice4.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        MovieContext _context;

        public MoviesController(MovieContext context)
        {
            this._context = context;

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
                Movie m2 = new Movie
                {
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


            }
        }

        // GET api/values
        [HttpGet("GetMovies")]
        public IQueryable<Movie> GetMovies()
        {
            var result = from m in this._context.Movies
                         select m;

            return result;
        }

        // GET api/values/5
        [HttpGet("GetMovies/{id}")]
        public IQueryable<Movie> GetMoviesID(int id)
        {
            // This query gets all the movies and the respective actors.
            var result = from m in this._context.Movies
                         where m.Id == id
                         join a in _context.Actors on m.Id equals a.MovieId into Actor
                         select new Movie
                         {
                             Id = m.Id,
                             Title = m.Title,
                             ReleaseYear = m.ReleaseYear,
                             Actors = Actor.ToList()
                         };

            // This query only gets the movies.
            // var result = from m in this._context.Movies
            //              where m.Id == id
            //              select m;

            return result;
        }

        // GET api/movies/GetMoviesPaged/1/3
        [HttpGet("GetMoviesPaged/{index_page}/{page_size}")]
        public IActionResult GetMoviesPaged(int index_page, int page_size) {
            var result = _context.Movies.Paginator<Movie>(index_page, page_size, m => m.Id);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("EditMovie/{id}")]
        public IActionResult EditMovie(int id, Movie movie)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model.");
            }

            var temp = _context.Movies.Find(id);
            if (temp == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                temp.Id = movie.Id;
                temp.Title = movie.Title;
                temp.ReleaseYear = movie.ReleaseYear;
                temp.Actors = movie.Actors;

                _context.Movies.Update(temp);
                _context.SaveChanges();
            }
            return View(movie);
        }

        // POST api/values/5
        [HttpPost("InsertMovie/{id}")]
        public IActionResult InsertMovie(int id, [FromBody] Movie movie)
        {
            if (movie != null)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
            }

            // if (!ModelState.IsValid)
            // {
            //     return BadRequest("Not a valid model.");
            // }

            // var temp = _context.Movies.Where(mo => mo.Id == movie.Id).FirstOrDefault();
            // if (temp != null)
            // {
            //     temp.Title = movie.Title;
            //     temp.ReleaseYear = movie.ReleaseYear;
            //     temp.Actors = movie.Actors;

            //     _context.Movies.Add(temp);
            //     _context.SaveChanges();
            // }
            // else
            // {
            //     return NotFound();
            // }
            return NoContent();
        }




        // DELETE api/values/5
        [HttpDelete("DeleteMovie/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var temp = _context.Movies.Find(id);
            if (temp == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(temp);
            _context.SaveChanges();
            return Ok();
        }
    }
}
