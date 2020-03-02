using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateScopus
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            var required = isRequired ? " required " : "";
            var uniqueId = $"RegateScopus__{Guid.NewGuid().ToString().Replace("-", "")}";

            var html = $@"<input class='form-control' style='font-family: monospace;' dir='ltr' id='{uniqueId}' name='{name}' value='{value}' {required} />";

            const string functionScript = @"
                <script>
                    function initRegateScopus(targetId) {
                        console.log('initRegateScopus', targetId);
                        var $elem = $('#' + targetId);
                        if ($elem.data('initRegateScopus') === 'true') {
                            console.log('initRegateScopus is already called', targetId);
                            return;
                        }

                        function handleEvent() {
                            var value = $(this).val();
                            try {
                                var searchParams = new URLSearchParams(value.split('?')[1]);
                                value = searchParams.get('authorId');
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

            var script = $"<script>initRegateScopus('{uniqueId}');</script>";

            return html + functionScript + script;
        }
    }
}