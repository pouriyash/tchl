using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Tochal.AppService.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayValue(Enum enumName)
        {
            var type = (Type)enumName.GetType();
            var field = type.GetField(enumName.ToString());
            if (field == null)
                return enumName.ToString();
            var display = ((System.ComponentModel.DataAnnotations.DisplayAttribute[])field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)).FirstOrDefault();
            return display != null
                ? @display.GetName()
                : enumName.ToString();
        }

        // TODO : Move it to Web Helpers 
        public static List<SelectListItem> EnumToList(Type enumType, string selectedItem, bool defaultSelectItem = false)
        {
            var items = new List<SelectListItem>();

            if (defaultSelectItem)
            {
                items.Add(new SelectListItem
                {
                    Value = "",
                    Text = "انتخاب کنید",
                    Selected = string.IsNullOrWhiteSpace(selectedItem)
                });
            }

            if (enumType == null)
                return items;

            var values = Enum.GetValues(enumType);
            items.AddRange(from Enum item in values
                select new SelectListItem
                {
                    Value = item.ToString(),
                    Text = GetEnumDisplayValue(item),
                    Selected = selectedItem != null && item.ToString() == selectedItem.ToString()
                });
            return items.ToList();
        }

        public static List<EnumModel> EnumToList(Type enumType)
        {
            var items = new List<EnumModel>();
           
            var values = Enum.GetValues(enumType);
            items.AddRange(from Enum item in values
                select new EnumModel
                {
                    Value =Convert.ToInt32(item),
                    DisplayName = GetEnumDisplayValue(item),
                    Name =item.ToString()
                });

            return items;
        }
               
    }
    public class EnumModel
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public static class EnumHelperCore
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// 
        /// By: Mojtaba Dashtinejad
        /// https://stackoverflow.com/a/25109103/3971911
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null) return "";

            try
            {
                var model = enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>();

                return model?.Name;
            }
            catch (Exception)
            {
                return enumValue.ToString();
            }
        }
    }
}
