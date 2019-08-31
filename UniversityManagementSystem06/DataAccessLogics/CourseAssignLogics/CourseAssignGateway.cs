using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.BusinessLogics.CourseAssignLogics;
using UniversityManagementSystem06.DataAccessLogics.DatabaseConnection;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.DataAccessLogics.CourseAssignLogics
{
    public class CourseAssignGateway:ConnectionString
    {

        //DepartmentManager aDepartmentManager = new DepartmentManager();
        //CourseManager aCourseManager = new CourseManager();
        //TeacherManager aTeacherManager = new TeacherManager();
        CourseAssignManager aCourseAssignManager = new CourseAssignManager();
        public List<int> GetAssignedCreditByTeacherId(int teacherId)
        {
            List<int> courseIdFromAssignedTeachers = new List<int>();
            SqlConnection aSqlConnection = new SqlConnection(connectionString);
            string query = "SELECT courseId FROM courseAssignToTeacher_tbl WHERE teacherId=@teacherId";
            SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@teacherId", teacherId);
            aSqlConnection.Open();
            SqlDataReader aSqlDataReader = aSqlCommand.ExecuteReader();
            while (aSqlDataReader.Read())
            {
                courseIdFromAssignedTeachers.Add(Convert.ToInt32(aSqlDataReader["courseId"].ToString()));
            }
            aSqlConnection.Close();
            return courseIdFromAssignedTeachers;
        }
        public int SaveCourseAssignToTeacher(CourseAssignToTeacherModel courseAssignToTeacherModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO courseAssignToTeacher_tbl(departmentId,teacherId,courseId) VALUES (@departmentId,@teacherId,@courseId)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@departmentId", courseAssignToTeacherModel.DepartmentId);
            cmd.Parameters.AddWithValue("@teacherId", courseAssignToTeacherModel.TeacherId);
            cmd.Parameters.AddWithValue("@courseId", courseAssignToTeacherModel.CourseId);
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
        public CourseAssignToTeacherModel GetSingleCourseAssignToTeacherModel(int courseAssignId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM courseAssignToTeacher_tbl WHERE id='" + courseAssignId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            // command.Parameters.AddWithValue("@deptId", DeptId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            CourseAssignToTeacherModel aCourseAssignToTeacherModel = new CourseAssignToTeacherModel();
            while (reader.Read())
            {
                aCourseAssignToTeacherModel.CourseAssignToTeacherId = Convert.ToInt32(reader["id"].ToString());
                aCourseAssignToTeacherModel.DepartmentId = int.Parse(reader["departmentId"].ToString());
                aCourseAssignToTeacherModel.CourseId = Convert.ToInt32(reader["courseId"].ToString());
                aCourseAssignToTeacherModel.TeacherId = int.Parse(reader["teacherId"].ToString());

            }
            aCourseAssignToTeacherModel.Department = aCourseAssignManager.GetSingleDepartmentByDeptId(aCourseAssignToTeacherModel.DepartmentId);
            aCourseAssignToTeacherModel.Teacher = aCourseAssignManager.GetSingleTeacherByTeacherId(aCourseAssignToTeacherModel.TeacherId);
            aCourseAssignToTeacherModel.Course = aCourseAssignManager.GetSingleCourseByCourseId(aCourseAssignToTeacherModel.CourseId);


            connection.Close();
            return aCourseAssignToTeacherModel;
        }
        public int DeleteAssignedCourseByAssignedCourseId(int assignedCourseId)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "DELETE FROM courseAssignToTeacher_tbl WHERE id=@assignedCourseId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@assignedCourseId", assignedCourseId);

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
        public int UpdateCourseAssign(CourseAssignToTeacherModel aCourseAssignToTeacherModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE courseAssignToTeacher_tbl SET departmentId=@departmentId,teacherId=@teacherId,courseId=@courseId WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@departmentId", aCourseAssignToTeacherModel.DepartmentId);
            cmd.Parameters.AddWithValue("@teacherId", aCourseAssignToTeacherModel.TeacherId);
            cmd.Parameters.AddWithValue("@courseId", aCourseAssignToTeacherModel.CourseId);
            cmd.Parameters.AddWithValue("@id", aCourseAssignToTeacherModel.CourseAssignToTeacherId);
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
        /// <summary>
        /// check if course is already assigned to another teacher
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public bool IsCourseExist(int courseID)
        {
            bool isCourseExists = false;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT CourseID FROM courseAssignToTeacher_tbl WHERE CourseID= @CourseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            //            command.Parameters.Add("DeptCode", SqlDbType.NVarChar);
            //            command.Parameters["DeptCode"].Value = DeptCode;
            command.Parameters.AddWithValue("@CourseID", courseID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isCourseExists = true;
            }
            connection.Close();
            return isCourseExists;
        }
        /*
        public bool IsCourseExist(int courseID)
        {
            bool isCourseExists = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT CourseID FROM courseAssignToTeacher_tbl WHERE CourseID= @CourseID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            //            command.Parameters.Add("DeptCode", SqlDbType.NVarChar);
            //            command.Parameters["DeptCode"].Value = DeptCode;
            command.Parameters.AddWithValue("@CourseID", courseID);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isCourseExists = true;
            }
            connection.Close();

            return isCourseExists;
        }
        */

        /// <summary>
        /// to get all courseAssignedToTeacherData
        /// </summary>
        /// <returns>returns all courseAssignedToTeacherData</returns>
        public List<CourseAssignToTeacherModel> GetAllCourseAssignedTeachers()
        {
            List<CourseAssignToTeacherModel> coursesAssignToTeacher = new List<CourseAssignToTeacherModel>();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM courseAssignToTeacher_tbl";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Clear();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CourseAssignToTeacherModel aCourseAssignToTeacherModel = new CourseAssignToTeacherModel();
                aCourseAssignToTeacherModel.CourseAssignToTeacherId = Convert.ToInt32(reader["ss"].ToString());
                aCourseAssignToTeacherModel.DepartmentId = Convert.ToInt32(reader[""].ToString());
                aCourseAssignToTeacherModel.TeacherId = Convert.ToInt32(reader[""].ToString());
                aCourseAssignToTeacherModel.CourseId = Convert.ToInt32(reader[""].ToString());
                aCourseAssignToTeacherModel.Department = aCourseAssignManager.GetSingleDepartmentByDeptId(aCourseAssignToTeacherModel.DepartmentId);
                aCourseAssignToTeacherModel.Teacher = aCourseAssignManager.GetSingleTeacherByTeacherId(aCourseAssignToTeacherModel.TeacherId);
                aCourseAssignToTeacherModel.Course = aCourseAssignManager.GetSingleCourseByCourseId(aCourseAssignToTeacherModel.CourseId);
                coursesAssignToTeacher.Add(aCourseAssignToTeacherModel);
            }

            return coursesAssignToTeacher;
        }
    }
}