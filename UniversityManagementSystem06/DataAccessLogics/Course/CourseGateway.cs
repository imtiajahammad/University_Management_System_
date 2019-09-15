using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystem06.BusinessLogics.Department;
using UniversityManagementSystem06.BusinessLogics.Semester;
using UniversityManagementSystem06.DataAccessLogics.DatabaseConnection;
using UniversityManagementSystem06.Models;

namespace UniversityManagementSystem06.DataAccessLogics.Course
{
    public class CourseGateway:ConnectionString
    {


        //SqlConnection connection = new SqlConnection(connectionString);


        public int SaveCourse(CourseModel aCourseModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO course_tbl(courseCode,courseName,courseCredit,courseDescription,departmentId,semesterId) VALUES (@courseCode,@courseName,@courseCredit,@courseDescription,@departmentId,@semesterId)";

            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@courseCode", aCourseModel.courseCode);
            cmd.Parameters.AddWithValue("@courseName", aCourseModel.courseName);
            cmd.Parameters.AddWithValue("@courseCredit", aCourseModel.courseCredit);
            if (string.IsNullOrEmpty(aCourseModel.courseDescription))
            {
                cmd.Parameters.AddWithValue("@courseDescription", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@courseDescription", aCourseModel.courseDescription);
            }
            cmd.Parameters.AddWithValue("@departmentId", aCourseModel.departmentId);
            cmd.Parameters.AddWithValue("@semesterId", aCourseModel.semesterId);

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
        public bool IsCourseCodeExist(string CourseCode)
        {
            bool isCourseCodeExists = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT courseCode FROM course_tbl WHERE courseCode= @courseCode ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.AddWithValue("@courseCode", CourseCode);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isCourseCodeExists = true;
            }
            connection.Close();

            return isCourseCodeExists;
        }
        public bool IsCourseNameExist(string CourseName)
        {
            bool isCourseNameExists = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT courseName FROM course_tbl WHERE courseName= @courseName ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            //command.Parameters.Add("DeptName", SqlDbType.NVarChar);
            //  command.Parameters["DeptName"].Value = DeptName;
            command.Parameters.AddWithValue("@courseName", CourseName);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isCourseNameExists = true;
            }
            connection.Close();

            return isCourseNameExists;
        }
        public List<CourseModel> GetAllCourses()
        {
            DepartmentManager aDepartmentManager = new DepartmentManager();
            SemesterManager aSemesterManager = new SemesterManager();
            List<CourseModel> courses = new List<CourseModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM course_tbl";
            //string query = "SELECT course_tbl.courseId, course_tbl.courseCode,course_tbl.courseName, " +
            //    "course_tbl.courseCredit, course_tbl.courseDescription, department_tbl.departmentName, " +
            //    "semester_tbl.semester FROM course_tbl " +
            //    "INNER JOIN department_tbl ON department_tbl.departmentId = course_tbl.departmentId " +
            //    "INNER JOIN semester_tbl ON semester_tbl.id = course_tbl.semesterId; ";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseModel aCourseModel = new CourseModel();
                aCourseModel.courseId = Convert.ToInt32(reader["courseId"].ToString());
                aCourseModel.courseCode = reader["courseCode"].ToString();
                aCourseModel.courseName = reader["courseName"].ToString();
                aCourseModel.courseCredit = Convert.ToDouble(reader["courseCredit"].ToString());
                aCourseModel.courseDescription = reader["courseDescription"].ToString();
                aCourseModel.departmentId = int.Parse(reader["departmentId"].ToString());
                aCourseModel.semesterId = int.Parse(reader["semesterId"].ToString());
                aCourseModel.Department = aDepartmentManager.GetDepartmentById(aCourseModel.departmentId);
                aCourseModel.Semester = aSemesterManager.GetSemesterById(aCourseModel.semesterId);
                courses.Add(aCourseModel);
            }

