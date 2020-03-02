using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateScholar
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            var required = isRequired ? " required " : "";
            var uniqueId = $"RegateScholar__{Guid.NewGuid().ToString().Replace("-", "")}";

            var html = $@"<input class='form-control' style='font-family: monospace;' dir='ltr' id='{uniqueId}' name='{name}' value='{value}' {required} />";

            const string functionScript = @"
                <script>
                    function initRegateScholar(targetId) {
                        console.log('initRegateScholar', targetId);
                        var $elem = $('#' + targetId);
                        if ($elem.data('initRegateScholar') === 'true') {
                            console.log('initRegateScholar is already called', targetId);
                            return;
                        }

                        function handleEvent() {
                            var value = $(this).val();
                            try {
                                var searchParams = new URLSearchParams(value.split('?')[1]);
                                value = searchParams.get('user');
                                if (value) $(this).val(value);
                            } catch (err) {
                                console.warn(err);
                            }
                        }

                        $elem.on('input blur click', handleEvent);
                        handleEvent.bind($elem[0])();
                    }
                </script>
            ";

            var script = $"<script>initRegateScholar('{uniqueId}');</script>";

            return html + functionScript + script;
        }
    }
}