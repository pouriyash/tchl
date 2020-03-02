using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateContenteditable
    {
        public static string Build(string name = "" , string value = "")
        {
            var uniqueId = $"RegateContenteditable__{name}__{Guid.NewGuid().ToString()}";
            value = WebUtility.HtmlEncode(value);

            var funcScriptTag = @"
                <script>
                    function makeTextareaContenteditable(name) {
                        var textareaValue = $('[data-textarea=\'' + name + '\']').val();
                        $('[data-textarea=\'' + name + '\']').css('display', 'none');
                        var divhtml = '<div  contenteditable=\'true\' data-content=\'' + name + '\'>' + textareaValue + '</div>';
                        $('[data-textarea=\'' + name + '\']').after(divhtml);
                        $('[data-content=\'' + name + '\']').on('input change',
                            function () {
                                var name = $(this).attr('data-content');
                                var textareaVal = $(this).html();
                                $('[data-textarea=\'' + name + '\']').val(textareaVal);
                            });
                    }
                </script>
            ";

            var textareaTag = $"<textarea data-textarea='{name}' name='{name}'>{value}</textarea>";

            var finalMarkup = $@"
                {textareaTag}
                {funcScriptTag}
                <script>makeTextareaContenteditable('{name}');</script>
            ";

            return finalMarkup;
        }
    }
}
