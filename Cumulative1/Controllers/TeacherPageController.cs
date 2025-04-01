using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        //This is a private variable that holds the connection of data base
        private readonly TeacherAPIController _api;
        //This is a constructor that initializes the connection of data base
        public TeacherPageController(TeacherAPIController api)
        {
            //when this method is used we want to see the list of teachers 
            _api = api;
        }
        /// <summary>
        /// Retrieves a list of all teachers.
        /// </summary>
        /// <returns>A view displaying the list of teachers.</returns>
        /// <example>
        /// GET /TeacherPage/List
        /// </example>
        public IActionResult List()
        {
            List<Teacher> teacherList = _api.TeacherNameList();
            return View(teacherList);
        }
        /// <summary>
        /// Retrieves details of a specific teacher based on their ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the teacher.</param>
        /// <returns>A view displaying the teacher's details.</returns>
        /// <example>
        /// GET /TeacherPage/Show/1
        /// </example>
        public IActionResult Show(int Id)
        {
            Teacher teacherlist1 = _api.FindTeacherById(Id);
            return View(teacherlist1);
        }
    }
}
