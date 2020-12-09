using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
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
            return new List<Department> { };
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Course>> GetDepartmentCourses(int id)
        {
            return db.Departments.Include(p => p.Courses)
                .First(p => p.DepartmentId == id).Courses.ToList();  
        }

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(Department model)
        {
            model.DateModified = DateTime.Now;
            // db.Departments.Add(model);
            // db.SaveChanges();
            db.Departments.FromSqlRaw($"EXECUTE dbo.Department_Insert {model.Name}, {model.Budget}, {model.StartDate}, {model.InstructorId}");
            return Created($"/api/Department/{model.DepartmentId}",model);
        }

        [HttpPut("{id}")]
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