            connection.Close();
            return courses;
        }

        
        public List<CourseModel> GetAllCoursesNotAssignedToTeacher()
        {
            DepartmentManager aDepartmentManager = new DepartmentManager();
            SemesterManager aSemesterManager = new SemesterManager();
            List<CourseModel> courses = new List<CourseModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from course_tbl where courseid not in (select CourseId from courseAssignToTeacher_tbl)";
            //string query = "SELECT course_tbl.courseId, course_tbl.courseCode,course_tbl.courseName, " +
            //    "course_tbl.courseCredit, course_tbl.courseDescription, department_tbl.departmentName, " +
            //    "semester_tbl.semester FROM course_tbl " +
            //    "INNER JOIN department_tbl ON department_tbl.departmentId = course_tbl.departmentId " +
            //    "INNER JOIN semester_tbl ON semester_tbl.id = course_tbl.semesterId; ";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CourseModel aCourseModel = new CourseModel();
                aCourseModel.courseId = Convert.ToInt32(reader["courseId"].ToString());
                aCourseModel.courseCode = reader["courseCode"].ToString();
                aCourseModel.courseName = reader["courseName"].ToString();
                aCourseModel.courseCredit = Convert.ToDouble(reader["courseCredit"].ToString());
                aCourseModel.courseDescription = reader["courseDescription"].ToString();
                aCourseModel.departmentId = int.Parse(reader["departmentId"].ToString());
                aCourseModel.semesterId = int.Parse(reader["semesterId"].ToString());
                aCourseModel.Department = aDepartmentManager.GetDepartmentById(aCourseModel.departmentId);
                aCourseModel.Semester = aSemesterManager.GetSemesterById(aCourseModel.semesterId);
                courses.Add(aCourseModel);
            }

