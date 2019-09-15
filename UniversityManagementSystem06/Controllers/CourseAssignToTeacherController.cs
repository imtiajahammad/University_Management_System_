using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem06.BusinessLogics.CourseAssignLogics;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.Controllers
{
    public class CourseAssignToTeacherController : Controller
    {
        // GET: CourseAssignToTeacher

        /// <summary>
        /// show all courseAssignedToTeachers 
        /// </summary>
        /// <param name="messageFromEdit">after saving or updating, this controller is been called. so the save/update message can be sent as parameter if i want</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewAllAssignedTeachers(int? messageFromEdit)
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            List<CourseAssignToTeacherModel> CourseAssignToTeachers = new List<CourseAssignToTeacherModel>();
            CourseAssignToTeachers = aCourseAssignManager.GetAllCourseAssignedTeachers();
            //return View(CourseAssignToTeachers);
            if (CourseAssignToTeachers.Count == 0)
            {
                string message = "No data in the database for AssignedTeacher";
                ViewBag.MessageViewCourses = message;
            }
            else if (messageFromEdit > 0)
            {
                ViewBag.MessageViewCourses = "AssignedTeacher Updated Successfully";
            }
            ViewBag.CourseAssignToTeacherModelList = CourseAssignToTeachers;
            
            return View();
        }
        //public ActionResult ViewAllCourses(int? messageFromEdit)
        //{
        //    List<CourseModel> courses = new List<CourseModel>();
        //    courses = aCourseAssignManager.ViewAllCourses();
        //    if (courses.Count == 0)
        //    {
        //        string message = "No data in the database for courses";
        //        ViewBag.MessageViewCourses = message;
        //    }
        //    else if (messageFromEdit > 0)
        //    {
        //        ViewBag.MessageViewCourses = "Courses Updated Successfully";
        //    }
        //    ViewBag.CourseList = courses;
        //    return View();
        //}

        [HttpGet]
        public ActionResult AssignTeacher()
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            ViewBag.Departments = aCourseAssignManager.GetAllDepartments();
            ViewBag.Teachers = aCourseAssignManager.GetAllTeachers();
            ViewBag.CourseCodeList = aCourseAssignManager.GetAllCourseCodes();
            return View();
        }
        [HttpPost]
        public ActionResult AssignTeacher(CourseAssignToTeacherModel courseAssignToTeacherModel)
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            ViewBag.Departments = aCourseAssignManager.GetAllDepartments();
            ViewBag.Teachers = aCourseAssignManager.GetAllTeachers();
            ViewBag.CourseCodeList = aCourseAssignManager.GetAllCourseCodes();
            //do the code to store in the database
            if (aCourseAssignManager.IsCourseExist(courseAssignToTeacherModel.CourseId))
            {
                ViewBag.Message = ("Course is already assigned");
            }
            else
            {
                int stored = aCourseAssignManager.SaveCourseAssignToTeacher(courseAssignToTeacherModel);
                if (stored == 1)
                {
                    ViewBag.Message = ("Course Assign to Teacher done successfully");
                }
                else
                {
                    ViewBag.Message = ("Course Assign to Teacher Error");
                }
            }

            return View();

            /* 
             1. one course should be assigned to one teacher only-done
             2. if teacher credit exceed, an dialog box with yes/no pop up and work accordingly
             3. remaining credit returning false count--done
             */
        }



        /// <summary>
        /// to get teacher list from Dept id
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public JsonResult GetTeacherByDeptId(int deptId)
        {            
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            List<TeacherModel> teachers = new List<TeacherModel>();
            teachers = aCourseAssignManager.GetTeachersByDeptId(deptId);
            //var teachers = db.Teachers.Where(m => m.DepartmentId == deptId).ToList();
            //return Json(teachers, JsonRequestBehavior.AllowGet);
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseListByDeptId(int deptId)
        {
            List<CourseModel> courseList = new List<CourseModel>();
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            courseList = aCourseAssignManager.GetCourseListByDeptId(deptId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get teacher credit from teacher table
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public JsonResult GetCreditByTeacherId(int teacherId)
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            int creditToBeTaken = aCourseAssignManager.GetCreditByTeacherId(teacherId);
            return Json(creditToBeTaken, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// checks how much credits are remaining
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns>total remaining credit </returns>
        public JsonResult GetRemainingCreditByTeacherId(int teacherId)
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            int creditToBeTaken = aCourseAssignManager.GetCreditByTeacherId(teacherId);
            int remainingCredit;
            int totalAssignedCredit = aCourseAssignManager.GetAssignedCreditByTeacherId(teacherId);
            if (totalAssignedCredit == 0)
            {
                remainingCredit = creditToBeTaken;
            }
            else if (totalAssignedCredit < creditToBeTaken)
            {
                remainingCredit = creditToBeTaken - totalAssignedCredit;
            }
            else
            {
                remainingCredit = creditToBeTaken - totalAssignedCredit;
            }
            return Json(remainingCredit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// check if teacherAssignedCredit is more than the credit is being taken
        /// </summary>
        /// <param name="teacherID"></param>
        /// <param name="currentCreditTaking"></param>
        /// <returns></returns>
        public JsonResult CheckTakenCreditOverFlowsRemainingCredit(int teacherID, int currentCreditTaking)
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            int creditToBeTaken = aCourseAssignManager.GetCreditByTeacherId(teacherID);
            int totalAssignedCredit = aCourseAssignManager.GetAssignedCreditByTeacherId(teacherID);
            totalAssignedCredit = totalAssignedCredit + currentCreditTaking;
            if (totalAssignedCredit > creditToBeTaken)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult GetCourseModelByCourseId(int courseId)
        {
            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            CourseModel aCourseModel = new CourseModel();
            aCourseModel = aCourseAssignManager.GetCourseModelByCourseId(courseId);
            return Json(aCourseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditAssignedTeacher(int id, int? message)
        {
            if (message != null)
            {
                if (message == 0)
                {
                    ViewBag.Message = "Sorry! Course Assign Update Failed !!";
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

            CourseAssignManager aCourseAssignManager = new CourseAssignManager();
            ViewBag.Departments = aCourseAssignManager.GetAllDepartments();
            ViewBag.Teachers = aCourseAssignManager.GetAllTeachers();
            ViewBag.CourseCodeList = aCourseAssignManager.GetAllCourseCodes();


            CourseAssignToTeacherModel aCourseAssignToTeacherModel = new CourseAssignToTeacherModel();
            aCourseAssignToTeacherModel = aCourseAssignManager.GetAssignedCourseToTeacherModelById(id);



            ViewBag.Departments = aCourseAssignManager.GetAllDepartments();
            ViewBag.Teachers = aCourseAssignManager.GetAllTeachers();
            ViewBag.CourseCodeList = aCourseAssignManager.GetAllCourseCodes();
            return View(aCourseAssignToTeacherModel);
        }
        [HttpPost]
        public ActionResult EditAssignedTeacher(CourseAssignToTeacherModel courseAssignToTeacherModel)
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteAssignedTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteAssignedTeacher(int id)
        {
            return View();
        }
    }
}