using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cumulative1.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
namespace Cumulative1.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        // Dependency injection of database context
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns a list of all teachers in the system.
        /// </summary>
        /// <example>
        /// GET api/Teacher/TeacherNames -> [{"TeacherId":2, "TeacherFName":"Caitlin", "TeacherLName":"Cummings", "TeacherEmployeeID":"IdT381", "HireDate":"10-06-2014 00:00:00", "Salary":62.77}, ...]
        /// </example>
        /// <returns>
        /// A list of Teacher objects containing details of all teachers.
        /// </returns>
        [HttpGet]
        [Route(template: "TeacherNames")]
        public List<Teacher> TeacherNameList()
        {
            // Create an empty list to store teacher details
            List<Teacher> teacherDetails = new List<Teacher>();
            // Connection is being assigned to the connection string
            MySqlConnection Connection = _context.AccessDataBase();

            Connection.Open();
            string SQLquery = "SELECT * FROM teachers";
            // Create a SQL command to retrieve all teachers
            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = SQLquery;

            // Execute the query and store the result set
            MySqlDataReader ResultSet = Command.ExecuteReader();
            // Loop through each row in the result set
            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeID = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                double Salary = Convert.ToDouble(ResultSet["salary"]);



                
                Teacher newteacher = new Teacher();

                newteacher.TeacherFName = TeacherFName;
                newteacher.TeacherLName = TeacherLName;
                newteacher.TeacherEmployeeID = TeacherEmployeeID;
                newteacher.HireDate = HireDate;
                newteacher.Salary = Salary;
                newteacher.TeacherId = Convert.ToInt32(ResultSet["TeacherId"]);
                // Add the teacher object to the list
                teacherDetails.Add(newteacher);
            }
            Connection.Close();
            return teacherDetails;     // Return the final list of teacher details

        }
        /// <summary>
        /// Retrieves a specific teacher's details by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the teacher.</param>
        /// <returns>
        /// A Teacher object containing details all the teachers.
        /// </returns>
        /// <example>
        /// GET api/Teacher/FindTeacherById/1 -> {"TeacherId":3, "TeacherFName":"Linda", "TeacherLName":"Chan", "TeacherEmployeeID":"EMP001", "HireDate":"22-08-2015 00:00:00", "Salary":60.22}
        /// In this we can get a list of teachers on a sam page and by 
        /// </example>
        [HttpGet]
        [Route(template: "FindTeacherById/{id}")]
        public Teacher FindTeacherById(int id)
        {

            // Connection is being assigned to the connection string
            MySqlConnection Connection = _context.AccessDataBase();

            Connection.Open();

            string SQLquery = "SELECT * FROM teachers WHERE teacherid =" + id.ToString();
            // Create SQL query to find teacher by ID
            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = SQLquery;

            MySqlDataReader ResultSet = Command.ExecuteReader();

            Teacher newteacher = new Teacher();
            // Loop through the result set
            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherEmployeeID = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                double Salary = Convert.ToDouble(ResultSet["salary"]);



                newteacher.TeacherFName = TeacherFName;
                newteacher.TeacherLName = TeacherLName;
                newteacher.TeacherEmployeeID = TeacherEmployeeID;
                newteacher.HireDate = HireDate;
                newteacher.Salary = Salary;
                newteacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
            }
            Connection.Close();
            // Return the teacher object with the retrieved details
            return newteacher;
        }

    }
}

