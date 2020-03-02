using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateRemoveRow
    {
        public static string Build(string url, string closest = "tr")
        {
            var uniqueId = $"RegateRomoveRow__{Guid.NewGuid().ToString().Replace("-", "")}";

            var styleMarkup = $@"
                <style>
                    .regate-remove-row-container.is-disabled {{
                        opacity: 0.5;
                        pointer-events: none;
                    }}

                    #{uniqueId} .fa-spinner {{
                        display: none;
                    }}

                    #{uniqueId}.is-loading .fa-spinner {{
                        display: inline-block;
                    }}
                </style>
            ";

            var htmlMarkup = $@"
                <button class='btn btn-default' id='{uniqueId}'>
                    <span>
                        submit
                    </span>
                    <span class='fa fa-spinner fa-spin'></span>
                </button>
            ";

            var initScript = @"
                <script>
                    function initRegateRemoveRow(selector, url) {
                        var closest = '' || 'tr';

                        $(selector).on('click',
                            function () {
                                var _this = this;
                                $(_this).addClass('is-loading');
                                $.post(url).done(function (res) {
                                    if (res.Succeeded)
                                        $(_this).closest(closest).addClass('regate-remove-row-container').addClass('is-disabled');
                                })
                                .fail(function(res) {
                                    showBeautyMessage(res);
                                })
                                .always(function() {
                                    $(_this).removeClass('is-loading');
                                });
                            }
                        );
                     }
                </script>
            ";

            var scriptMarkup = $@"
                <script>
                    initRegateRemoveRow('#{uniqueId}', '{url}');
                </script>
            ";

            return styleMarkup + htmlMarkup + initScript + scriptMarkup;
        }
    }
}
