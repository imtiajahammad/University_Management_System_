using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem06.BusinessLogics.Department;
using UniversityManagementSystem06.BusinessLogics.Teacher;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager aTeacherManager = new TeacherManager();
        // GET: TeacherModel
        [HttpGet]
        public ActionResult SaveTeacher()
        {
            DepartmentManager aDepartmentManager = new DepartmentManager();
            List<DepartmentModel> departmentList = aDepartmentManager.GetAllDepartments();
            ViewBag.departments = departmentList;
            //string[] designationArray = { "Professor","Assistance Professor","Senior Teacher","Junior Teacher"};
            List<DesignationModel> designations = aTeacherManager.GetAllDesignations();
            ViewBag.designations = designations;
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher(TeacherModel aTeacherModel)
        {


            //if (aTeacherModel.DesignationId == 0)
            //{
            //    ViewBag.Message = "Teacher Save Failed !! \n Designation is not selected";
            //}
            //else if (aTeacherModel.DepartmentId==0)
            //{
            //    ViewBag.Message = "Teacher Save Failed !! \n Designation is not selected";
            //}
            //else
            {
                string message = "";
                message = aTeacherManager.SaveTeacher(aTeacherModel);

                ViewBag.Message = message;
            }


            DepartmentManager aDepartmentManager = new DepartmentManager();
            List<DepartmentModel> departmentList = aDepartmentManager.GetAllDepartments();
            ViewBag.departments = departmentList;

            List<DesignationModel> designations = aTeacherManager.GetAllDesignations();
            ViewBag.designations = designations;

            return View();
        }

        [HttpGet]
        public ActionResult ViewAllTeachers(int? message)
        {
            List<TeacherModel> teachers = new List<TeacherModel>();
            teachers = aTeacherManager.GetAllTeachers();
            if (teachers.Count == 0)
            {
                string msg = "No data in the database for teachers";
                ViewBag.Message = msg;
            }
            else if (message > 0)
            {
                ViewBag.Message = "Teacher Updated Successfully";
            }
            ViewBag.TeacherList = teachers;
            return View();
        }


        [HttpGet]
        public ActionResult EditTeacherFromList(int teacherId, int? message)
        {
            //use modelstate.IsValid
            if (message != null)
            {
                if (message == 0)
                {
                    ViewBag.Message = "Sorry! Teacher Update Failed !!";
                }
                else if (message == 5)
                {
                    ViewBag.Message = "Sorry! Teacher Email Exists !!";
                }
            }
            TeacherModel aTeacherModel = new TeacherModel();
            aTeacherModel = aTeacherManager.GetTeacherByTeacherId(teacherId);

            List<DepartmentModel> depts = aTeacherManager.GetAllDepartments();
            List<DesignationModel> designations = aTeacherManager.GetAllDesignations();
            ViewBag.Depts = depts;
            ViewBag.Designations = designations;

            return View(aTeacherModel);
        }


        [HttpPost]
        public ActionResult EditTeacherFromList(TeacherModel aTeacherModel)
        {
            //string message = "";  
            int rowAffected;
            rowAffected = aTeacherManager.UpdateTeacher(aTeacherModel);

            //ViewBag.Message2 = message;
            if (rowAffected == 1)
            {
                return RedirectToAction("ViewAllTeachers", new { message = rowAffected });
            }
            else
            {
                return RedirectToAction("EditTeacherFromList", new { teacherId = aTeacherModel.TeacherId, message = rowAffected });
            }
        }




        public ActionResult DeleteTeacherFromList(int teacherId)
        {
            int rowsEffected = aTeacherManager.DeleteTeacher(teacherId);
            if (rowsEffected > 0)
            {
                return RedirectToAction("ViewAllTeachers");
            }
            else
            {
                return null;
            }
        }
    }
}