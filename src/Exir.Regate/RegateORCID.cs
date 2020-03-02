using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateORCID
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            var required = isRequired ? " required " : "";
            var uniqueId = $"RegateORCID__{Guid.NewGuid().ToString().Replace("-", "")}";
            var html = $@"<input class='form-control' style='font-family: monospace;' dir='ltr' id='{uniqueId}' name='{name}' value='{value}' {required} />";
            const string functionScript = @"
                <script>
                    function initRegateORCID(targetId) {
                        console.log('initRegateORCID', targetId);
                        var $elem = $('#' + targetId);
                        if ($elem.data('initRegateORCID') === 'true') {
                            console.log('initRegateORCID is already called', targetId);
                            return;
                        }

                        $elem.data('initRegateORCID', 'true');

                        function handleEvent() {
                            console.log('handleEvent');
                            var pattern = /(([0-9]{4}[-]){3}[0-9]{4})/g;
                            var value = $(this).val();

                            var orcid = value.match(pattern);
                            if (orcid) value = orcid[0];

                            $(this).val(value);
                        }

        
                        $elem.on('input blur click', handleEvent);
                        handleEvent.bind($elem[0])();
                    };
                </script>
            ";

            var script = $"<script>initRegateORCID('{uniqueId}');</script>";
            return html + functionScript + script;
        }
    }
}