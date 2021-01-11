using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.AspNetCore.Http;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseInstructorController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInsructors()
        {
            return db.CourseInstructors.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int id)
        {
            return db.CourseInstructors.Find(id);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<CourseInstructor> PostCourseInstructor(CourseInstructor model)
        {

            db.CourseInstructors.Add(model);
            db.SaveChanges();
            return Created($"/api/CourseInstructor/{model.InstructorId}", model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult PutCourseInstructor(int id, CourseInstructor model)
        {
            var updateItem = db.CourseInstructors.Find(id);
            updateItem.Instructor = model.Instructor;
            db.CourseInstructors.Update(updateItem);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int id)
        {
            var deleteItem = db.Courses.Find(id);
            db.Courses.Remove(deleteItem);
            db.SaveChanges();
            return Ok(deleteItem);
        }
    }
}