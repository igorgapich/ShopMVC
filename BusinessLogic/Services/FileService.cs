using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private const string imageFolder = "images";
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task DeleteProductImage(string imagePath)
        {
            string root = _environment.WebRootPath;
            var oldFile = Path.Combine(root, imagePath);
            if (File.Exists(oldFile))
            {
                File.Delete(oldFile);
            }
        }
        public async Task<string> SaveProductImage(IFormFile file)
        {
            // get path to "wwwroot" for ASP.NET Core
            string root = _environment.WebRootPath;
            string newFileName = Guid.NewGuid().ToString(); //random name of file         name.
            string extensionPath = Path.GetExtension(file.FileName); //.jpg .png
            string fullFileName = newFileName + extensionPath; // full name file
            
            // image/picture.png
            string imagePath = Path.Combine(imageFolder, fullFileName);
            string imageFullPath = Path.Combine(root, imagePath);

            //save file on the folder image
            using (FileStream fileStream = new FileStream(imageFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imagePath;
        }

       
    }
}
