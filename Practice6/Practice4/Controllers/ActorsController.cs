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
    public class ActorsController : Controller
    {
        MovieContext _context;

        public ActorsController(MovieContext context)
        {
            this._context = context;
        }

        // GET api/values
        [HttpGet("GetActors")]
        public IQueryable<Actor> GetActors()
        {
            var result = from a in this._context.Actors
                         select a;

            return result;
        }

        // GET api/values/5
        [HttpGet("GetActors/{id}")]
        public IQueryable<Actor> GetActors(int id)
        {
            var result = from a in this._context.Actors
                         where a.Id == id
                         select a;

            return result;
        }

        // PUT api/values/5
        [HttpPut("EditActor/{id}")]
        public IActionResult EditActor(int id, Actor actor)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model.");
            }

            var temp = _context.Actors.Find(id);
            if (temp == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                temp.Id = actor.Id;
                temp.Name = actor.Name;
                temp.Gender = actor.Gender;
                temp.Birthdate = actor.Birthdate;
                temp.MovieId = actor.MovieId;

                _context.Actors.Update(temp);
                _context.SaveChanges();
            }
            return View(actor); // Here we can also just put an Ok(); method if there is a need for it.
        }

        // POST api/values/5
        [HttpPost("InsertActor/{id}")]
        public IActionResult InsertActor(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model.");
            }

            var temp = _context.Actors.Where(a => a.Id == actor.Id).FirstOrDefault();
            if (temp != null)
            {
                temp.Id = actor.Id;
                temp.Name = actor.Name;
                temp.Gender = actor.Gender;
                temp.Birthdate = actor.Birthdate;
                temp.MovieId = actor.MovieId;

                _context.Actors.Add(temp);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }




        // DELETE api/values/5
        [HttpDelete("DeleteActor/{id}")]
        public IActionResult DeleteActor(int id)
        {
            var temp = _context.Actors.Find(id);
            if (temp == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(temp);
            _context.SaveChanges();
            return Ok();
        }
    }
}
