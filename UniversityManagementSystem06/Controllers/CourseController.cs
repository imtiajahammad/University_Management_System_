using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem06.BusinessLogics.Course;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.Controllers
{
    public class CourseController : Controller
    {
        CourseManager aCourseManager = new CourseManager();


        // GET: Course
        [HttpGet]
        public ActionResult SaveCourse()
        {
            List<DepartmentModel> departmentList = aCourseManager.GetDepartmentList();
            List<SemesterModel> semesterList = aCourseManager.GetAllSemesters();
            ViewBag.departmentList = departmentList;
            ViewBag.semesterList = semesterList;
            return View();
        }

        [HttpPost]
        public ActionResult SaveCourse(CourseModel aCourseModel)
        {
            string message = "";
            message = aCourseManager.SaveCourse(aCourseModel);
            ViewBag.Message = message;
            //List<string> departmentCodeList = aCourseManager.GetDepartmentList();
            //string[] courseArray = { "1st Semester", "2nd Semester", "3rd Semester", "4th Semester", "5th Semester", "6th Semester", "7th Semester", "8th Semester" };
            // List<string> semesterList = aCourseManager.GetAllSemesters();
            //string[] array ={"1st Semester", "2nd Semester", "3rd Semester", "4th Semester", "5th Semester", "6th Semester", "7th Semester", "8th Semester"};
            //ViewBag.departmentCodeList = departmentCodeList;
            // ViewBag.semesterList = semesterList;
            List<DepartmentModel> departmentList = aCourseManager.GetDepartmentList();
            List<SemesterModel> semesterList = aCourseManager.GetAllSemesters();
            ViewBag.departmentList = departmentList;
            ViewBag.semesterList = semesterList;
            return View();
        }

        public JsonResult GetDepartments()
        {
            List<DepartmentModel> departments = new List<DepartmentModel>();
            departments = aCourseManager.GetDepartmentList();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ViewAllCourses(int? messageFromEdit)
        {
            List<CourseModel> courses = new List<CourseModel>();
            courses = aCourseManager.ViewAllCourses();
            if (courses.Count == 0)
            {
                string message = "No data in the database for courses";
                ViewBag.MessageViewCourses = message;
            }
            else if (messageFromEdit > 0)
            {
                ViewBag.MessageViewCourses = "Courses Updated Successfully";
            }
            ViewBag.CourseList = courses;
            return View();
        }


        [HttpGet]
        public ActionResult EditCourseFromList(int courseId, int? message)
        {
            if (message != null)
            {
                if (message == 0)
                {
                    ViewBag.Message = "Sorry! Course Update Failed !!";
                }
                else if (message == 5)
                {
                    ViewBag.Message = "Sorry! Course Code Exists !!";

                }
                else if (message == 6)
                {
                    ViewBag.Message = "Sorry! Course Name Exists !!";

                }

            }
            CourseModel aCourseModel = new CourseModel();
            aCourseModel = aCourseManager.GetCourseByCourseId(courseId);
            List<DepartmentModel> departments = aCourseManager.GetDepartmentList();


            List<SemesterModel> semesters = aCourseManager.GetAllSemesters();
            ViewBag.Depts = departments;
            ViewBag.Sems = semesters;
            return View(aCourseModel);
        }


        [HttpPost]
        public ActionResult EditCourseFromList(CourseModel aCourseModel)
        {
            //string message = "";
            int rowAffected;
            rowAffected = aCourseManager.UpdateCourse(aCourseModel);

            //ViewBag.Message2 = message;
            if (rowAffected == 1)
            {
                return RedirectToAction("ViewAllCourses", new { message = rowAffected });
            }
            else
            {
                return RedirectToAction("EditCourseFromList", new { courseId = aCourseModel.courseId, message = rowAffected });
            }
        }
        public ActionResult DeleteCourseFromList(int courseId)
        {

            int rowsEffected = aCourseManager.DeleteCourse(courseId);
            if (rowsEffected > 0)
            {
                return RedirectToAction("ViewAllCourses");
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult ViewCourseStatistics()
        {
            List<CourseStatisticsModel> courseStatisticsInfo = new List<CourseStatisticsModel>();
            //courseStatisticsInfo = aCourseManager.;
            if (courseStatisticsInfo.Count == 0)
            {
                string message = "No data in the database for courses";
                ViewBag.MessageViewCourses = message;
            }
            ViewBag.courseStatisticsList = courseStatisticsInfo;
            return View();
        }
    }
}