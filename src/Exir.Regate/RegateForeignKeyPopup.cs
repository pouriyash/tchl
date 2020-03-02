using System;
using Exir.Remark;

namespace Exir.Regate
{
    public static class RegateForeignKeyPopup
    {
        public static string Build(
            string name = "",
            string popupUrl = "",
            string titleAjaxUrl = "",
            int? value = null,
            bool hasPlaceHolderTextbox = false,
            string placeHolderName = "",
            string placeHolderValue = ""
        )
        {

            var uniqueId = $"RegateForeignKeyPopup__{name.Replace(".", "_")}__{Guid.NewGuid().ToString().Replace("-", "")}";
            var hidden = uniqueId + "__hidden";
            var placeholder = uniqueId + "__placeholder";
            var setter = uniqueId + "__setter";

            var popupCenterScriptTag = $@"
                <script>
                    function PopupCenter__{uniqueId}(url, title, w, h) {{
                        // Fixes dual-screen position                         Most browsers      Firefox
                        var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
                        var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

                        var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
                        var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

                        var left = ((width / 2) - (w / 2)) + dualScreenLeft;
                        var top = ((height / 2) - (h / 2)) + dualScreenTop;
                        var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

                        // Puts focus on the newWindow
                        if (window.focus) {{
                            newWindow.focus();
                        }}

                        return false;
                    }}
                </script>
            ";

            var setterScriptTag = $@"
                <script>
                    window['{setter}'] = function(idTitleObject) {{
                        document.getElementById('{hidden}').value = idTitleObject.Id;
                        document.getElementById('{placeholder}').innerText = idTitleObject.Title;
                        document.getElementById('{placeholder}').value = idTitleObject.Title;
                    }}
                </script>
            ";

            var loadInitialValueScriptTag = $@"
                <script>
                    (function() {{
                        var $placeholder = $('#{placeholder}');
                        $.get('{titleAjaxUrl}', {{ id: '{value}' }}).then(function(response) {{
                            $placeholder.text(response.Title || response.title);
                        }});
                    }}());
                </script>
            ";

            var mainAltTextboxScript = $@"
                <script>
                    (function () {{
                        var main = '{hidden}';
                        var alt = '{placeholder}';
                        var mainSelector = $('#' + main);
                        var altSelector = $('#' + alt);
                        var defaultValue = '';
                        var url = '{titleAjaxUrl}';

                        function setMainVal(val) {{
                            mainSelector.val(val);
                        }}

                        function setAltVal(val) {{
                            // console.log(altSelector, val);
                            $(altSelector).val(val);
                        }}

                        function fetchAltText(url, id) {{
                            return $.get(url, {{ id: id }});
                        }}

                        function extractTitleFromResponse(response) {{
                            var title = response['Title'] || response['title'];
                            if (!title) console.warn('there is no Title in response:', response);
                            return title;
                        }}

                        function setLoadingState(state) {{
                            altSelector.attr('disabled', state);
                        }}

                        mainSelector.on('change',
                            function() {{
                                var value = this.value;
                                setLoadingState(true);
                                fetchAltText(url, value).then(function(res) {{
                                    var title = extractTitleFromResponse(res);
                                    setLoadingState(false);
                                    setAltVal(title);
                                }});
                            }}
                        );

                        altSelector.attr('tabindex', -1);

                        // first time
                        if (defaultValue !== '') {{
                            setMainVal(defaultValue);
                            mainSelector.change();
                        }}
                    }}());
                </script>
            ";

            var markup = $@"
                <a class='btn btn-info'
                    href='{popupUrl}?__setter={setter}'
                    onclick='return PopupCenter__{uniqueId}(this.href, ""RegateForeignKeyPopup"", 600, 700)'>انتخاب</a>

                <input type='text' name='{name}' value='{value}' id='{hidden}' style='width: 40px;' />
            ";

            var placeholderMarkup = $"<span id='{placeholder}'>{(value.HasValue ? "<i class='fa fa-spinner fa-spin'></i>" : "")}</span>";

            if (hasPlaceHolderTextbox)
            {
                placeholderMarkup = $"<input id='{placeholder}' name='{placeHolderName}' readonly value='{placeHolderValue}' />";
            }

            return popupCenterScriptTag
                + setterScriptTag 
                + markup 
                + placeholderMarkup
                + mainAltTextboxScript
                + ((value.HasValue && ! hasPlaceHolderTextbox) ? loadInitialValueScriptTag : "");
        }

        public static string Initialize()
        {
            return @"
                <script>
                    window.__qs__RegateForeignKeyPopup = (function (a) {
                        if (a == '') return {};
                        var b = {};
                        for (var i = 0; i < a.length; ++i) {
                            var p = a[i].split('=', 2);
                            if (p.length == 1)
                                b[p[0]] = '';
                            else
                                b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, ' '));
                        }
                        return b;
                    })(window.location.search.substr(1).split('&'));

                    $('body').one('click', '[data-role=""RegateForeignKeyPopup__Handler""]', function () {
                        var $elem = $(this);
                        var id = $elem.attr('data-id');
                        var title = $elem.attr('data-title');

                        var idTitleObject = {
                            Id: id,
                            Title: title
                        };

                        window.opener[__qs__RegateForeignKeyPopup.__setter](idTitleObject);
                        window.close();
                    });
                </script>
            ";
        }

        public static object SelectButton(string id, string title)
        {
            // throw new NotImplementedException();
            return $"<button type='button' data-role='RegateForeignKeyPopup__Handler' data-id='{id}' data-title='{title}' class='btn btn-info'>انتخاب</button>";
        }
    }
}
