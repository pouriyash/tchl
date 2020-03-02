using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Exir.Remark
{
    public static class RemarkCount
    {
        public static string Build(int id, string url)
        {
            return $@"
                <span data-ui='RemarkCount' data-id='{id}'>
                    <i class='fa fa-spinner fa-spin'></i>
                </span>
                <script>
                    $.get('{url}', {{ id: '{id}' }}).then(function(response) {{
                        $('[data-ui=RemarkCount][data-id={id}]').text(response.Count || response);
                    }});
                </script>
            ";

        }
    }
}
