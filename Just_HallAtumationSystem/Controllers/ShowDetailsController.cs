using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Db.DbOperation;
using MyApp.Models;

namespace Just_HallAtumationSystem.Controllers
{
    public class ShowDetailsController : Controller
    {
        // GET: ShowDetails
        SearchOperation searchOperation = new SearchOperation();
        // To get Specific User Information by UserName
        public ActionResult GetStudentData(string UserName) 
        {
            var Student = searchOperation.GetStudentInformation(UserName);
            return View(Student);
        }
        // To Show Student List
        public ActionResult StudentList()
        {
            var studentList = searchOperation.GetStudentList();
            return View(studentList);
        }

        // To Show Address Info
        public ActionResult ShowAddress(int? Id) // Student Id
        {
            var result = searchOperation.GetAdressOfUser((int)Id);
            return View(result);
        }
        // To Show Room Information
        public ActionResult ShowRoom(int? Id) // Student Id
        {
            var result = searchOperation.GetRoomOfUser((int)Id); 
            return View(result);
        }

        // To Show Department Information
        public ActionResult ShowDepartmentInfo(int? Id) // Student Id
        {
            var result = searchOperation.GetDepartmentInfoOfUser((int)Id);
            return View(result);
        }
       



    }
}