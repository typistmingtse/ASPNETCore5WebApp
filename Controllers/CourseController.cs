using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly Course course;
        private readonly ContosoUniversityContext dbContext;
        public CourseController(ContosoUniversityContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return dbContext.Courses.Where(z => z.IsDeleted == false).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id) 
        {
            return dbContext.Courses.FirstOrDefault(z => z.CourseId == id && z.IsDeleted == false);
        }

        [HttpGet("/credits/{credit}")]
        public ActionResult<IEnumerable<Course>> GetCourseByCredit(int credit)
        {
            return dbContext.Courses.Where(z => z.Credits == credit && z.IsDeleted == false).ToList();
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course model)
        {
            // model.DateModified = DateTime.Now;
            if (dbContext.Courses.Find(model.CourseId) == null)
            {
                dbContext.Courses.Add(model);
            }
            else
            {
                model.IsDeleted = false;
                dbContext.Courses.Update(model);
            }
            
            EntityEntry entityEntry = dbContext.Entry(model);
            if(entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
            {
                model.DateModified = DateTime.Now;
            }
            dbContext.SaveChanges();
            return Created($"/api/Course/{model.CourseId}", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, Course model)
        {
            var updateItem = dbContext.Courses.FirstOrDefault(z => z.CourseId == id && z.IsDeleted == false);
            updateItem.Title = model.Title;
            updateItem.Credits = model.Credits;
            // updateItem.DateModified = DateTime.Now;
            dbContext.Update(updateItem);
            EntityEntry entityEntry = dbContext.Entry(updateItem);
            if(entityEntry.State == EntityState.Modified){
                updateItem.DateModified = DateTime.Now;
            }
            dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseById(int id)
        {
            var delItem = dbContext.Courses.Find(id);
            delItem.IsDeleted = true;
            dbContext.Courses.Update(delItem);
            delItem.DateModified = DateTime.Now;
            dbContext.SaveChanges();
            return Ok(delItem);
        }

        [HttpGet("VwCourseStudent")]
        public ActionResult<IEnumerable<VwCourseStudent>> GetVwCourseStudent()
        {
            return dbContext.VwCourseStudents.ToList();
        }
        
        [HttpGet("VwCourseStudentCount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetVwCourseStudentCount()
        {
            return dbContext.VwCourseStudentCounts.ToList();
        }
        
        [HttpGet("VwDepartmentCourseCount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetVwDepartmentCourseCount()
        {
            return dbContext.VwDepartmentCourseCounts.FromSqlRaw("select DepartmentID, Name, CourseCount from VwDepartmentCourseCount").ToList();
        }
    }
}