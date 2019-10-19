using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Db;
using MyApp.Db.DbOperation;

namespace Just_HallAtumationSystem.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Update
        UpdateOperation updateOperation = new UpdateOperation();
        public ActionResult UpdateStudentInfo(int? Id)
        {
            if(Id == null)
            {
                // return Threat
            }
            StudentUpdateModel studentUpdateModel = new StudentUpdateModel();
            studentUpdateModel.StudentId = (int)Id;
            using(var context = new JustHallAtumationEntities())
            {
                studentUpdateModel.room = context.Rooms.ToList();
            }
            return View(studentUpdateModel);
        }
        [HttpPost]
        public ActionResult UpdateStudentInfo(StudentUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                int id = updateOperation.StudentInformationUpdate(model);
                if(id > 0)
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

        //Update Address Information

        public ActionResult UpdateAddressInfo(int? id)
        {
            AddressInfoModel addressInfoModel = new AddressInfoModel();
            addressInfoModel.StudentId = (int)id;
            using(var context=new JustHallAtumationEntities())
            {
                addressInfoModel.districts = context.Districts.ToList();
            }

            return View(addressInfoModel);
        }
        [HttpPost]
        public ActionResult UpdateAddressInfo(AddressInfoModel model)
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
                    ViewBag.Success = "Failed";
                }
            }
            return RedirectToAction("StudentList", "ShowDetails");

        }
    }
}