using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using Tochal.Core.DomainModels.SSOT; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Tochal.Web.Helpers
{
    public static class Routine2Helper
    {
        public static string RemoveButton(int entityId, Routine2Actions action)
        {
            return $"<script>Routine2.RemoveButton({entityId}, '{action}')</script>";
        }

        public static ContentResult DoAction(int entityId, Routine2Actions action, string description = "")
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                description = WebUtility.HtmlEncode(description);
                description = JavaScriptEncoder.Default.Encode(description);
            }

            var content = $"<script>window.parent.Routine2.DoAction({entityId}, '{action}', \"{description}\");</script>";
            return new ContentResult() { Content = content, ContentType = "text/html; charset=utf-8" };
        }

        public static string SetupDraftLetters()
        {
            return "OfficialLetterTemplate/CreateLetters";
        }

        public static string SendLetterLink(int routineId, string templateKey, int entityId, Routine2Actions routineAction)
        {
            return $"/OfficialLetterTemplate/SendDraftLetters/?RoutineAction={routineAction}&RoutineId={routineId}&TemplateKey={templateKey}&EntityId={entityId}";
        }
    }
}