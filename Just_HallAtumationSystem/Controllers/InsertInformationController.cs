using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db.DbOperation;
using MyApp.Db;
using System.IO;

namespace Just_HallAtumationSystem.Controllers
{
    public class InsertInformationController : Controller
    {
        // GET: InsertInformation
        InsertInfoOperation insertInfoOperation = new InsertInfoOperation();
        public ActionResult InsertRoomInfo() // To insert Room information into Room table
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertRoomInfo(Room model)
        {
            if (ModelState.IsValid)
            {
                int id = insertInfoOperation.InsertRoomInfo(model);
                if(id > 0) // Insert success 
                {
                    ViewBag.Success = "Successfully!";
                    ModelState.Clear();
                }
                else // Insert Failed
                {
                    ViewBag.Success = "Failed!";
                }
            }
            return View();
        }
        // Insert Student Information
        public ActionResult InsertStudentInfo()
        {
            StudentInfoModel studentInfoModel = new StudentInfoModel();
            using(var context = new JustHallAtumationEntities())
            {
                studentInfoModel.room = context.Rooms.ToList(); // To show DropDownList
            }
            return View(studentInfoModel);
        }
        [HttpPost]
        public ActionResult InsertStudentInfo(StudentInfoModel model)
        {
            if (ModelState.IsValid)
            {
                int id = insertInfoOperation.InsertStudentInfo(model, (string)Session["UserName"]);
                if(id > 0) // Successfully Insert
                {
                    ViewBag.Success = "Succesfully Inserted!";
                    ModelState.Clear();
                }
                else // Failed to Insert
                {
                    ViewBag.Success = "Failed!";
                }
            }
            return RedirectToAction("InsertUserDepartmentInfo", "InsertStudentInfo"); // Redirected to Department Info
        }

        // To Insert Department Name Of New Department 
        public ActionResult InsertDepartmentName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertDepartmentName(Department model)
        {
            if (ModelState.IsValid)
            {
                int id = insertInfoOperation.InsertDepartmentName(model);
                if(id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Success = "SuccessFully!";
                }
                else
                {
                    ViewBag.Success = "Failed";
                }
            }
            return View();
        }


        //To Insert Users Department Information
        public ActionResult InsertUserDepartmentInfo()
        {
            DepartmentInfoModel departmentInfoModel = new DepartmentInfoModel();
            using(var context = new JustHallAtumationEntities()) // To get Dropdown list for Department Name or Id
            {
                departmentInfoModel.department = context.Departments.ToList();
            }
            return View(departmentInfoModel);
        }
        [HttpPost]
        public ActionResult InsertUserDepartmentInfo(DepartmentInfoModel model)
        {
            if (ModelState.IsValid)
            {
                int id = insertInfoOperation.InsertUserDepartmentInfo(model, (string)Session["UserName"]);
                if(id > 0) // Inserted successfully
                {
                    ModelState.Clear();
                    ViewBag.Success = "Information Added!";
                }
                else // Insertion failed!
                {
                    ViewBag.Success = "Failed!";
                }
            }
            return View();
        }

        public ActionResult InsertDistrictName()
        {
            return View();
        }
        [HttpPost]
        //Insert districtName
        public ActionResult InsertDistrictName(District model)
        {
            if (ModelState.IsValid)
            {
                int id = insertInfoOperation.InsertDistrictName(model);
                if (id > 0)//Insert successfull
                {
                    ModelState.Clear();
                    ViewBag.Success = "Information added";
                }
                else//insert failed
                {
                    ViewBag.Success = "Failed!";
                }
            }
            return View();
        }

        public ActionResult InsertAddress()
        {
            AddressInfoModel addressInfoModel = new AddressInfoModel();
            using (var context=new JustHallAtumationEntities())
            {
                addressInfoModel.districts = context.Districts.ToList();
            }
            return View(addressInfoModel);
        }
        [HttpPost]
        public ActionResult InsertAddress(AddressInfoModel model)
        {
            if (ModelState.IsValid)
            {
                int id = insertInfoOperation.InsertAddress(model,(string)Session["UserName"]);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Success = "Information Added";
                }
                else
                {
                    ViewBag.Success = "failed!";
                }
            }
            return RedirectToAction("Contact","Home");
        }

        // To Upload Image for User

        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(ImageUploadModel img, HttpPostedFileBase file)

        {
            string UserName = (string)Session["UserName"];
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images"),
                                       Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    img.ImagePath = file.FileName;
                }

                using(var context = new JustHallAtumationEntities())
                {
                    var user = context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                    var image = context.UserImages.Where(x => x.UserId == user.UserId).FirstOrDefault();
                    if(image == null)
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
                
                return RedirectToAction("Index","Home");
            }
            return View(img);
        }
    }
}