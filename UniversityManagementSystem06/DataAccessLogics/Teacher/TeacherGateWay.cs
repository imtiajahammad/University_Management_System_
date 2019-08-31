using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.BusinessLogics.Department;
using UniversityManagementSystem06.BusinessLogics.Designation;
using UniversityManagementSystem06.DataAccessLogics.DatabaseConnection;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.DataAccessLogics.Teacher
{
    public class TeacherGateWay:ConnectionString
    {



        public bool IsTeacherEmailExist(string teacherEmail)
        {
            bool isTeacherEmailExist = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT teacherEmail FROM teacher_tbl WHERE teacherEmail=@teacherEmail";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            //command.Parameters.Add("DeptName", SqlDbType.NVarChar);
            //  command.Parameters["DeptName"].Value = DeptName;
            command.Parameters.AddWithValue("@teacherEmail", teacherEmail);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isTeacherEmailExist = true;
            }
            connection.Close();

            return isTeacherEmailExist;
        }

        public int SaveTeacher(TeacherModel aTeacherModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO teacher_tbl(teacherName,teacherAddress,teacherEmail,teacherNumber,designationId,departmentId,teacherCreditToBeTaken) VALUES (@teacherName,@teacherAddress,@teacherEmail,@teacherNumber,@designationId,@departmentId,@teacherCreditToBeTaken)";
            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@teacherName", aTeacherModel.TeacherName);
            cmd.Parameters.AddWithValue("@teacherAddress", aTeacherModel.TeacherAddress);
            cmd.Parameters.AddWithValue("@teacherEmail", aTeacherModel.TeacherEmail);
            cmd.Parameters.AddWithValue("@teacherNumber", aTeacherModel.TeacherNumber);
            cmd.Parameters.AddWithValue("@designationId", aTeacherModel.DesignationId);
            cmd.Parameters.AddWithValue("@departmentId", aTeacherModel.DepartmentId);
            cmd.Parameters.AddWithValue("@teacherCreditToBeTaken", aTeacherModel.TeacherCreditToBeTaken);


            int rowAffected = 0;
            try
            {
                connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {

            }
            return rowAffected;
        }

        public List<TeacherModel> GetAllTeachers()
        {
            List<TeacherModel> teachers = new List<TeacherModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM teacher_tbl";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DepartmentManager aDepartmentManager = new DepartmentManager();
                DesignationManager aDesignationManager = new DesignationManager();
                TeacherModel aTeacherModel = new TeacherModel();
                aTeacherModel.TeacherId = Convert.ToInt32(reader["teacherId"].ToString());
                aTeacherModel.TeacherName = reader["teacherName"].ToString();
                aTeacherModel.TeacherAddress = reader["teacherAddress"].ToString();
                aTeacherModel.TeacherEmail = reader["teacherEmail"].ToString();
                aTeacherModel.TeacherNumber = reader["teacherNumber"].ToString();
                aTeacherModel.DesignationId = int.Parse(reader["designationId"].ToString());
                aTeacherModel.Designation = aDesignationManager.GetDesignationById(aTeacherModel.DesignationId);
                aTeacherModel.DepartmentId = int.Parse(reader["departmentId"].ToString());
                aTeacherModel.Department = aDepartmentManager.GetDepartmentById(aTeacherModel.DepartmentId);
                aTeacherModel.TeacherCreditToBeTaken = Convert.ToInt32(reader["teacherCreditToBeTaken"].ToString());
                teachers.Add(aTeacherModel);
            }
            connection.Close();
            return teachers;
        }

        public TeacherModel GetTeacherForEdit(int teacherId)
        {
            DepartmentManager aDepartmentManager = new DepartmentManager();
            DesignationManager aDesignationManager = new DesignationManager();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM teacher_tbl WHERE teacherId='" + teacherId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            TeacherModel aTeacherModel = new TeacherModel();
            while (reader.Read())
            {
                aTeacherModel.TeacherId = Convert.ToInt32(reader["teacherId"].ToString());
                aTeacherModel.TeacherName = reader["teacherName"].ToString();
                aTeacherModel.TeacherAddress = reader["teacherAddress"].ToString();
                aTeacherModel.TeacherEmail = reader["teacherEmail"].ToString();
                aTeacherModel.TeacherNumber = reader["teacherNumber"].ToString();
                aTeacherModel.DesignationId = int.Parse(reader["designationId"].ToString());
                aTeacherModel.Designation = aDesignationManager.GetDesignationById(aTeacherModel.DesignationId);
                aTeacherModel.DepartmentId = int.Parse(reader["departmentId"].ToString());
                aTeacherModel.Department = aDepartmentManager.GetDepartmentById(aTeacherModel.DepartmentId);
                aTeacherModel.TeacherCreditToBeTaken = Convert.ToInt32(reader["teacherCreditToBeTaken"].ToString());
            }
            connection.Close();
            return aTeacherModel;
        }


        public List<TeacherModel> GetTeachersByDept(int deptId)
        {
            DepartmentManager aDepartmentManager = new DepartmentManager();
            DesignationManager aDesignationManager = new DesignationManager();
            List<TeacherModel> teachers = new List<TeacherModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM teacher_tbl WHERE departmentId='" + deptId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TeacherModel aTeacherModel = new TeacherModel();
                aTeacherModel.TeacherId = int.Parse(reader["teacherId"].ToString());
                aTeacherModel.TeacherName = reader["teacherName"].ToString();
                aTeacherModel.TeacherAddress = reader["teacherAddress"].ToString();
                aTeacherModel.TeacherEmail = reader["teacherEmail"].ToString();
                aTeacherModel.TeacherNumber = reader["teacherNumber"].ToString();
                aTeacherModel.DesignationId = int.Parse(reader["designationId"].ToString());
                aTeacherModel.Designation = aDesignationManager.GetDesignationById(aTeacherModel.DesignationId);
                aTeacherModel.DepartmentId = int.Parse(reader["departmentId"].ToString());
                aTeacherModel.Department = aDepartmentManager.GetDepartmentById(aTeacherModel.DepartmentId);
                teachers.Add(aTeacherModel);
            }
            connection.Close();
            return teachers;
        }

        public int GetCreditByTeacherId(int teacherId)
        {
            //List<string> teachers = new List<string>();
            int credit = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT teacherCreditToBeTaken FROM teacher_tbl WHERE teacherId=@teacherId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@teacherId", teacherId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    credit = Convert.ToInt32(reader["teacherCreditToBeTaken"].ToString());
                }
            }
            connection.Close();
            return credit;
        }

        //public bool getCreditAssignedToTeacher(int teacherId)
        //{
        //    SqlConnection aSqlConnection = new SqlConnection(connectionString);
        //    string query = "SELECT courseCredit FROM courseAssignToTeacher_tbl WHERE teacher=@teacher";
        //    SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
        //    aSqlCommand.Parameters.Clear();
        //    aSqlCommand.Parameters.AddWithValue("@teacher", teacherName);
        //    aSqlConnection.Open();
        //    SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
        //    if (aSqlDataReader.HasRows)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        public int DeleteTeacher(int teacherId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "DELETE FROM teacher_tbl WHERE teacherId=@teacherId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@teacherId", teacherId);
            int rowAffected = 0;
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
            }
            return rowAffected;
        }

        public int UpdateTeacher(TeacherModel aTeacherModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE teacher_tbl SET teacherName=@teacherName,teacherAddress=@teacherAddress,teacherEmail=@teacherEmail,teacherNumber=@teacherNumber,designationId=@designationId,departmentId=@departmentId,teacherCreditToBeTaken=@teacherCreditToBeTaken WHERE teacherId=@teacherId";
            SqlCommand commmand = new SqlCommand(query, connection);
            commmand.Parameters.Clear();
            commmand.Parameters.AddWithValue("@teacherName", aTeacherModel.TeacherName);
            commmand.Parameters.AddWithValue("@teacherAddress", aTeacherModel.TeacherAddress);
            commmand.Parameters.AddWithValue("@teacherEmail", aTeacherModel.TeacherEmail);
            commmand.Parameters.AddWithValue("@teacherNumber", aTeacherModel.TeacherNumber);
            commmand.Parameters.AddWithValue("@designationId", aTeacherModel.DesignationId);
            commmand.Parameters.AddWithValue("@departmentId", aTeacherModel.DepartmentId);
            commmand.Parameters.AddWithValue("@teacherCreditToBeTaken", aTeacherModel.TeacherCreditToBeTaken);
            commmand.Parameters.AddWithValue("@teacherId", aTeacherModel.TeacherId);
            int rowAffected = 0;
            try
            {
                connection.Open();
                rowAffected = commmand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                
            }
            return rowAffected;
        }

    }
}