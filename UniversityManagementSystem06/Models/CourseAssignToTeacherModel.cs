using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem06.Models
{
    public class CourseAssignToTeacherModel
    {
        public int CourseAssignToTeacherId { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }
        //public int TeacherCreditToBeTaken { get; set; }
        //public int TeacherRemainingCredit { get; set; }
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
    }
}