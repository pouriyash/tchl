using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Exir.Remark
{
    public static class RemarkScholar
    {
        public static string Build(string scholarId)
        {
            return $@"https://scholar.google.com/citations?user={scholarId}";

        }
    }
}
