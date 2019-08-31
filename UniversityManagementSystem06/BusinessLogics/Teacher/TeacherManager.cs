using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.DataAccessLogics.Department;
using UniversityManagementSystem06.DataAccessLogics.Designation;
using UniversityManagementSystem06.DataAccessLogics.Teacher;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.BusinessLogics.Teacher
{
    public class TeacherManager
    {
        public string SaveTeacher(TeacherModel aTeacherModel)
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            string message = "";
            if (aTeacherGateWay.IsTeacherEmailExist(aTeacherModel.TeacherEmail))
            {
                message = "Teacher Email Exists";
            }
            else
            {
                int rowAffected = aTeacherGateWay.SaveTeacher(aTeacherModel);// aDepartmentGateway.SaveDepartment(aDepartmetModel);
                if (rowAffected > 0)
                {
                    message = "Teacher Saved Successfully";
                }
                else
                {
                    message = "Sorry! Teacher Save Failed !!";
                }
            }
            return message;
        }

        public List<string> GetAllDepartmentCodes()
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            List<string> listOfDepartmentCode = new List<string>();
            listOfDepartmentCode = aDepartmentGateway.GetDepartmentCodeList();
            return listOfDepartmentCode;
        }

        public List<TeacherModel> GetTeachersByDepartmentId(int deptId)
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            //          TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            List<TeacherModel> teachers = new List<TeacherModel>();
            teachers = aTeacherGateWay.GetTeachersByDept(deptId);
            return teachers;
        }

        public int GetCreditByTeacherId(int teacherId)
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            return aTeacherGateWay.GetCreditByTeacherId(teacherId);
        }






        public List<DesignationModel> GetAllDesignations()
        {
            DesignationGateway aDesignationGateway = new DesignationGateway();
            List<DesignationModel> designations = new List<DesignationModel>();
            designations = aDesignationGateway.GetAllDesignations();
            return designations;
        }

        public List<DepartmentModel> GetAllDepartments()
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            List<DepartmentModel> listOfDepartments = new List<DepartmentModel>();
            listOfDepartments = aDepartmentGateway.GetAllDepartments();
            return listOfDepartments;
        }

        public List<TeacherModel> GetAllTeachers()
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            List<TeacherModel> teachers = aTeacherGateWay.GetAllTeachers();
            return teachers;
        }

        public int UpdateTeacher(TeacherModel aTeacherModel)
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            //{

            //if (aTeacherGateWay.IsTeacherEmailExist(aTeacherModel.teacherEmail))
            //{
            //    return 5;
            //}
            //else
            //{
            int rowAffected = aTeacherGateWay.UpdateTeacher(aTeacherModel);
            return rowAffected;
            //    }

            //}
        }

        public TeacherModel GetTeacherByTeacherId(int teacherId)
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            TeacherModel aTeacherModel = new TeacherModel();
            aTeacherModel = aTeacherGateWay.GetTeacherForEdit(teacherId);
            return aTeacherModel;
        }

        public int DeleteTeacher(int deleteId)
        {
            TeacherGateWay aTeacherGateWay = new TeacherGateWay();
            //          string message = "";
            int rowAffected = aTeacherGateWay.DeleteTeacher(deleteId);
            //return message;
            return rowAffected;
        }
    }
}