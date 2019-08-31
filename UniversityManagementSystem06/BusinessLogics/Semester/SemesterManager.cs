using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.DataAccessLogics.Semester;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.BusinessLogics.Semester
{
    public class SemesterManager
    {
        public List<SemesterModel> GetAllSemesters()
        {
            SemesterGateway aSemesterGateway = new SemesterGateway();
            List<SemesterModel> semesters = new List<SemesterModel>();
            semesters = aSemesterGateway.GetAllSemesters();
            return semesters;
        }

        public SemesterModel GetSemesterById(int semesterId)
        {
            SemesterGateway aSemesterGateway = new SemesterGateway();
            return aSemesterGateway.GetSemesterById(semesterId);
        }
    }
}