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
        public ActionResult FileUpload(HttpPostedFileBase files , string FileTile)
        {
            try
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
                    Fd.Date = DateTime.Now.Date;
                    Fd.FileContent = FileDet;

                    using (var context = new JustHallAtumationEntities())
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
            catch(Exception ex)
            {
                return View(ex);
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
            try
            {
                List<MyApp.Models.File> files = new List<MyApp.Models.File>();
                files = GetFileList();
                files.Reverse();
                return View(files);

            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
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