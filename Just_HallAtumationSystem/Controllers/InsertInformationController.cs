using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperation;
using MyApp.Db;
using System.IO;
using System.Net;

namespace Just_HallAtumationSystem.Controllers
{
    public class InsertInformationController : Controller
    {
        // GET: InsertInformation
        InsertInfoOperation insertInfoOperation = new InsertInfoOperation();
        public ActionResult InsertRoomInfo() // To insert Room information into Room table
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult InsertRoomInfo(Room model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = insertInfoOperation.InsertRoomInfo(model);
                    if (id > 0) // Insert success 
                    {
                        ViewBag.Success = "Successfully Inserted!";
                        ModelState.Clear();
                    }
                    else // Insert Failed
                    {
                        ViewBag.Success = "Ops! Failed!";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        // Insert Student Information
        public ActionResult InsertStudentInfo()
        {
            try
            {
                StudentInfoModel studentInfoModel = new StudentInfoModel();
                using (var context = new JustHallAtumationEntities())
                {
                    var user = context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                    var std = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault();
                    if(std != null) // Student Information Already Given
                    {
                        return RedirectToAction("InsertUserDepartmentInfo", "InsertInformation");
                    }

                    studentInfoModel.room = context.Rooms.ToList(); // To show DropDownList
                }
                return View(studentInfoModel);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult InsertStudentInfo(StudentInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = insertInfoOperation.InsertStudentInfo(model, User.Identity.Name);
                    if (id > 0) // Successfully Insert
                    {
                        ViewBag.Success = "Succesfully Inserted!";
                        ModelState.Clear();
                    }
                    else // Failed to Insert
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return RedirectToAction("InsertUserDepartmentInfo", "InsertInformation"); // Redirected to Department Info
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        // To Insert Department Name Of New Department 
        public ActionResult InsertDepartmentName()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult InsertDepartmentName(Department model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = insertInfoOperation.InsertDepartmentName(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.Success = "SuccessFully Inserted!";
                    }
                    else
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }


        //To Insert Users Department Information
        public ActionResult InsertUserDepartmentInfo()
        {
            try
            {
                DepartmentInfoModel departmentInfoModel = new DepartmentInfoModel();

                using (var context = new JustHallAtumationEntities()) // To get Dropdown list for Department Name or Id
                {
                    var user = context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                    var std = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault();
                    if(std == null)
                    {
                        return RedirectToAction("InsertStudentInfo", "InsertInformation");
                    }
                    var add = context.DepartmentInfoes.Where(x => x.StudentId == std.StudentId).FirstOrDefault();
                    if(add != null)
                    {
                        return RedirectToAction("InsertAddress");
                    }

                    departmentInfoModel.department = context.Departments.ToList();
                }
                return View(departmentInfoModel);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult InsertUserDepartmentInfo(DepartmentInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = insertInfoOperation.InsertUserDepartmentInfo(model, User.Identity.Name);
                    if (id > 0) // Inserted successfully
                    {
                        ModelState.Clear();
                        ViewBag.Success = "Information Added!";
                    }
                    else // Insertion failed!
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return RedirectToAction("InsertAddress");
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        public ActionResult InsertDistrictName()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }
        [HttpPost]
        //Insert districtName
        public ActionResult InsertDistrictName(District model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = insertInfoOperation.InsertDistrictName(model);
                    if (id > 0)//Insert successfull
                    {
                        ModelState.Clear();
                        ViewBag.Success = "Information Inserted!";
                    }
                    else//insert failed
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }

        public ActionResult InsertAddress()
        {
            try
            {
                AddressInfoModel addressInfoModel = new AddressInfoModel();
                using (var context = new JustHallAtumationEntities())
                {
                    var user = context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                    var std = context.Students.Where(x => x.UserId == user.UserId).FirstOrDefault();
                    if (std == null) // If student information doesn't given
                    {
                        return RedirectToAction("InsertStudentInfo", "InsertInformation");
                    }
                    var dep = context.DepartmentInfoes.Where(x => x.StudentId == std.StudentId).FirstOrDefault();
                    if (dep == null)  /// DepartMent Info doesn't given go insertDepartment info function.
                    {
                        RedirectToAction("InsertUserDepartmentInfo", "InsertInformation");
                    }
                    var add = context.Addresses.Where(x => x.StudentId == std.StudentId).FirstOrDefault();
                    if (add != null) /// Go to Profile bcs already given the Address information.
                    {
                        return RedirectToAction("Contact", "Home");
                    }
                    addressInfoModel.districts = context.Districts.ToList();
                }
                return View(addressInfoModel);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult InsertAddress(AddressInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = insertInfoOperation.InsertAddress(model, User.Identity.Name);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.Success = "Information Inserted!";
                    }
                    else
                    {
                        ViewBag.Success = "failed!";
                    }
                }
                return RedirectToAction("UploadImage"); /// Should go to profile

            }
            catch (Exception ex)
            {
                return View(ex);
            }
            
        }

        // To Upload Image for User

        public ActionResult UploadImage()
        {
            try
            {
                return View();

            }
            catch(Exception ex)
            {
                return View(ex);

            }
            
        }

        [HttpPost]
        public ActionResult UploadImage(ImageUploadModel img, HttpPostedFileBase file)

        {
            try
            {
                string UserName = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    if(file == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Images"),
                                           Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        img.ImagePath = file.FileName;
                    }

                    using (var context = new JustHallAtumationEntities())
                    {
                        var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                        var image = context.UserImages.Where(x => x.UserId == user.UserId).FirstOrDefault();
                        if (image == null)
                        {
                            UserImage userImage = new UserImage
                            {
                                UserId = user.UserId,
                                Image = img.ImagePath

                            };
                            context.UserImages.Add(userImage);
                            context.SaveChanges();
                        }
                        else
                        {
                            image.Image = img.ImagePath;
                            context.SaveChanges();
                        }

                    }

                    return RedirectToAction("Index", "Home");
                }
                return View(img);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
           
        }
    }
}