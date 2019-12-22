using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db;
using MyApp.Db.DbOperation;
using System.Net;

namespace Just_HallAtumationSystem.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Update
        UpdateOperation updateOperation = new UpdateOperation();
        public ActionResult UpdateStudentInfo(int? Id) // Student Id
        {
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentUpdateModel studentUpdateModel = new StudentUpdateModel();
                studentUpdateModel.StudentId = (int)Id;
                using (var context = new JustHallAtumationEntities())
                {
                    var student = context.Students.Where(x => x.StudentId == (int)Id).FirstOrDefault();
                    studentUpdateModel.StudentName = student.StudentName;
                    studentUpdateModel.FatherName = student.FatherName;
                    studentUpdateModel.MotherName = student.MotherName;
                    studentUpdateModel.MobileNumber = student.MobileNumber;
                    studentUpdateModel.room = context.Rooms.ToList();
                }
                return View(studentUpdateModel);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
        [HttpPost]
        public ActionResult UpdateStudentInfo(StudentUpdateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = updateOperation.StudentInformationUpdate(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.Successs = "Data Updated!";
                    }
                    else
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return RedirectToAction("StudentList", "ShowDetails");

            }
             catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        //Update Address Information

        public ActionResult UpdateAddressInfo(int? id) // Student Id
        {
            try
            {
                AddressInfoModel addressInfoModel = new AddressInfoModel();
                addressInfoModel.StudentId = (int)id;
                using (var context = new JustHallAtumationEntities())
                {
                    var address = context.Addresses.Where(x => x.StudentId == (int)id).FirstOrDefault();
                    addressInfoModel.P_PostOffice = address.P_PostOffice;
                    addressInfoModel.P_VillageName = address.P_VillageName;
                    addressInfoModel.T_PostOffice = address.T_PostOffice;
                    addressInfoModel.T_VillageName = address.T_VillageName;
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
        public ActionResult UpdateAddressInfo(AddressInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = updateOperation.AddressInformationUpdate(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.Success = "Inserted Successfully";
                    }
                    else
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return RedirectToAction("StudentList", "ShowDetails");

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            

        }
        public ActionResult UpdateDepartmentInfo(int? id)
        {
            try
            {
                DepartMentWithDeptName departmentInfoModel = new DepartMentWithDeptName();
                departmentInfoModel.StudentId = (int)id;
                using (var context = new JustHallAtumationEntities())
                {
                    var result = context.DepartmentInfoes.Where(x => x.StudentId == (int)id).FirstOrDefault();
                    departmentInfoModel.Cgpa = result.Cgpa;
                    departmentInfoModel.Session = result.Session;
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
        public ActionResult UpdateDepartmentInfo(DepartMentWithDeptName model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = updateOperation.DepartmentInfoUpdate(model);
                    if (id > 0)
                    {
                        ModelState.Clear();
                        ViewBag.Success = "Successfully Updated!";
                    }
                    else
                    {
                        ViewBag.Success = "Failed!";
                    }
                }
                return RedirectToAction("StudentList", "ShowDetails");

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }
    }
}