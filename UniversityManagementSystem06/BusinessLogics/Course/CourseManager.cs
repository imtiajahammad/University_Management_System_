using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.BusinessLogics.Department;
using UniversityManagementSystem06.BusinessLogics.Semester;
using UniversityManagementSystem06.DataAccessLogics.Course;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.BusinessLogics.Course
{
    public class CourseManager
    {
        CourseGateway aCourseGateway = new CourseGateway();
        //        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        DepartmentManager aDepartmentManager = new DepartmentManager();
        SemesterManager aSemesterManager = new SemesterManager();

        public List<CourseModel> ViewAllCourses()
        {
            List<CourseModel> courses = new List<CourseModel>();
            courses = aCourseGateway.GetAllCourses();
            return courses;
        }

        public List<CourseModel> ViewAllCoursesNotAssignedToTeacher()
        {
            List<CourseModel> courses = new List<CourseModel>();
            courses = aCourseGateway.GetAllCoursesNotAssignedToTeacher();
            return courses;
        }

        public int UpdateCourse(CourseModel aCourseModel)
        {
            {
                int rowAffected = aCourseGateway.UpdateCourse(aCourseModel);
                return rowAffected;
            }


        }

        public List<DepartmentModel> GetDepartmentList()
        {
            List<DepartmentModel> departmentList = new List<DepartmentModel>();
            departmentList = aDepartmentManager.GetAllDepartments();
            return departmentList;
        }
        public List<string> GetCourseNameCourseCode(string courseCode)
        {
            return aCourseGateway.GetCourseNameByCourseCode(courseCode);
        }


        public List<CourseModel> GetCourseListByDeptId(int deptId)
        {
            return aCourseGateway.GetCourseListByDeptId(deptId);
        }
        public List<CourseModel> GetCourseListNotAssignedByDeptId(int deptId)
        {
            return aCourseGateway.GetCourseListNotAssignedByDeptId(deptId);
        }
        

        public string SaveCourse(CourseModel aCourseModel)
        {
            string message = "";
            if (aCourseGateway.IsCourseCodeExist(aCourseModel.courseCode))
            {
                message = "Course Code Exists";
            }
            else if (aCourseGateway.IsCourseNameExist(aCourseModel.courseName))//  aDepartmentGateway.IsDepartmentNameExist(aDepartmetModel.departmentName))
            {
                message = "Course Name Exists";
            }
            else
            {
                int rowAffected = aCourseGateway.SaveCourse(aCourseModel);// aDepartmentGateway.SaveDepartment(aDepartmetModel);
                if (rowAffected > 0)
                {
                    message = "Course Saved Successfully";
                }
                else
                {
                    message = "Sorry! Course Save Failed !!";
                }
            }
            return message;
        }

        public CourseModel GetCourseByCourseId(int courseId)
        {
            CourseModel aCourseModel = new CourseModel();
            aCourseModel = aCourseGateway.GetSingleCourseModel(courseId);
            return aCourseModel;
        }

        public int DeleteCourse(int courseId)
        {
            //          string message = "";

            int rowAffected = aCourseGateway.DeleteCourse(courseId);


            //return message;
            return rowAffected;
        }

        public List<SemesterModel> GetAllSemesters()
        {
            List<SemesterModel> semesters = new List<SemesterModel>();
            semesters = aSemesterManager.GetAllSemesters();
            return semesters;
        }

        public CourseModel GetCourseModelByCourseId(int courseId)
        {
            return aCourseGateway.GetSingleCourseModel(courseId);
        }
        public int GetCourseCreditByCourseId(int courseId)
        {
            return aCourseGateway.GetCourseCreditByCourseId(courseId);
        }
    }
}