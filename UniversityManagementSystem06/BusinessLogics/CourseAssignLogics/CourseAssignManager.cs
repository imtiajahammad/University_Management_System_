using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.BusinessLogics.Course;
using UniversityManagementSystem06.BusinessLogics.Department;
using UniversityManagementSystem06.BusinessLogics.Teacher;
using UniversityManagementSystem06.DataAccessLogics.CourseAssignLogics;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.BusinessLogics.CourseAssignLogics
{
    public class CourseAssignManager
    {
        /// <summary>
        /// calling GetAllDepartments()  function from CourseAssignManager;
        /// that GetAllDepartments()  Function calls GetAllDepartments() function From DepartmentManager;
        /// that GetAllDepartments()  Function calls GetAllDepartments() function From DepartmentGateway;
        /// that GetAllDepartments()  gets all the departments with id,names from database;
        /// </summary>
        /// <returns>all the departments with id,names from database.</returns>
        public List<DepartmentModel> GetAllDepartments()
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            List<DepartmentModel> list = aDepartmentManager.GetAllDepartments();
            return list;
        }
        public List<TeacherModel> GetAllTeachers()
        {
            TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aTeacherManager.GetAllTeachers();
        }
        public List<CourseModel> GetAllCourseCodes()
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aCourseManager.ViewAllCourses();
        }

        public List<TeacherModel> GetTeachersByDeptId(int deptId)
        {
            TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aTeacherManager.GetTeachersByDepartmentId(deptId);
        }
        public List<CourseModel> GetCourseListByDeptId(int deptId)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aCourseManager.GetCourseListByDeptId(deptId);
        }
        public int GetCreditByTeacherId(int teacherId)
        {
            TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            int creditToBeTaken = aTeacherManager.GetCreditByTeacherId(teacherId);
            return creditToBeTaken;
        }

        public CourseModel GetCourseModelByCourseId(int courseId)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            CourseModel aCourseModel = aCourseManager.GetCourseModelByCourseId(courseId);
            return aCourseModel;
        }

        /// <summary>
        /// takes id and fetch list of assign credit list 
        /// then uses GetTotalAssignedCreditFromAssignedTeachers method to get total 
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns>total assigned credit</returns>
        public int GetAssignedCreditByTeacherId(int teacherId)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            List<int> courseIdFromAssignedTeachers = new List<int>();
            courseIdFromAssignedTeachers = aCourseAssignGateway.GetAssignedCreditByTeacherId(teacherId);
            int totalAssignedCredit = GetTotalAssignedCreditFromAssignedTeachers(courseIdFromAssignedTeachers);

            return totalAssignedCredit;
        }
        /// <summary>
        /// takes list of integer and returns summation of them
        /// </summary>
        /// <param name="courseIdFromAssignedTeachers"></param>
        /// <returns> "sum of integers in int type"</returns>
        public int GetTotalAssignedCreditFromAssignedTeachers(List<int> courseIdFromAssignedTeachers)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            //error
            //I am making an addition to courseId list
            //imtiaj 
            int total = 0;
            if (courseIdFromAssignedTeachers.Count > 0)
            {
                foreach (int i in courseIdFromAssignedTeachers)
                {
                    total += aCourseManager.GetCourseCreditByCourseId(i);
                }
                return total;
            }
            else
            {
                return 0;
            }

        }
        public bool IsCourseExist(int courseID)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            bool isCourseExists = aCourseAssignGateway.IsCourseExist(courseID);
            return isCourseExists;
        }
        public int DeleteAssignedCourseByCourseAssignId(int AssignedCourseId)
        {
            //          string message = "";
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();

            int rowAffected = aCourseAssignGateway.DeleteAssignedCourseByAssignedCourseId(AssignedCourseId);


            //return message;
            return rowAffected;
        }
        public int UpdateAssignedCourseToTeacherModel(CourseAssignToTeacherModel aCourseAssignToTeacherModel)
        {
            {
                //TeacherManager aTeacherManager = new TeacherManager();
                //DepartmentManager aDepartmentManager = new DepartmentManager();
                //CourseManager aCourseManager = new CourseManager();
                CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
                int rowAffected = aCourseAssignGateway.UpdateCourseAssign(aCourseAssignToTeacherModel);
                return rowAffected;
            }
        }
        public CourseAssignToTeacherModel GetAssignedCourseToTeacherModelById(int assignedCourseId)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            CourseAssignToTeacherModel aCourseAssignToTeacherModel = aCourseAssignGateway.GetSingleCourseAssignToTeacherModel(assignedCourseId);
            return aCourseAssignToTeacherModel;
        }
        public int SaveCourseAssignToTeacher(CourseAssignToTeacherModel courseAssignToTeacherModel)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();

            int input = aCourseAssignGateway.SaveCourseAssignToTeacher(courseAssignToTeacherModel);
            return input;

        }
        public DepartmentModel GetSingleDepartmentByDeptId(int deptId)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aDepartmentManager.GetDepartmentById(deptId);
        }
        public TeacherModel GetSingleTeacherByTeacherId(int TeacherId)
        {
            TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aTeacherManager.GetTeacherByTeacherId(TeacherId);
        }
        public CourseModel GetSingleCourseByCourseId(int CourseId)
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            CourseManager aCourseManager = new CourseManager();
            //CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aCourseManager.GetCourseByCourseId(CourseId);
        }
        public List<CourseAssignToTeacherModel> GetAllCourseAssignedTeachers()
        {
            //TeacherManager aTeacherManager = new TeacherManager();
            //DepartmentManager aDepartmentManager = new DepartmentManager();
            //CourseManager aCourseManager = new CourseManager();
            CourseAssignGateway aCourseAssignGateway = new CourseAssignGateway();
            return aCourseAssignGateway.GetAllCourseAssignedTeachers();
        }
    }
}