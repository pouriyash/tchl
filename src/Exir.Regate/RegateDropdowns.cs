using System;

namespace Exir.Regate
{
    public static class RegateDropdowns
    {
        public static string Build(
            string name = "",
            string url = "",
            string placeholder = "",
            string selectedValue = "",
            string sendAjaxValue = "",
            string sendAjaxParam = "id",
            string relatedDropdownName = "",
            bool required = true,
            string idKey = "id",
            string titleKey = "title",
            string callback = "",
            bool disablePlaceholder = false,
            bool initSelect2 = false,
            bool shouldCallbackOnInitial = false,
            bool disabled = false
        )
        {
            var uniqueId = $"RegateDropdowns__{name}__{Guid.NewGuid().ToString()}";

            return $@"
				<div style='text-align: center;'
                     data-cdd='{name}'
                     data-url='{url}'
                     data-placeholder='{placeholder}'
                     data-id-key='{idKey}'
                     data-title-key='{titleKey}'
                     data-selected-value='{selectedValue}'
                     data-sendajax-value='{sendAjaxValue}'
                     data-sendajax-param='{sendAjaxParam}'
                     data-callback='{callback}'
                     {(shouldCallbackOnInitial ? "data-shouldcallbackoninitial='true'" : "")}
                     data-disable-placeholder='{disablePlaceholder.ToString().ToLower()}'
                     {(!string.IsNullOrWhiteSpace(relatedDropdownName) ? $"data-has-related-dropdown='{relatedDropdownName}'" : "")}
                >
                    <i class='fa fa-spin fa-spinner' data-role='loader' style='position: absolute; z-index: 100; display: none;'></i>
                    <select name='{name}' {(initSelect2 ? " data-role='select2' " : "")} {(required ? "required='required'" : "")} placeholder='{placeholder}' class='form-control'{(disabled == true ? "disabled" : "")}></select>
                </div>
            ";
        }

        public static string Initialize()
        {
            // return "";

            return @"
                <script>
                    $(function () { 
                        if (window.__RegateDropdownsInitialized) {
                            console.log('tried to initialize RegateDropdowns more than once. Please check your page.');
                            return;
                        }

                        window.__RegateDropdownsInitialized = true;

                        function fillDropdown(name) {

                            console.log('build dropdown for :', name);
                            var $container = $('[data-cdd=\'' + name + '\']');

                            if ($container.length == 0) return;

                            var $dd = $container.find('select');
                            var $select2 = $container.find('.select2');
                            var $loader = $container.find('[data-role=\'loader\']');
                            var url = $container.attr('data-url');
                            var sendajaxValue = $container.attr('data-sendajax-value');
                            var sendajaxParam = $container.attr('data-sendajax-param');
                            var selectedValue = $container.attr('data-selected-value');
                            var placeholder = $container.attr('data-placeholder');
                            var idKey = $container.attr('data-id-key');
                            var titleKey = $container.attr('data-title-key');
                            var callback = $container.attr('data-callback');
                            var shouldCallbackOnInitial = $container.attr('data-shouldcallbackoninitial');

                            // loading
                            $dd.hide();
                            $select2.hide();
                            $loader.hide();

                            hideRelatedDropdowns(name);

                            if (sendajaxValue == '') return;

                            $loader.show();

                            // send ajax request
                            var data = {};
                            data[sendajaxParam] = sendajaxValue;

                            //var req = $.get(url, data);
                            var req = httpGet(url,data);

                                 // the response is ready
                                // hide loader
                                $loader.hide();


                                // empty dropdown
                                $dd.find('option').remove();

                                // create placeholder option
                                var $optionPlaceholder = $('<option disabled selected></option>')
                                    .attr('value', '0')
                                    .text((placeholder || 'انتخاب کنید'))
                                    .appendTo($dd);
                                   debugger;
                                if (selectedValue === 'FIRST') {
                                    $optionPlaceholder.prop('disabled', true)
                                }

                                // create options
                                var shouldSelectFirstItem = false;
                                   $.each(JSON.parse(req), function (i, item) {
                                    var id = item.id;
                                    var title = item.title;
                                    //console.log(idKey, titleKey, id, title);


                                    var $option = $('<option></option>')
                                        .attr('value', id)
                                        .text(title);

                                    // select current value
                                    if (selectedValue === 'FIRST') {
                                        shouldSelectFirstItem = true;
                                    } else {
                                        if (selectedValue == id) {
                                            $option.prop('selected', true);
                                            setRelatedDropdownValue(name, selectedValue);
                                            fillRelatedDropdown(name);
                                            if (callback) window[callback]();
                                        }
                                    }

                                    $option.appendTo($dd);
                                    });


                                if (shouldSelectFirstItem) {
                                    var $option = $dd.find('option[value!=\'\']').eq(0);
                                    var value = $option.val();

                                    $option.prop('selected', true);
                                    setRelatedDropdownValue(name, value);
                                    fillRelatedDropdown(name);
                                    if (callback) window[callback]();
                                    // console.log(value);
                                }

                                if (shouldCallbackOnInitial) window[callback]();

                                // show dropdown
                                $dd.show();
                                $select2.show();
    }
function httpGet(url,data)
{
var params = new URLSearchParams(data).toString();

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open( 'GET', url + '?' + params, false ); // false for synchronous request
    xmlHttp.send();
            return xmlHttp.responseText;

        }
                        function hideRelatedDropdowns(parentDropdownName) {
                            // console.log('hideRelatedDropdowns', parentDropdownName);
                            setRelatedDropdownValue(parentDropdownName, '');
                            fillRelatedDropdown(parentDropdownName);

                            // nested
                            var $parent = $('[data-cdd=\'' + parentDropdownName + '\'][data-has-related-dropdown]');
                            var relatedDropdownName = $parent.attr('data-has-related-dropdown');

                            if (relatedDropdownName)
                                hideRelatedDropdowns(relatedDropdownName);
                        }

                        function setRelatedDropdownValue(parentDropdownName, parentValue) {
                            var $parent = $('[data-cdd=\'' + parentDropdownName + '\'][data-has-related-dropdown]');
                            var relatedDropdownName = $parent.attr('data-has-related-dropdown');
                            var $relatedDropdownContainer = $('[data-cdd=\'' + relatedDropdownName + '\']');
                            $relatedDropdownContainer.attr('data-sendajax-value', parentValue);
                        }

                        function fillRelatedDropdown(parentDropdownName) {
                            var $parent = $('[data-cdd=\'' + parentDropdownName + '\'][data-has-related-dropdown]');
                            var relatedDropdownName = $parent.attr('data-has-related-dropdown');
                            fillDropdown(relatedDropdownName);
                        }

                        // get all dropdowns
                        var $dropdowns = $('[data-cdd]');
                        $dropdowns.each(function () {
                            var name = $(this).attr('data-cdd');
                            fillDropdown(name);
                        });

                        // attach onchange event
                        $('body').on('change', '[data-cdd][data-has-related-dropdown]', function () {
                            var $container = $(this);
                            var name = $container.attr('data-cdd');
                            var callback = $container.attr('data-callback');
                            var $dd = $container.find('select');
                            var value = $dd.val();
                            
                            // callback
                            if (callback) {
                                console.log(callback);
                            }

                            // we should fill the related dropdown
                            setRelatedDropdownValue(name, value);
                            fillRelatedDropdown(name);
                        });

                        // callback handling
                        $('body').on('change', '[data-cdd]', function() {
                            var $container = $(this);
                            var callback = $container.attr('data-callback');

                            if (callback)
                                window[callback]();
                        });
                    });
                </script>
            ";
        }
    }
}
