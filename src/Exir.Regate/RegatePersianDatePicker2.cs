using System;
using DNTPersianUtils.Core;

namespace Exir.Regate
{
    public static class RegatePersianDatePicker2
    {
        public static string Build(string name, DateTime? value = null, bool isRequired = true)
        {
            var uniqueId = "RegatePersianDatePicker2__" + name + "__" + Guid.NewGuid().ToString().Replace("-", "");
            var uniqueIdPicker = uniqueId + "__picker";
            var uniqueIdHidden = uniqueId + "__hidden";
            var uniqueIdClear = uniqueId + "__clear";

            var defaultDateJalali = "";

            if (value != null)
                defaultDateJalali = ((DateTime) value).ToPersianDateTimeString("yyyy/MM/dd");


            var markup = $@"

                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars.min.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars.plus.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars.persian.min.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars.persian-fa.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.plugin.min.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars.picker.min.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars-fa.js'></script>
                <script src='/lib/jquery-calendars-2.0.0/jquery.calendars.picker-fa.js'></script>
                <link rel='stylesheet' type='text/css' href='/lib/jquery-calendars-2.0.0/jquery.calendars.picker.css' />

                <style>
                    .calendar-container {{
                        position: relative;
                    }}
                     .calendar-container > .form-control {{
                        padding-left: 30px;
                     }}
                    .calendar-container [data-role=clear] {{
                        position: absolute;
                        left: 10px;
                        top: 10px;
                        cursor: pointer;
                        display: none;
                    }}
                </style>


                <div class='calendar-container'>
                    <input type='text'
                        {(isRequired ? " required='required' " : "")}
                        id='{uniqueIdHidden}'
                        name='{name}'
                        style='opacity: 0; position: absolute; z-index: -1; width: 0; height: 0; pointer-events: none; overflow: hidden; margin-right: 10px; marign-top: 5px;' tabindex='-1'
                    />

                    <input type='text'
                        id='{uniqueIdPicker}'
                        readonly='readonly'
                        class='form-control'
                        data-role='persian-date-picker'
                    />
                    <i id='{uniqueIdClear}' class='fa fa-times' data-role='clear'></i>
                </div>

                
            ";

            var initScript = @"
                <script>
                    function initRegatePersianDatePicker2(name, uniqueId, uniqueIdPicker, uniqueIdHidden, uniqueIdClear, defaultDateJalali) {
                        var $picker = $('#' + uniqueIdPicker);
                        var $hidden = $('#' + uniqueIdHidden);
                        var $clear = $('#' + uniqueIdClear);
                        var defaultDate = defaultDateJalali;

                        var _defaultDate = defaultDate;
                        var _dateFormat = 'yyyy/mm/dd';
                        var _hiddenDateFormat = 'yyyy-mm-dd 00:00:00';

                        var options = {
                            calendar: $.calendars.instance('persian', 'fa'),
                            dateFormat: _dateFormat,
                            selectDefaultDate: true,
                            useMouseWheel: false,
                            changeMonth: true,
                            showSpeed: 0,
                            onSelect: function(dates) {
                                var jd = dates[0].toJD();

                                var date = $.calendars.instance('gregorian').fromJD(jd);
                                $hidden.val(date.formatDate(_hiddenDateFormat));

                                $clear.css('display', 'block');
                            }
                        };

                        if (_defaultDate != '') {
                            options.defaultDate = _defaultDate;
                        }

                        $picker.calendarsPicker(options);

                        $clear.on('click' ,
                            function () {
                                $picker.val('');
                                $hidden.val('');
                                $(this).css('display', 'none');
                            }
                        );
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegatePersianDatePicker2('{name}', '{uniqueId}', '{uniqueIdPicker}', '{uniqueIdHidden}', '{uniqueIdClear}', '{defaultDateJalali}')
                </script>
            ";

            return markup + initScript + script;
        }
    }
}