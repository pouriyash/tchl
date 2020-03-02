using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateResearcherId
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            var required = isRequired ? "required" : "";
            var uniqueId = $"RegateResearcherId__{name.Replace(".", "_")}__{Guid.NewGuid().ToString().Replace("-", "")}";
            var html = $@"<input class='form-control' style='font-family: monospace;' dir='ltr' id='{uniqueId}' name='{name}' value='{value}' {required} />";
            var functionScript = @"
                <script>
                    function initRegateResearcherId(target) {
                        function handleEvent(e) {
                            var pattern = /^(https?:\/\/)?.*researcherid\.com\/rid\/([a-zA-z\-0-9]+)/;
                            if (e && e.type === 'paste') {
                                var clipboardData = e.clipboardData || window.clipboardData;
                                var value = clipboardData.getData('Text');
                            } else
                                var value = $(this).val();

                            value = value.match(pattern);
                            if (value)
                                value = value[2];
                            if (value) {
                                e.preventDefault();
                                $(this).val(value);
                            }
                        }

                        var $elem = $('#' + target);

                        if ($elem.data('initRegateResearcherId') === 'true') {
                            console.log('initRegateResearcherId is already called', targetId);
                            return;
                        }

                        $elem.data('initRegateResearcherId', 'true');


                        $elem[0].addEventListener('paste', handleEvent);
                        $elem.on('blur click', handleEvent);

                        $elem.click();
                    };
                </script>
            ";
            var script = $@"
                <script>
                    initRegateResearcherId('{uniqueId}')
                </script>
            ";
            return html + functionScript + script;
        }
    }
}