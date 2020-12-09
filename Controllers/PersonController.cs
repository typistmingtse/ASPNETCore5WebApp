using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.Person.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Person> PostPerson(Person model)
        {
            model.DateModified = DateTime.Now;
            db.Person.Add(model);
            db.SaveChanges();
            return Created($"/api/Person/{model.Id}",model);
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person model)
        {
            var updateItem = db.Person.Find(id);
            updateItem.FirstName = model.FirstName;
            updateItem.DateModified = DateTime.Now;
            db.Update(updateItem);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
            var deleteItem = db.Person.Find(id);
            db.Person.Remove(deleteItem);
            db.SaveChanges();
            return Ok(deleteItem);
        }
    }
}