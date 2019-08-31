using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem06.BusinessLogics.Department;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();

        // GET: Department
        [HttpGet]
        public ActionResult SaveDepartment()
        {
            return View();
        }

        public ActionResult Try()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveDepartment(DepartmentModel aDepartmentModel)
        {
            string message = "";

            message = aDepartmentManager.SaveDepartment(aDepartmentModel);

            ViewBag.Message = message;

            return View();
        }

        [HttpGet]

        public ActionResult ViewDepartments(int? messageFromEdit)
        {
            List<DepartmentModel> departments = new List<DepartmentModel>();
            departments = aDepartmentManager.GetAllDepartments();
            if (departments.Count == 0)
            {
                //int d = departments.Count;
                string message = "No data in the database for Departments";
                ViewBag.MessageViewDepartments = message;
            }
            else if (messageFromEdit > 0)
            {
                ViewBag.MessageViewDepartments = "Department Updated Successfully";
            }
            ViewBag.DepartmentList = departments;
            //int i = 0;
            return View();
        }

        public JsonResult IsDeptCodeExists(string DeptCode)
        {
            bool isDeptCodeExists = aDepartmentManager.IsDepartmentCodeExist(DeptCode);

            if (isDeptCodeExists)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult IsDeptNameExists(string DeptName)
        {
            bool isDeptNameExists = aDepartmentManager.IsDepartmentNameExist(DeptName);

            if (isDeptNameExists)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult EditDepartmentFromList(int departmentId, int? message)
        {
            if (message != null)
            {
                if (message == 0)
                {
                    ViewBag.Message = "Sorry! Department Update Failed !!";
                }
                else if (message == 5)
                {
                    ViewBag.Message = "Sorry! Department Code Exists !!";

                }
                else if (message == 6)
                {
                    ViewBag.Message = "Sorry! Department Name Exists !!";

                }

            }
            DepartmentModel aDepartmentModel = new DepartmentModel();
            aDepartmentModel = aDepartmentManager.GetDepartmentById(departmentId);
            return View(aDepartmentModel);
        }


        [HttpPost]
        public ActionResult EditDepartmentFromList(DepartmentModel aDepartmentModel)
        {
            //string message = "";
            int rowAffected;
            rowAffected = aDepartmentManager.UpdateDepartment(aDepartmentModel);

            //ViewBag.Message2 = message;
            if (rowAffected == 1)
            {
                return RedirectToAction("ViewDepartments", new { message = rowAffected });
            }
            else
            {
                return RedirectToAction("EditDepartmentFromList", new { departmentId = aDepartmentModel.DepartmentId, message = rowAffected });
            }
        }


        public ActionResult DeleteDepartmentFromList(int departmentId)
        {

            int rowsEffected = aDepartmentManager.DeleteDepartment(departmentId);
            if (rowsEffected > 0)
            {
                return RedirectToAction("ViewDepartments");
            }
            else
            {
                string msg = "Delete could not occur";
                return RedirectToAction("ViewDepartments", new { message = msg });
            }


        }
    }
}