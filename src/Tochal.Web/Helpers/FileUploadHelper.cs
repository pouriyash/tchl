using Alamut.Data.Structure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tochal.Web.Helpers
{
    public static class FileUploadHelper
    {
        //فقط باید انواع نوع های عکس و فیلم را بگیرد
        //بررسی امنیت عکس
        //تصویر بند انگشتی
        //محل ذخیره سازی فایل و عکس و فیلم باید جدا شود 
        public static async Task<(ServiceResult, string)> UploadFile(IFormFile file)
        {

            if (file == null || file.Length == 0)
                return (ServiceResult.Error("فایل نادرست است"), string.Empty);
            var picName = Guid.NewGuid() + file.FileName;
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", "repository", "FileUpload", picName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return (ServiceResult.Okay(), picName);
        }

        public static async Task<(ServiceResult, List<string>)> UploadFile(List<IFormFile> files)
        {
            var list = new List<string>();

            foreach (var item in files)
            {
                var model = await UploadFile(item);

                if (model.Item1.Succeed)
                {
                    list.Add(model.Item2);
                }
            }

            return (ServiceResult.Okay(), list);
        }

    }
}
