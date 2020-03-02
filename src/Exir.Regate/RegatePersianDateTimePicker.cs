using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegatePersianDateTimePicker
    {
        public static string Build(string name, DateTime? value = null, string labelName = null, bool isRequired = false)
        {
            var uniqueId = $"RegatePersianDateTimePicker__{Guid.NewGuid().ToString().Replace("-", "")}";
            var required = isRequired ? " required " : "";
            var label = labelName != null ? labelName : "";
            var style = @"
                <link href='/lib/PersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.css' rel='stylesheet' />
                <script src='/lib/PersianDateTimePicker/jalaali.js'></script>
                <script src='/lib/PersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.js'></script>
                <script src='/lib/PersianDateTimePicker/jdate.min.js'></script>

                <style type='text/css'>
                    .persianDateTimePicker {
                        min-width: 355px;
                        font-family: inherit;
                        right: 195px;
                    }
                    @@media (max-width:785px){
                        .persianDateTimePicker {
                            right: 15px;
                        } 
                    }
                    .persianDateTimePicker.bottom > .arrow {
                        display: none;
                    }
                    .persianDateTimePicker [data-target] {
                        direction: ltr;
                        margin: 10px;
                        width: 240px;
                        position: relative;
                    }
                    .persianDateTimePicker [data-check] {
                        direction: ltr;
                        margin: 10px;
                        width: 240px;
                        position: relative;
                    }
                    .persianDateTimePicker .btn.active.focus,
                    .persianDateTimePicker .btn.active:focus,
                    .persianDateTimePicker .btn.focus,
                    .persianDateTimePicker .btn:active.focus,
                    .persianDateTimePicker .btn:active:focus,
                    .persianDateTimePicker .btn:focus {
                        outline: none;
                    }
                    .persianDateTimePicker .table-striped > tbody > tr:nth-of-type(odd) {
                        background-color: unset;
                    }
                    .persianDateTimePicker [data-name='md-datetimepicker-title'] {
                        padding: 10px 15px;
                        font-size: 12px;
                        font-weight: 400;
                        font-family: inherit;
                    }
                    .persianDateTimePicker [data-name='md-datetimepicker-popovercontent'] a[data-name='go-today'] {
                        padding: 10px 15px;
                        font-size: 12px;
                        font-weight: 400;
                        font-family: inherit;
                    }
                    .persianDateTimePicker [data-name='md-persiandatetimepicker-headertable'].table .btn {
                        background: unset;
                        color: #fff;
                        background-color: #009933;
                        padding: 0px 8px;
                        font-size: 14px;
                        height: 30px;
                        border-radius: 12px;
                        text-shadow: unset;
                        font-weight: bold;
                    }
                    .persianDateTimePicker .btn-default.active.focus,
                    .persianDateTimePicker .btn-default.active:focus,
                    .persianDateTimePicker .btn-default.active:hover,
                    .persianDateTimePicker .btn-default:active.focus,
                    .persianDateTimePicker .btn-default:active:focus,
                    .persianDateTimePicker .btn-default:active:hover,
                    .persianDateTimePicker .open > .dropdown-toggle.btn-default.focus,
                    .persianDateTimePicker .open > .dropdown-toggle.btn-default:focus,
                    .persianDateTimePicker .open > .dropdown-toggle.btn-default:hover {
                        background-color: #00cc44;
                        color: #fff;
                    }
                    .persianDateTimePicker .dropdown-menu > li > a:focus,
                    .persianDateTimePicker .dropdown-menu > li > a:hover {
                        background-color: #ff6600;
                        color: #fff;
                        background-image: unset;
                    }
                    .persianDateTimePicker [data-name='md-persiandatetimepicker-weekdaysnames'] {
                        color: #ff6600;
                    }
                    .persianDateTimePicker .text-danger {
                        color: unset;
                    }
                    .persianDateTimePicker [data-name='md-persiandatetimepicker'] table.table td {
                        padding: 5px;
                    }
                    .persianDateTimePicker [data-name='day']:hover {
                        background: #eee;
                    }
                    .persianDateTimePicker [data-name='md-persiandatetimepicker-timepicker'].table input {
                        height: 30px;
                        font-size: 15px;
                        display:none;
                    }
                    [data-name='md-persiandatetimepicker-timepicker']{
                        display:none;
                    }
                    .persianDateTimePicker [data-name='md-persiandatetimepicker'] table.table td {
                        padding: 7px 5px;
                        border-radius: 12px;
                        font-size: 14px;                       
                    }
                    .persianDateTimePicker .bg-info {
                        background-color: #ff6600;
                        color: #fff !important;
                    }
                    .persianDateTimePicker [data-name='day']:hover {
                        background-color: #ff6600;
                        color: #fff !important;
                    }
                    .persianDateTimePicker [data-name='today'] {
                        background-color: #11c7c5;
                    }
                    .persianDateTimePicker__inputBox {
                        position: relative;
                        direction: ltr;
                    }
                    .persianDateTimePicker__clear {
                        position: absolute;
                        right: 20px;
                        top: 10px;
                        cursor: pointer;
                        display: none;
                    }
                    .persianDateTimePicker__clear::before {
                        content: '\f00d';
                    }
                    .persianDateTimePicker__data-check{
                        position:absolute;
                       opacity:0;
                       z-index:-1;
                    }
                </style>
            ";
            var html = $@"
                <div class='persianDateTimePicker__inputBox'>
                    <input class='form-control persianDateTimePicker__data-check' data-check='{uniqueId}' tabindex='-1' {required} type='text' name='{name}'  value='{(value != null ? ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") : "")}'>
                    <input class='form-control' data-target='{uniqueId}' type='text' placeholder='{label}'>
                </div>
            ";

            var initScript = @"
                <script>
                    function initDateTimePicker(target) {
                        var name = target;

                        //init date picker
                        $('[data-target=' + name + ']').MdPersianDateTimePicker({
                            Placement: 'bottom',
                            Trigger: 'focus',
                            EnableTimePicker: true,
                            TargetSelector: '[data-target=' + name + ']',
                            GroupId: '',
                            Format: 'yyyy/MM/dd',
                            EnglishNumber: true,
                        });

                        //init check
                        var jalaliDate = convertor(name)
                        $('[data-target=' + name + ']').val(jalaliDate);
                        var strLength = $('[data-target=' + name + ']').val();

                        if (!(strLength.length == 0)) {
                            checker();
                            $('.persianDateTimePicker__inputBox .persianDateTimePicker__clear').css('display', 'block');
                        }


                        //update checker
                        $('[data-target=' + name + ']').on('change', function () {
                            checker();
                        })

                        // convert miladi date to jalali
                        function convertor(target) {
                            var initDat = $('[data-check=' + name + ']').val();            
                            if (!(initDat === '')) {
                                var JDate = require('jdate');
                                var jdate = new JDate;
                                var initDat = initDat.split(' ');
                                var initTime = initDat[1];           
                                initDat = initDat[0].split('-');                         
                                jdate.setFullYear(initDat[0]);          
                                jdate.setMonth(initDat[1]);
                                jdate.setDate(initDat[2]);
                                initDat = JDate.to_jalali(jdate);
                                var XinitDat = JDate.to_jalali(jdate);

                                if (initDat[1] < 10) {
                                    initDat[1] = '0' + initDat[1];
                                }
                                if (initDat[2] < 10) {
                                    initDat[2] = '0' + initDat[2];
                                }

                                initDat = initDat.join('/');
                                var finalDat = initDat;
                                return finalDat;
                            }
                        }

                        //update checker function
                        function checker() {
                            var strLength = $('[data-target=' + name + ']').val();
                            if (!(strLength.length == 0)) {
                                var jalaliDat = $('[data-target=' + name + ']').MdPersianDateTimePicker('getDate');
                                var year = (jalaliDat).getFullYear();
                                var month = (jalaliDat).getMonth() + 1;
                                var day = (jalaliDat).getDate();
                                var hours = (jalaliDat).getHours();
                                var minutes = (jalaliDat).getMinutes();
                                var Seconds = (jalaliDat).getSeconds();
                                if (month < 10) {
                                    month = '0' + month;
                                }
                                if (day < 10) {
                                    day = '0' + day;
                                }

                                $('[data-check=' + name + ']').val(year + '-' + month + '-' + day + ' ' + hours + ':' + minutes + ':' + Seconds);
                            } else {
                                $('[data-check=' + name + ']').val('');
                                $('.persianDateTimePicker__inputBox .persianDateTimePicker__clear').css('display', 'none');
                            }
            
                        }


                        $('[data-target=' + name + ']').on('change', function () {
                            if (!($(this).val()) == '') {
                                $('.persianDateTimePicker__clear').css('display', 'block');
                            }

                            else {
                                $('.persianDateTimePicker__clear').css('display', 'none');
                            }
                        });

                        $('.persianDateTimePicker__clear').click(function () {
                            $('[data-target=' + name + ']').val('');
                            $('[data-check=' + name + ']').val('');
                            $('.persianDateTimePicker__inputBox .persianDateTimePicker__clear').css('display', 'none');
                        });
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initDateTimePicker('{uniqueId}');
                </script>
            ";

            return style + html + initScript + script;
        }
    }
}