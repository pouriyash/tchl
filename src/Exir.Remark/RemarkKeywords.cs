using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Exir.Remark
{
    public static class RemarkKeywords
    {
        public static string Build(string jsonValue = "", string delimiter = "، ")
        {
            if (string.IsNullOrWhiteSpace(jsonValue)) return "";

            try
            {
                var list = JsonConvert.DeserializeObject<List<string>>(jsonValue);
                return string.Join(delimiter, list);
            }
            catch (Exception)
            {
                return jsonValue;
            }
        }
    }
}
