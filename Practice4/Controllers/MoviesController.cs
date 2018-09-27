using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice4.Model;

namespace Practice4.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        MovieContext _context;

        public MoviesController(MovieContext context) {
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
        public IQueryable GetMovies(int id)
        {
            var result = from m in this._context.Movies
                        where m.Id == id
                        select m;

            return result;
        }

        // POST api/values
        [HttpPost("InsertMovie")]
        public IQueryable InsertMovie() 
        {
            var result = "result"; // TODO: Insert a movie here (maybe with some actors?) with a LINQ query.

            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
