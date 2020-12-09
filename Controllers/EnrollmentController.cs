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
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public EnrollmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
        {
            return db.Enrollments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollmentById(int id)
        {
            return db.Enrollments.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Enrollment> PostEnrollment(Enrollment model)
        {
            db.Enrollments.Add(model);
            db.SaveChanges();
            return Created($"/api/Enrollment/{model.EnrollmentId}", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutEnrollment(int id, Enrollment model)
        {
            var updateItem = db.Enrollments.Find(id);
            updateItem.Student = model.Student;
            db.Enrollments.Update(updateItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Enrollment> DeleteEnrollmentById(int id)
        {
            var deleteItem = db.Enrollments.Find(id);
            db.Enrollments.Remove(deleteItem);
            db.SaveChanges();
            return Ok(deleteItem);
        }
    }
}