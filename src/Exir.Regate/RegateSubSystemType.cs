using System;

namespace Exir.Regate
{
    public static class RegateSubSystemType
    {
        public static string Build(int? value = null
            , bool hasOutputSite = false
            , string onChange = "")
        {
            var uniqueId = $"RegateSubSystemType__{Guid.NewGuid().ToString()}";
            var subSystemTypeUrl = "/SubSystemType/GetUserSubSystemTypes/";

            var html = $@"
                <div id='{uniqueId}__SubSystemTypeContainer'>
                    <i class='fa fa-spin fa-spinner'></i>
                </div>                
            ";

            var initScript = @"
                <script>
                    function initRegateSubSystemType(uniqueId, subSystemTypeUrl, currentSubSystemType, hasOutputSite, _hasOnChangeCallback, _onChangeCallback) {
                        var $subSystemTypeContainer = $('#' + uniqueId + '__SubSystemTypeContainer');

                        $.get(subSystemTypeUrl, { hasOutputSite: hasOutputSite }).then(function(response) {
                            var __SubSystemTypes = response;

                            generateDropDowns(__SubSystemTypes);
                        });

                        function generateDropDowns(subSystemTypes) {
                            var $subSystemTypeDropDown = $('<select></select>');
                            $subSystemTypeDropDown.attr('class', 'form-control');
                            $subSystemTypeDropDown.attr('name', 'SubSystemType');
                            $subSystemTypeDropDown.prop('required', true);
                            if (_hasOnChangeCallback) $subSystemTypeDropDown.on('change', function () { window[_onChangeCallback](); });
                            var _shouldFireOnChangeCallback = false;

                            $('<option></option>')
                                .attr('value', '')
                                .text('---')
                                .appendTo($subSystemTypeDropDown);

                            $.each(subSystemTypes, function(i, module) {
                                var $option = $('<option></option>');
                                $option.attr('value', module.Id);
                                $option.text(module.Title);
                                $option.data('jsonModel', module);

                                if (module.Id == currentSubSystemType) {
                                    $option.prop('selected', true);
                                    _shouldFireOnChangeCallback = true;
                                }

                                $option.appendTo($subSystemTypeDropDown);
                            });

                            $subSystemTypeContainer.html('');
                            $subSystemTypeDropDown.appendTo($subSystemTypeContainer);
                                
                            if (_shouldFireOnChangeCallback) window[_onChangeCallback]();
                        }
                    };
                </script>
            ";

            var script = $@"
                <script>
                    initRegateSubSystemType('{uniqueId}', '{subSystemTypeUrl}', '{value}', '{hasOutputSite.ToString().ToLower()}', '{(!string.IsNullOrWhiteSpace(onChange)).ToString().ToLower()}', '{onChange}')
                </script>
            ";

            return html + initScript + script;
        }
    }
}
