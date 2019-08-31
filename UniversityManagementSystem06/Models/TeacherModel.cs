using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem06.Models
{
    public class TeacherModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherNumber { get; set; }
        public int DesignationId { get; set; }
        public DesignationModel Designation { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
        public int TeacherCreditToBeTaken { get; set; }
    }
}