using System;
using System.IO;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Tochal.Web.Helpers
{
    public static class FancyboxHelper
    {
        public static string Button(string width = "", string height = "")
        {
            var widthProperty = string.IsNullOrWhiteSpace(width) ? "" : $" data-width='{width}' ";
            var heightProperty = string.IsNullOrWhiteSpace(height) ? "" : $" data-height='{height}' ";
            return $" data-fancybox data-type='iframe' {widthProperty} {heightProperty} ";
        }

        public static ContentResult CloseAndMessage(ServiceResult result)
        {
            var message = JsonConvert.SerializeObject(result);
            var content = $"<script>window.parent.showBeautyMessage({message}); setTimeout(window.parent.closeFancybox, 10);</script>";
            return new ContentResult() { Content = content, ContentType = "text/html; charset=utf-8" };
        }
    }
}