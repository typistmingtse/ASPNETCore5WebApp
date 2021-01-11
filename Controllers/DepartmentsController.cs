using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ASPNETCore5Demo.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public DepartmentsController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Departments.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<Course>> GetDepartmentCourses(int id)
        {
            if(db.Departments.Include(p => p.Courses)
                .First(p => p.DepartmentId == id).Courses.ToList().Count == 0)
            {
                return NoContent();
            }

            return db.Departments.Include(p => p.Courses)
                .First(p => p.DepartmentId == id).Courses.ToList();  
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Department> PostDepartment(Department model)
        {
            model.DateModified = DateTime.Now;
            // db.Departments.Add(model);
            // db.SaveChanges();
            db.Departments.FromSqlRaw($"EXECUTE dbo.Department_Insert {model.Name}, {model.Budget}, {model.StartDate}, {model.InstructorId}");
            return Created($"/api/Department/{model.DepartmentId}",model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult PutDepartment(int id, Department model)
        {
             var updateItem = db.Departments.Find(id);
             updateItem.InstructorId = model.InstructorId;
             updateItem.DateModified = DateTime.Now;
            // db.SaveChanges();
            db.Departments.FromSqlRaw($"EXECUTE dbo.Department_Update {updateItem.DepartmentId}, {updateItem.Name}, {updateItem.Budget}, {updateItem.StartDate}, {updateItem.InstructorId}, {updateItem.RowVersion}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
        {
            var delItem = db.Departments.Find(id);
            // db.Departments.Remove(delItem);
            // db.SaveChanges();
            db.Departments.FromSqlRaw($"EXECUTE dbo.Department_Delete {delItem.DepartmentId}, {delItem.RowVersion}");
            return Ok(delItem);
        }
    }
}