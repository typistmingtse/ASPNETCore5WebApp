﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ASPNETCore5Demo.Models
{
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Person Instructor { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
