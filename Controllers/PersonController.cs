using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
            return db.Person.Where(z => z.IsDeleted == false).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.FirstOrDefault(z => z.Id == id && z.IsDeleted == false);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Person> PostPerson(Person model)
        {
            // model.DateModified = DateTime.Now;
            if(db.Person.FirstOrDefault(z => z.Id == model.Id && z.IsDeleted == true) == null)
            {
                db.Person.Add(model);
            }
            else
            {
                model.IsDeleted = false;
                db.Person.Update(model);
            }

            EntityEntry entityEntry = db.Entry(model);
            if(entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified) 
            {
                model.DateModified = DateTime.Now;
            }
            db.SaveChanges();
            return Created($"/api/Person/{model.Id}",model);
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person model)
        {
            var updateItem = db.Person.FirstOrDefault(z => z.Id == id && z.IsDeleted == false);
            updateItem.FirstName = model.FirstName;
            // updateItem.DateModified = DateTime.Now;
            db.Update(updateItem);
            EntityEntry entityEntry = db.Entry(model);
            if(entityEntry.State == EntityState.Modified)
            {
                model.DateModified = DateTime.Now;
            }
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
            var deleteItem = db.Person.Find(id);
            deleteItem.IsDeleted = true;
            db.Person.Update(deleteItem);
            db.SaveChanges();
            return Ok(deleteItem);
        }
    }
}