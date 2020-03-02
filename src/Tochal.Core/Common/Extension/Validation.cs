
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tochal.Core.DomainModels.SSOT;

namespace Tochal.Core.Common.Extension
{
    public static class Validation
    {
        public static bool IsValidText(this string data)
        {
            return !string.IsNullOrWhiteSpace(data);
        }


        public static bool IsValidJson(this string data, int count = 1)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;

            try {
                var items = JsonConvert.DeserializeObject<List<string>>(data);
                return items.Count >= count;
            }
            catch (Exception) {
                return false;
            }
        }

        public static bool IsValidEmail(this string data)
        {
            return new EmailAddressAttribute().IsValid(data);
        }

        public static bool IsValidNationalCode(this string data)
        {
            return !string.IsNullOrWhiteSpace(data);
        }

        public static bool IsValidUniversityId(this int? data)
        {
            return data != null;
        }

        public static bool IsValidFacultyId(this int? data)
        {
            return data != null;
        }

        public static bool IsValidAcademicRankId(this int? data)
        {
            return data != null;
        }

        public static bool IsValidNumber(this int? data)
        {
            return data != null;
        }
        public static bool IsValidNumber2(this int data)
        {
            return true; // TODO HANDLE THIS !
        }

        public static bool IsValidPrice(this decimal? data)
        {
            return data != null;
        }

        public static bool IsValidPrice(this decimal data)
        {
            return data >= 0;
        }

        public static bool IsValidDate(this DateTime? data)
        {
            return data != null;
        }

        public static bool IsValidDate2(this DateTime data)
        {
            return data != null;
        }

        public static bool IsDegreeId(this DegreeSSOT? degree)
        {
            return degree != null;
        }
    }
}
