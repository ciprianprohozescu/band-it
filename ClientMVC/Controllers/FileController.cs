using Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using FileLogic = Models.File;

namespace ClientMVC.Controllers
{
    public class FileController
    {
        string[] acceptedImageExtensions = { ".jpg", ".png", ".bmp", ".gif" };
        string[] acceptedMusicExtensions = { ".mp3", ".wav", ".ogg" };
        int maxFileSize = 5000000;

        private IWebHostEnvironment _env;
        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void SaveFile(string ownerType, int ownerID, string fileType, IFormFile file)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            if (file == null || file.Length == 0)
            {
                return;
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var fileSize = file.Length;

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
                var folder = $"{_env.WebRootPath}/Content/Uploads/Users/{ownerID}/";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, fileName);
                var fileStream = new FileStream(path, FileMode.Create);
                file.CopyTo(fileStream);

                if (fileType == "profilePicture")
                {
                    var user = new User();
                    user.ID = ownerID;
                    user.ProfilePicture = fileName;

                    var request = new RestRequest("user/update/profilepicture", Method.PUT);
                    request.AddJsonBody(user);

                    client.Execute(request);
                } else
                {
                    var fileLogic = new FileLogic();
                    fileLogic.Name = fileName;

                    var user = new User();
                    user.ID = ownerID;
                    user.Files = new List<FileLogic>();
                    user.Files.Add(fileLogic);

                    var request = new RestRequest("user/add/file", Method.POST);
                    request.AddJsonBody(user);

                    client.Execute(request);
                }
            }
        }

        public void DeleteFile(string ownerType, int ownerID, int fileID, string fileName)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            fileName = Path.GetFileName(fileName);

            if (ownerType == "user")
            {
                var folder = $"{_env.WebRootPath}/Content/Uploads/Users/{ownerID}/";
                var path = Path.Combine(folder, fileName);
                System.IO.File.Delete(path);

                var request = new RestRequest($"user/delete/file/{fileID}", Method.DELETE);

                client.Execute(request);
            }
        }

        public List<FileLogic> GetImages(List<FileLogic> files)
        {
            var images = new List<FileLogic>();

            foreach (var file in files)
            {
                if (acceptedImageExtensions.Contains(Path.GetExtension(file.Name)))
                {
                    images.Add(file);
                }
            }

            return images;
        }

        public List<FileLogic> GetMusic(List<FileLogic> files)
        {
            var music = new List<FileLogic>();

            foreach (var file in files)
            {
                if (acceptedMusicExtensions.Contains(Path.GetExtension(file.Name)))
                {
                    music.Add(file);
                }
            }

            return music;
        }
    }
}