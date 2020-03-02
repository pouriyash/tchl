using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alamut.Data.Structure;
using HeyRed.Mime;
using Tochal.Core.DomainModels.SSOT;
using Microsoft.AspNetCore.Http;
using FileType = Tochal.Core.DomainModels.SSOT.FileType;

namespace Tochal.Web.Helpers
{
    public static class FileHelper
    {
        public static string SaveFile(IFormFile file, FileConfig config, FileType fileType)
        {
            if (file.Length <= 0)
            {
                throw new Exception("there is no content in uploaded file.");
            }

            var date = DateTime.Now;
            var relativePath = $"{fileType}/{date.Year}/{date.Month}/{date.Day}/";
            var folderPath = Path.Combine(config.PhysicalAddress, relativePath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            Directory.CreateDirectory(folderPath);
            var filepath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            //Check Mime Type
            var allowedExtensions = new List<string>
            {
                "image/png",
                "image/tiff",
                "image/jpeg",
                "image/bmp",
                "image/gif",
            };

            if (fileType == FileType.file)
            {
                allowedExtensions.Add("application/msword");
                allowedExtensions.Add("application/zip");
                allowedExtensions.Add("application/x-rar-compressed");
                allowedExtensions.Add("application/pdf");
                allowedExtensions.Add("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }

            var mimeType = MimeGuesser.GuessMimeType(filepath);

            if (allowedExtensions.Contains(mimeType))
                return Path.Combine(relativePath, fileName);


            try
            {
                File.Delete(filepath);
            }
            catch (IOException)
            {

            }

            throw new Exception("فایل مورد نظر غیر مجاز می‌باشد");
        }
    }
}