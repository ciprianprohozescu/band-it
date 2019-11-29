using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientMVC.Controllers
{
    public class FileController
    {
        string[] acceptedImageExtensions = { ".jpg", ".png", ".bmp", ".gif" };
        string[] acceptedMusicExtensions = { ".mp3", ".wav" };
        int maxFileSize = 5000000;

        public void SaveFile(string ownerType, int ownerID, string fileType, HttpPostedFileBase file)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            if (file == null || file.ContentLength == 0)
            {
                return;
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var fileSize = file.ContentLength;

            if (fileType == "profilePicture" || fileType == "image")
            {
                if (!acceptedImageExtensions.Contains(fileExtension))
                {
                    return;
                }
            }
            else
            {
                if (!acceptedMusicExtensions.Contains(fileExtension))
                {
                    return;
                }
            }

            if (fileSize > maxFileSize)
            {
                return;
            }

            if (ownerType == "user")
            {
                var folder = HttpContext.Current.Server.MapPath($"~/Content/Uploads/Users/{ownerID}/");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, fileName);

                file.SaveAs(path);

                if (fileType == "profilePicture")
                {
                    var request = new RestRequest("user/update/profilepicture", Method.GET);
                    request.AddParameter("ownerID", ownerID);
                    request.AddParameter("fileName", fileName);

                    client.Execute(request);
                    Console.WriteLine();
                }
            }
        }
    }
}