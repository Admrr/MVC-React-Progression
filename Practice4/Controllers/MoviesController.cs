using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Practice4.Model;

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
        public IQueryable<Movie> GetMovies(int id)
        {
            var result = from m in this._context.Movies
                         where m.Id == id
                         select m;

            return result;
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
        public IActionResult InsertMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model.");
            }

            var temp = _context.Movies.Where(mo => mo.Id == movie.Id).FirstOrDefault();
            if (temp != null)
            {
                temp.Title = movie.Title;
                temp.ReleaseYear = movie.ReleaseYear;
                temp.Actors = movie.Actors;

                _context.Movies.Add(temp);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
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
