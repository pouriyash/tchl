using System.Collections.Generic;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Research.City.Admin.Toolkit
{
    public static class Utility
    {
        public static ContentResult CloseAndRefreshOneLevel()
        {
            const string content = @"<script>
                window.parent.closeAndRefreshOneLevel();
            </script>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }


        public static ContentResult CloseAndRefresh()
        {
            const string content = @"<script>
                window.parent.closeAndRefresh();
            </script>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }

        public static ContentResult CloseAndRedirectOneLevel()
        {
            const string content = @"<script>
                window.parent.closeAndRedirectOneLevel();
            </script>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }

        public static ContentResult CloseAndRedirect(string url)
        {
            var content = $@"<script>
                window.parent.closeAndRedirect('{url}');
            </script>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }

        public static ContentResult CloseFancybox()
        {
            const string content = @"<script>
                window.parent.closeFancybox();
            </script>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }

        public static ContentResult CloseFancyboxAndShowServiceResult(ServiceResult result)
        {
            var resultJson = JsonConvert.SerializeObject(result);

            var content = $@"<script>
                window.parent.closeFancyboxAndShowServiceResult({resultJson});
            </script>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html",
            };
        }
    }
}