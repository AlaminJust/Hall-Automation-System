using MyApp.Db.DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using MyApp.Models;
using LinqGrouping.Models;

namespace Just_HallAtumationSystem.Controllers
{
    public class ShowDetailsController : Controller
    {

        // GET: ShowDetails
        SearchOperation searchOperation = new SearchOperation();
        /// <summary>
        /// To see currently how many student is in room
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult GroupByRoom()
        {
            using (var context = new JustHallAtumationEntities())
            {
                var result = (from st in context.Students
                              join room in context.Rooms on st.RoomId equals room.RoomId
                              select new roomGroup
                              {
                                  roomNumber = room.RoomNumber.ToString(),
                                  student = st,
                                  totolSeat = (int)room.TotalSeat
                              }).ToList();
                return View(result);
            }
        }

        // To get Specific User Information by UserName
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult GetStudentData(string UserName)
        {
            try
            {
                var Student = searchOperation.GetStudentInformation(UserName);
                return View(Student);

            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        // To Show Student List
        [Authorize(Roles = "Admin,Student,MealAdmin")]

        public ActionResult Studentslist(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "data_desc" : "Date";
                ViewBag.DepartSortParm = sortOrder == "dep" ? "dep_desc" : "dep";
                ViewBag.SessionSortParm = sortOrder == "ses" ? "ses_desc" : "ses";
                ViewBag.RoomNoSortParm = sortOrder == "room" ? "room_desc" : "room";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                var studentLists = searchOperation.GetStudentList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    studentLists = studentLists.Where(x => x.student.StudentName.Contains(searchString) || x.room.RoomNumber.ToString().Contains(searchString)).ToList();
                }
                switch (sortOrder)
                {
                    case "name_dasc":
                        studentLists = studentLists.OrderByDescending(x => x.student.StudentName).ToList();
                        break;
                    case "Date":
                        studentLists = studentLists.OrderBy(x => x.HallEntryDate).ToList();
                        break;
                    case "data_desc":
                        studentLists = studentLists.OrderByDescending(x => x.HallEntryDate).ToList();
                        break;
                    case "dep":
                        studentLists = studentLists.OrderBy(x => x.department.DeptName).ToList();
                        break;
                    case "dep_desc":
                        studentLists = studentLists.OrderByDescending(x => x.department.DeptName).ToList();
                        break;

                    case "ses_desc":
                        studentLists = studentLists.OrderByDescending(x => x.departmentInfo.Session).ToList();
                        break;
                    case "ses":
                        studentLists = studentLists.OrderBy(x => x.departmentInfo.Session).ToList();
                        break;
                    case "room_desc":
                        studentLists = studentLists.OrderByDescending(x => x.room.RoomNumber).ToList();
                        break;
                    case "room":
                        studentLists = studentLists.OrderBy(x => x.room.RoomNumber).ToList();
                        break;

                    default:
                        studentLists = studentLists.OrderBy(x => x.student.StudentName).ToList();
                        break;
                }

                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(studentLists.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }


        // To Show Address Info
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult ShowAddress(int? Id) // Student Id
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = searchOperation.GetAdressOfUser((int)Id);
                return View(result);

            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        // To Show Room Information
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult ShowRoom(int? Id) // Student Id
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = searchOperation.GetRoomOfUser((int)Id);
                return View(result);

            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }

        // To Show Department Information
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult ShowDepartmentInfo(int? Id) // Student Id
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var result = searchOperation.GetDepartmentInfoOfUser((int)Id);
                return View(result);

            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }

        // To show Own Profile after login
        [Authorize(Roles = "Admin,Student,MealAdmin")]
        public ActionResult profile()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {

                }
                string UserName = User.Identity.Name;
                var Student = searchOperation.GetStudentInformation(UserName);
                return View(Student);

            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        public ActionResult AdminProfile()
        {
            try
            {
                using (var context = new JustHallAtumationEntities())
                {
                    AdminProfile adminProfiles = (from user in context.Users
                                                  join userim in context.UserImages on user.UserId equals userim.UserId
                                                  select new AdminProfile()
                                                  {
                                                      user = user,
                                                      userImage = userim
                                                  }).FirstOrDefault();
                    return View(adminProfiles);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}