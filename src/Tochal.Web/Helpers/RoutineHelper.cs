using System;
using System.IO;
using System.Net;
using System.Text;
using Tochal.Core.DomainModels.SSOT; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace Tochal.Web.Helpers
{
    public static class RoutineHelper
    {
        public static string RemoveButtonOk(int entityId)
        {
            return $"<script>removeDefaultRoutineButtonOk({entityId})</script>";
        }

        public static string RemoveButtonNext(int entityId)
        {
            return $"<script>removeDefaultRoutineButtonNext({entityId})</script>";
        }

        public static string RemoveButtonCancel(int entityId)
        {
            return $"<script>removeDefaultRoutineButtonCancel({entityId})</script>";
        }

        public static string RemoveButtonEdit(int entityId)
        {
            return $"<script>removeDefaultRoutineButtonEdit({entityId})</script>";
        }

        public static string SubmitCancel(int entityId)
        {
            return $"routineSubmitCancel({entityId})";
        }

        public static ContentResult SubmitCancel(int entityId, bool fromPopup, string description = "")
        {
            description = WebUtility.HtmlEncode(description);

            var content = $"<script>window.parent.routineSubmitCancel({entityId}, \"{description}\");</script>";
            return new ContentResult() { Content = content, ContentType = "text/html; charset=utf-8" };
        }

        public static string SubmitOk(int entityId)
        {
            return $"routineSubmitOk({entityId})";
        }

        public static ContentResult SubmitOk(int entityId, bool fromPopup, string description = "")
        {
            description = WebUtility.HtmlEncode(description);

            var content = $"<script>window.parent.routineSubmitOk({entityId}, \"{description}\");</script>";
            return new ContentResult() { Content = content, ContentType = "text/html; charset=utf-8" };
        }

        public static string SubmitEdit(int entityId, string description = "")
        {
            description = WebUtility.HtmlEncode(description);

            return $"routineSubmitEdit({entityId}, \"{description}\")";
        }

        public static ContentResult SubmitEdit(int entityId, bool fromPopup, string description = "")
        {
            description = WebUtility.HtmlEncode(description);

            var content = $"<script>window.parent.routineSubmitEdit({entityId}, \"{description}\");</script>";
            return new ContentResult() { Content = content, ContentType = "text/html; charset=utf-8" };
        }

        public static string SubmitNext(int entityId)
        {
            return $"routineSubmitNext({entityId})";
        }

        public static ContentResult SubmitNext(int entityId, bool fromPopup, string description = "")
        {
            description = WebUtility.HtmlEncode(description);

            var content = $"<script>window.parent.routineSubmitNext({entityId}, \"{description}\");</script>";
            return new ContentResult() { Content = content, ContentType = "text/html; charset=utf-8" };
        }
        //TODO***
        //public static string ShowDraftLettersAndSendLink(int entityId, string letterKey, int routineId, string redirectUrl = null)
        //{
        //    var entityIdE = AESGCM.SimpleEncryptWithPassword(entityId.ToString(), AppConfiguration.EncryptionSalt);
        //    var letterKeyE = AESGCM.SimpleEncryptWithPassword(letterKey, AppConfiguration.EncryptionSalt);
        //    var routineIdE = AESGCM.SimpleEncryptWithPassword(routineId.ToString(), AppConfiguration.EncryptionSalt);

        //    entityIdE = Uri.EscapeDataString(entityIdE);
        //    letterKeyE = Uri.EscapeDataString(letterKeyE);
        //    routineIdE = Uri.EscapeDataString(routineIdE);
        //    redirectUrl = string.IsNullOrWhiteSpace(redirectUrl) ? "" :  $"&redirectUrl={Uri.EscapeDataString(redirectUrl)}";

        //    var href = $"/Routine/ShowDraftLettersAndSend/?entityIdE={entityIdE}&letterKeyE={letterKeyE}&routineIdE={routineIdE}{redirectUrl}";

        //    return href;
        //}
    }
}