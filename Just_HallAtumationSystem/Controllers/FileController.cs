using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MyApp.Models;

namespace Just_HallAtumationSystem.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase files , string FileTile)
        {
            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                MyApp.Models.File Fd = new MyApp.Models.File();
                Fd.FileTitle = FileTile;
                Fd.FileName = files.FileName;
                Fd.Datetime = DateTime.Now;
                Fd.FileContent = FileDet;

                using(var context = new JustHallAtumationEntities())
                {
                    context.Files.Add(Fd);
                    context.SaveChanges();
                }

                return RedirectToAction("FileUpload");
            }
            else
            {

                ViewBag.FileStatus = "Invalid file format.";
                return View();

            }
        }

        private List<MyApp.Models.File> GetFileList()
        {
            List<MyApp.Models.File> files = new List<MyApp.Models.File>();
            using (var context = new JustHallAtumationEntities())
            {
                files = context.Files.ToList();
            }
            return files;
        }

        public ActionResult FileDetails()
        {
            List < MyApp.Models.File > files = new List<MyApp.Models.File>();
            files = GetFileList();
            files.Reverse();
            return View(files);
        }


        [HttpGet]
        public FileResult DownLoadFile(int id)
        {


            List<MyApp.Models.File> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                            where FC.FileId.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);

        }

    }
}