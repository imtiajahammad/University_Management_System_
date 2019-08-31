using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.DataAccessLogics.Department;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.BusinessLogics.Department
{
    public class DepartmentManager
    {
        public string SaveDepartment(DepartmentModel aDepartmetModel)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            string message = "";

            if (aDepartmentGateway.IsDepartmentCodeExist(aDepartmetModel.DepartmentCode))
            {
                message = "Department Code Exists";
            }
            else if (aDepartmentGateway.IsDepartmentNameExist(aDepartmetModel.DepartmentName))
            {
                message = "Department Name Exists";
            }
            else
            {
                int rowAffected = aDepartmentGateway.SaveDepartment(aDepartmetModel);
                if (rowAffected > 0)
                {
                    message = "Department Saved Successfully";
                }
                else
                {
                    message = "Sorry! Department Save Failed !!";
                }
            }
            return message;
        }
        public List<DepartmentModel> GetAllDepartments()
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            List<DepartmentModel> departments = new List<DepartmentModel>();
            departments = aDepartmentGateway.GetAllDepartments();
            return departments;
        }
        public bool IsDepartmentCodeExist(string DeptCode)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            return aDepartmentGateway.IsDepartmentCodeExist(DeptCode);
        }
        public bool IsDepartmentNameExist(string DeptName)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            return aDepartmentGateway.IsDepartmentNameExist(DeptName);
        }
        public DepartmentModel GetDepartmentById(int DeptId)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            DepartmentModel aDepartmentModel = new DepartmentModel();
            aDepartmentModel = aDepartmentGateway.GetDepartmentById(DeptId);
            return aDepartmentModel;
        }
        public int UpdateDepartment(DepartmentModel aDepartmetModel)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            //            string message = "";
            /*            if (aDepartmentGateway.IsDepartmentCodeExist(aDepartmetModel.departmentCode)){
                            return 5;
                        }

                        else if (aDepartmentGateway.IsDepartmentNameExist(aDepartmetModel.departmentName)){

                            return 6;
                        }
                        else */
            {
                int rowAffected = aDepartmentGateway.UpdateDepartment(aDepartmetModel);


                //return message;
                return rowAffected;
            }


        }
        public int DeleteDepartment(int departmentId)
        {
            DepartmentGateway aDepartmentGateway = new DepartmentGateway();
            //          string message = "";

            int rowAffected = aDepartmentGateway.DeleteDepartment(departmentId);


            //return message;
            return rowAffected;
        }

    }
}