            connection.Close();
            return courses;
        }
        public CourseModel GetSingleCourseModel(int courseId)
        {
            DepartmentManager aDepartmentManager = new DepartmentManager();
            SemesterManager aSemesterManager = new SemesterManager();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM course_tbl WHERE courseId='" + courseId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            // command.Parameters.AddWithValue("@deptId", DeptId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            CourseModel aCourseModel = new CourseModel();
            while (reader.Read())
            {
                aCourseModel.courseId = Convert.ToInt32(reader["courseId"].ToString());
                aCourseModel.courseCode = reader["courseCode"].ToString();
                aCourseModel.courseName = reader["courseName"].ToString();
                aCourseModel.courseCredit = Convert.ToDouble(reader["courseCredit"].ToString());
                aCourseModel.courseDescription = reader["courseDescription"].ToString();
                aCourseModel.departmentId = int.Parse(reader["departmentId"].ToString());
                aCourseModel.semesterId = int.Parse(reader["semesterId"].ToString());
            }
            aCourseModel.Department = aDepartmentManager.GetDepartmentById(aCourseModel.departmentId);
            aCourseModel.Semester = aSemesterManager.GetSemesterById(aCourseModel.semesterId);

            connection.Close();
            return aCourseModel;
        }
        public int GetCourseCreditByCourseId(int courseId)
        {
            int credit = 0;
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT courseCredit FROM course_tbl WHERE courseId=@courseId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@courseId", courseId);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                credit = Convert.ToInt32(reader["courseCredit"].ToString());
            }
            //int aaa = Convert.ToInt32(reader["courseCredit"].ToString());
            con.Close();
            return credit;
        }
        public int DeleteCourse(int courseId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "DELETE FROM course_tbl WHERE courseId=@courseId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@courseId", courseId);
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
        public int UpdateCourse(CourseModel aCourseModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE course_tbl SET courseCode=@courseCode,courseName=@courseName,courseCredit=@courseCredit,courseDescription=@courseDescription,departmentId=@departmentId,semesterId=@semesterId WHERE courseId=@courseId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@courseCode", aCourseModel.courseCode);
            cmd.Parameters.AddWithValue("@courseName", aCourseModel.courseName);
            cmd.Parameters.AddWithValue("@courseCredit", aCourseModel.courseCredit);
            if (string.IsNullOrEmpty(aCourseModel.courseDescription))
            {
                cmd.Parameters.AddWithValue("@courseDescription", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@courseDescription", aCourseModel.courseDescription);
            }
            cmd.Parameters.AddWithValue("@departmentId", aCourseModel.departmentId);
            cmd.Parameters.AddWithValue("@semesterId", aCourseModel.semesterId);
            cmd.Parameters.AddWithValue("@courseId", aCourseModel.courseId);
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
        public List<string> GetCourseCodeList()
        {
            List<string> courseCodeList = new List<string>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT courseCode FROM course_tbl";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string DeptCode = reader["courseCode"].ToString();


                courseCodeList.Add(DeptCode);
            }
            connection.Close();
            return courseCodeList;
        }
        public List<string> GetCourseNameByCourseCode(string courseCode)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT courseName,courseCredit FROM course_tbl WHERE courseCode=@courseCode";
            SqlCommand commaand = new SqlCommand(query, connection);
            commaand.Parameters.Clear();
            commaand.Parameters.AddWithValue("@courseCode", courseCode);
            connection.Open();
            SqlDataReader aSqlDataReader = commaand.ExecuteReader();
            string courseName = aSqlDataReader["courseName"].ToString();
            int courseCredit = Convert.ToInt32(aSqlDataReader["courseCredit"].ToString());
            List<string> list = new List<string>();
            list.Add(courseName);
            list.Add(courseCredit.ToString());
            return list;
        }
        public List<string> GetCourseNameCreditByDept(string dept)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT courseName,courseCredit FROM course_tbl WHERE courseDepartment=@courseDepartment";
            SqlCommand commaand = new SqlCommand(query, connection);
            commaand.Parameters.Clear();
            commaand.Parameters.AddWithValue("@courseDepartment", dept);
            connection.Open();
            SqlDataReader aSqlDataReader = commaand.ExecuteReader();
            if (aSqlDataReader.HasRows)
            {
                string courseName = aSqlDataReader["courseName"].ToString();
                int courseCredit = Convert.ToInt32(aSqlDataReader["courseCredit"].ToString());
                List<string> list = new List<string>();
                list.Add(courseName);
                list.Add(courseCredit.ToString());
                return list;
            }
            else
            {
                return null;
            }
        }
        public List<CourseModel> GetCourseListByDeptId(int deptId)
        {
            SemesterManager aSemesterManager = new SemesterManager();
            DepartmentManager aDepartmentManager = new DepartmentManager();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM course_tbl WHERE departmentId=@departmentId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@departmentId", deptId);
            con.Open();
            SqlDataReader aSqlDataReader = cmd.ExecuteReader();
            List<CourseModel> courses = new List<CourseModel>();
            if (aSqlDataReader.HasRows)
            {
                while (aSqlDataReader.Read())
                {
                    CourseModel aCourseModel = new CourseModel();
                    aCourseModel.courseId = Convert.ToInt32(aSqlDataReader["courseId"].ToString());
                    aCourseModel.courseCode = aSqlDataReader["courseCode"].ToString();
                    aCourseModel.courseName = aSqlDataReader["courseName"].ToString();
                    aCourseModel.courseCredit = Convert.ToDouble(aSqlDataReader["courseCredit"].ToString());
                    aCourseModel.courseDescription = aSqlDataReader["courseDescription"].ToString();
                    aCourseModel.departmentId = int.Parse(aSqlDataReader["departmentId"].ToString());
                    aCourseModel.semesterId = int.Parse(aSqlDataReader["semesterId"].ToString());
                    aCourseModel.Department = aDepartmentManager.GetDepartmentById(aCourseModel.departmentId);
                    aCourseModel.Semester = aSemesterManager.GetSemesterById(aCourseModel.semesterId);
                    courses.Add(aCourseModel);
                }
            }
            con.Close();
            return courses;
        }
        public List<CourseModel> GetCourseListNotAssignedByDeptId(int deptId)
        {
            SemesterManager aSemesterManager = new SemesterManager();
            DepartmentManager aDepartmentManager = new DepartmentManager();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM course_tbl WHERE departmentId=@departmentId and courseId not in (select CourseId from courseAssignToTeacher_tbl)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@departmentId", deptId);
            con.Open();
            SqlDataReader aSqlDataReader = cmd.ExecuteReader();
            List<CourseModel> courses = new List<CourseModel>();
            if (aSqlDataReader.HasRows)
            {
                while (aSqlDataReader.Read())
                {
                    CourseModel aCourseModel = new CourseModel();
                    aCourseModel.courseId = Convert.ToInt32(aSqlDataReader["courseId"].ToString());
                    aCourseModel.courseCode = aSqlDataReader["courseCode"].ToString();
                    aCourseModel.courseName = aSqlDataReader["courseName"].ToString();
                    aCourseModel.courseCredit = Convert.ToDouble(aSqlDataReader["courseCredit"].ToString());
                    aCourseModel.courseDescription = aSqlDataReader["courseDescription"].ToString();
                    aCourseModel.departmentId = int.Parse(aSqlDataReader["departmentId"].ToString());
                    aCourseModel.semesterId = int.Parse(aSqlDataReader["semesterId"].ToString());
                    aCourseModel.Department = aDepartmentManager.GetDepartmentById(aCourseModel.departmentId);
                    aCourseModel.Semester = aSemesterManager.GetSemesterById(aCourseModel.semesterId);
                    courses.Add(aCourseModel);
                }
            }
            con.Close();
            return courses;
        }
    }
}