using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db;

namespace Just_HallAtumationSystem.Controllers
{
    public class FilterController : Controller
    {
        // GET: Filter
        public ActionResult Index()
        {
            List<District> districts = new List<District>();
            using(var context = new JustHallAtumationEntities())
            {
                districts = context.Districts.ToList();
            }
            return View(districts);
        }
        public ActionResult Search()
        {
            SearchingModel searchingModel = new SearchingModel();
            using(var context = new JustHallAtumationEntities())
            {
                var result = (from u in context.Users
                              join s in context.Students on u.UserId equals s.UserId
                              join r in context.Rooms on s.RoomId equals r.RoomId
                              select new Searching
                              {
                                  user = u,
                                  student = s,
                                  room = r
                              }).ToList();
                searchingModel.searchings = result;
                searchingModel.search = context.Searches.ToList();
            }
            return View(searchingModel);
        }
        [HttpPost]
        public ActionResult Search(SearchingModel model)
        {
            SearchingModel searchingModel = new SearchingModel();
            if (ModelState.IsValid)
            {
                using (var context = new JustHallAtumationEntities())
                {
                    if(model.SearchId == 1)
                    {
                        var result = (from u in context.Users
                                      join s in context.Students on u.UserId equals s.UserId
                                      join r in context.Rooms on s.RoomId equals r.RoomId
                                      where(r.RoomNumber.ToString() == model.searching)
                                      select new Searching
                                      {
                                          user = u,
                                          student = s,
                                          room = r
                                      }).ToList();
                        searchingModel.searchings = result;
                    }
                    else if(model.SearchId == 3)
                    {
                        var result = (from u in context.Users
                                      join s in context.Students on u.UserId equals s.UserId
                                      join r in context.Rooms on s.RoomId equals r.RoomId
                                      where (u.UserName == model.searching)
                                      select new Searching
                                      {
                                          user = u,
                                          student = s,
                                          room = r
                                      }).ToList();
                        searchingModel.searchings = result;
                    }
                    else if(model.SearchId == 4)
                    {
                        var result = (from u in context.Users
                                      join s in context.Students on u.UserId equals s.UserId
                                      join r in context.Rooms on s.RoomId equals r.RoomId
                                      join ad in context.Addresses on s.StudentId equals ad.StudentId
                                      join d in context.Districts on ad.P_DistrictId equals d.DistrictId
                                      where (d.DistrictName == model.searching)
                                      select new Searching
                                      {
                                          user = u,
                                          student = s,
                                          room = r
                                      }).ToList();
                        searchingModel.searchings = result;
                    }
                    else if(model.SearchId == 5) /// Insert Kora hoinai Roll number
                    {
                        var result = (from u in context.Users
                                      join s in context.Students on u.UserId equals s.UserId
                                      join r in context.Rooms on s.RoomId equals r.RoomId
                                      where (r.RoomNumber.ToString() == model.searching)
                                      select new Searching
                                      {
                                          user = u,
                                          student = s,
                                          room = r
                                      }).ToList();
                        searchingModel.searchings = result;
                    }
                    else if(model.SearchId == 6)
                    {
                        var result = (from u in context.Users
                                      join s in context.Students on u.UserId equals s.UserId
                                      join r in context.Rooms on s.RoomId equals r.RoomId
                                      where (s.StudentName == model.searching)
                                      select new Searching
                                      {
                                          user = u,
                                          student = s,
                                          room = r
                                      }).ToList();
                        searchingModel.searchings = result;
                    }
                    else if(model.SearchId == 7)
                    {
                        var result = (from u in context.Users
                                      join s in context.Students on u.UserId equals s.UserId
                                      join r in context.Rooms on s.RoomId equals r.RoomId
                                      join depinfo in context.DepartmentInfoes on s.StudentId equals depinfo.StudentId
                                      join dep in context.Departments on depinfo.DepartmentId equals dep.DeptId
                                      where ( dep.DeptName == model.searching)
                                      select new Searching
                                      {
                                          user = u,
                                          student = s,
                                          room = r
                                      }).ToList();
                        searchingModel.searchings = result;
                    }

                    searchingModel.search = context.Searches.ToList();
                }

            }
            return View(searchingModel);
        }

    }
}