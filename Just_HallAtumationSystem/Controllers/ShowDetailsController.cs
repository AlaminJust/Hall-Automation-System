using MyApp.Db.DbOperation;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Just_HallAtumationSystem.Controllers
{
    public class ShowDetailsController : Controller
    {
       
        // GET: ShowDetails
        SearchOperation searchOperation = new SearchOperation();
        // To get Specific User Information by UserName
        [Authorize(Roles ="Admin")]
        public ActionResult GetStudentData(string UserName) 
        {
            try
            {
                var Student = searchOperation.GetStudentInformation(UserName);
                return View(Student);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }
        // To Show Student List
        [Authorize(Roles = "Admin")]
        public ActionResult StudentList(int ?Id)
        {
            try
            {
                if (Id == null || Id == 0)
                {
                    var studentLists = searchOperation.GetStudentList().OrderBy(x => x.student.StudentName);
                    return View(studentLists);
                }
                else if(Id == 1)
                {
                    var studentLists = searchOperation.GetStudentList().OrderBy(x => x.department.DeptName);
                    return View(studentLists);
                }
                else if(Id == 2)
                {
                    var studentLists = searchOperation.GetStudentList().OrderBy(x => x.departmentInfo.Session);
                    return View(studentLists);
                }
                else if(Id == 3)
                {
                    var studentLists = searchOperation.GetStudentList().OrderBy(x => x.room.RoomNumber);
                    return View(studentLists);
                }
                else if(Id == 4)
                {
                    var studentLists = searchOperation.GetStudentList().OrderBy(x => x.HallEntryDate);
                    return View(studentLists);
                }
                return View(searchOperation.GetStudentList());
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        // To Show Address Info
        public ActionResult ShowAddress(int? Id) // Student Id
        {
            try
            {
                var result = searchOperation.GetAdressOfUser((int)Id);
                return View(result);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        // To Show Room Information
        public ActionResult ShowRoom(int? Id) // Student Id
        {
            try
            {
                var result = searchOperation.GetRoomOfUser((int)Id);
                return View(result);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        // To Show Department Information
        public ActionResult ShowDepartmentInfo(int? Id) // Student Id
        {
            try
            {
                var result = searchOperation.GetDepartmentInfoOfUser((int)Id);
                return View(result);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
       
        // To show Own Profile after login

        public ActionResult profile()
        {
            try
            {
                string UserName = User.Identity.Name;
                var Student = searchOperation.GetStudentInformation(UserName);
                return View(Student);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
    }
}