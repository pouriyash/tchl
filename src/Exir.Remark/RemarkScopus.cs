using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Exir.Remark
{
    public static class RemarkScopus
    {
        public static string Build(string scopusId, tagType tag = tagType.text)
        {
            if (tag == tagType.link)
                return $@"<a href='https://www.scopus.com/authid/detail.uri?authorId={scopusId}'>Scopus</a>";

            if (tag == tagType.span)
                return $@"<span>https://www.scopus.com/authid/detail.uri?authorId={scopusId}</span>";

            if (tag == tagType.div)
                return $@"<div>https://www.scopus.com/authid/detail.uri?authorId={scopusId}</div>";

            return $@"https://www.scopus.com/authid/detail.uri?authorId={scopusId}";

        }
    }
}
