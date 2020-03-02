using System;

namespace Exir.Regate
{
    public static class RegateSubSystemTypeWithForeignKey
    {
        public static string Build(string foreignKeyUrl
            , string foreignKeyName
            , int? currentSubSystemType
            , int? currentForeignKey
            , bool hasOutputSite = false
            , bool isRequiredSubSystemType = true
            , bool isRequiredForeignKey = true
            , string onChange = ""
        )
        {
            var uniqueId = $"RegateSubSystemTypeWithForeignKey__{foreignKeyName}__{Guid.NewGuid().ToString()}";
            var subSystemTypeUrl = "/SubSystemType/GetUserSubSystemTypes/";
            // var foreignKeyUrl = "/NewsCategory/GetAllNewsCategoryJson/";
            // var foreignKeyName = "NewsCategoryId";
            // var currentSubSystemType = 10000;
            // var currentForeignKey = 4;

            return $@"

				<div id='{uniqueId}__SubSystemTypeContainer'>
                    <i class='fa fa-spin fa-spinner'></i>
                </div>
                <div id='{uniqueId}__ForeignKeyContainer'></div>

				<div>
				    <script>
                        (function() {{
                            var uniqueId = '{uniqueId}';
                            var subSystemTypeUrl = '{subSystemTypeUrl}';
                            var foreignKeyUrl = '{foreignKeyUrl}';
                            var foreignKeyName = '{foreignKeyName}';
                            var currentSubSystemType = '{currentSubSystemType}';
                            var currentForeignKey = '{currentForeignKey}';
                            var hasOuputSite = '{hasOutputSite.ToString().ToLower()}';
                            var _hasOnChangeCallback = '{(!string.IsNullOrWhiteSpace(onChange)).ToString().ToLower()}';
                            var _onChangeCallback = '{onChange}';

                            var $subSystemTypeContainer = $('#' + uniqueId + '__SubSystemTypeContainer');
                            var $foreignKeyContainer = $('#' + uniqueId + '__ForeignKeyContainer');

                            $.get(subSystemTypeUrl, {{ hasOutputSite: hasOuputSite }}).then(function(response) {{
                                var __SubSystemTypes = response;

                                $.get(foreignKeyUrl).then(function(response) {{
                                    var __ForeignKeyList = response;
                                    generateDropDowns(__SubSystemTypes, __ForeignKeyList);
                                }});
                            }});

                            function generateDropDowns(subSystemTypes, foreignKeyList) {{
                                var $subSystemTypeDropDown = $('<select></select>');
                                $subSystemTypeDropDown.attr('class', 'form-control');
                                $subSystemTypeDropDown.attr('name', 'SubSystemType');
                                $subSystemTypeDropDown.prop('required', {isRequiredSubSystemType.ToString().ToLower()});
                                if (_hasOnChangeCallback) $subSystemTypeDropDown.on('change', function () {{ window[_onChangeCallback](); }});

                                $('<option></option>')
                                    .attr('value', '')
                                    .text('---')
                                    .appendTo($subSystemTypeDropDown);

                                $.each(subSystemTypes, function(i, module) {{
                                    var $option = $('<option></option>');
                                    $option.attr('value', module.Id);
                                    $option.text(module.Title);
                                    $option.data('jsonModel', module);

                                    if (module.Id == currentSubSystemType)
                                        $option.prop('selected', true);

                                    $option.appendTo($subSystemTypeDropDown);
                                }});

                                $subSystemTypeDropDown.on('change', function() {{
                                    var value = this.value;
                                    generateForeignKeyDropDown(foreignKeyList, value);
                                }});

                                generateForeignKeyDropDown(foreignKeyList);

                                $subSystemTypeContainer.html('');
                                $subSystemTypeDropDown.appendTo($subSystemTypeContainer);

                                $subSystemTypeDropDown.change();
                            }}

                            function generateForeignKeyDropDown(foreignKeyList, currentSubSystemType) {{
                                // console.log(foreignKeyList);
                                // console.log(currentSubSystemType);

                                $foreignKeyContainer.html('');

                                var $dropdown = $('<select></select>');
                                $dropdown.attr('name', foreignKeyName);
                                $dropdown.attr('class', 'form-control');
                                $dropdown.prop('required', {isRequiredForeignKey.ToString().ToLower()});

                                $('<option></option>')
                                    .attr('value', '')
                                    .text('---')
                                    .appendTo($dropdown);


                                $.each(foreignKeyList, function(i, item) {{
                                    if (item.SubSystemType != currentSubSystemType) return true;

                                    var $option = $('<option></option>');
                                    $option.attr('value', item.Id);
                                    $option.text(item.Title);

                                    if (item.Id == currentForeignKey)
                                        $option.prop('selected', true);

                                    $option.appendTo($dropdown);
                                }});

                                $dropdown.appendTo($foreignKeyContainer);
                            }}
                        }}());
                    </script>
				</div>

            ";
        }
    }
}
