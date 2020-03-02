using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;

namespace Exir.Regate
{
    public static class RegateJson
    {
        public static string Build(
              string name
            , string jsonValue = "[]"
            , RegateJsonOptions items = null
            , bool debug = false
            , bool hasFile = false
            , bool hasImage = false
            , string repositoryWebAddress = ""
        )
        {
            #region StyleMarkup
            var styleMarkup = @"
                <style>
                    .sam-handler {
                        width: 10px;
                        position: relative;
                        font-size: 0;
                        line-height: 0;
                        display: inline-block;
                        cursor: move;
                        white-space: normal;
                    }

                    .sam-handler-elem {
                        display: inline-block;
                        background: gray;
                        transition-property: background-color;
                    }

                    .sam-handler-dotted .sam-handler-elem {
                        width: 3px;
                        height: 3px;
                        border-radius: 50%;
                        margin: 1px;
                    }


                    .sam-handler:hover .sam-handler-elem {
                        background-color: #333;
                    }

                    .table > tbody > tr > td {
                        padding: 0px 5px;
                    }

                    .input-group .form-control:last-child {
                        margin-right: -2px;
                    }

                    .input-group-addon {
                        border-color: #d0d0d0 !important;
                        font-size: 12px;
                        font-weight: 500;
                        padding: 6px;
                        text-align: right;
                    }

                    .form-control {
                        background-color: #fff !important;
                    }
                </style>
            ";
            #endregion

            #region NeededLibs
            var neededLibs = @"
                <script src='/lib/angular-1.5.7/angular.min.js'></script>
                <script src='/lib/underscore-1.8.3/underscore-min.js'></script>
                <script src='/lib/jquery-ui-1.11.4/jquery-ui.min.js'></script>
                <script src='/lib/sortable-1.4.2/sortable.min.js'></script>
                <script src='/lib/sortable-1.4.2/ng-sortable.js'></script>
                <script src='/lib/angular-ui-sortable-0.14.2/sortable.js'></script>
            ";
            #endregion

            #region Definition Script
            var definitionScript = @"
                <script>
                    function initRegateJson(name, value) {

                        window['__setFile_' + name + '__File'] = function (file) {
                            window.__file = file;
                            $('#' + name + ' [data-role=\'addFile\']').click();
                        };

                        window['__setImage_' + name + '__Image'] = function (file) {
                            window.__file = file;
                            $('#' + name + ' [data-role=\'addImage\']').click();
                        };

                        (function () {
                            // get Razor Model as JSON and make it more humanity
                            var _jsonData = value;

                            // the angularAppUniqueId which we need for this section
                            var _angularAppUniqueId = name;

                            // create angular app
                            var app = angular.module(_angularAppUniqueId, ['ng-sortable']);

                            // create angular module
                            app.controller('Controller', function ($scope, $http) {
                                var _model = this;

                                _model.SortableConfig = { animation: 150, handle: '.module-field--handler' };

                                _model.Items = (_jsonData.constructor.name === Array.name) ? (_jsonData || []) : [];

                                _model.removeItem = function (index) {
                                    // var product = model.Items[index];
                                    // console.log(product.title);
                                    _model.Items.splice(index, 1);
                                };

                                _model.addItem = function () {
                                    _model.Items.push({});
                                };

                                _model.chooseFile = function () {
                                    var file = window.__file;
                                    file.Id = file.Id || file._id;

                                    _model.Items[_model.currentFileIndex].File = file;
                                };

                                _model.chooseFileCurrent = function (index) {
                                    _model.currentFileIndex = index;
                                    $('#' + name + ' [data-role=\'RegateFileContainer\'] input + a').click();
                                };

                                _model.chooseImage = function () {
                                    var file = window.__file;
                                    file.Id = file.Id || file._id;

                                    _model.Items[_model.currentImageIndex].Image = file;
                                };

                                _model.chooseImageCurrent = function (index) {
                                    _model.currentImageIndex = index;
                                    $('#' + name + ' [data-role=\'RegateImageContainer\'] input + a').click();
                                };
                            });

                            var elem = document.getElementById(_angularAppUniqueId);
                            angular.bootstrap(elem, [_angularAppUniqueId]);
                        }());
                    }

                </script>
            ";
            #endregion

            #region HTML Fields
            var htmlFields = "<td class=''>";

            var fileMarkup = $@"
                <td class='text-center' width='20%'>
                    <div class='m-b-sm' ng-if='item.File.length'>
                        <a href='{repositoryWebAddress}{{{{ item.File }}}}' target='_blank'>مشاهده فایل</a>
                    </div>

                    <div class='caption' style='position: relative;'>
                        <input type='text' class='form-control' dir='ltr' required ng-model='item.File' style='position: absolute; pointer-events: none; z-index: -1; opacity: 0;' tabindex='-1' />
                        <button type='button' class='btn btn-info' ng-click='Model.chooseFileCurrent($index)'>انتخاب فایل</button>
                    </div>
                </td>
            ";

            var imageMarkup = $@"
                <td class='text-center' width='20%'>
                    <div class='m-b-sm' ng-if='item.Image.length'>
                        <img ng-src='{repositoryWebAddress}{{{{ item.Image }}}}' style='max-width: 64px; max-height: 64px;' />
                    </div>

                    <div class='caption' style='position: relative;'>
                        <input type='text' class='form-control' dir='ltr' required ng-model='item.Image' style='position: absolute; pointer-events: none; z-index: -1; opacity: 0;' tabindex='-1' />
                        <button type='button' class='btn btn-info' ng-click='Model.chooseImageCurrent($index)'>انتخاب تصویر</button>
                    </div>
                </td>
            ";

            items?.Fields.ForEach(field =>
            {
                var formControl = $"<input type='text' class='form-control' ng-model='item.{field.Key}'>";

                if (field.IsList)
                {
                    formControl = $"<select class='form-control' required ng-model='item.{field.Key}'>";
                    formControl += "<option value=''>---</option>";

                    field.ListItems.ForEach(listItem =>
                    {
                        formControl += $"<option value='{listItem.Value}'>{listItem.Title}</option>";
                    });

                    formControl += "</select>";
                }

                htmlFields += $@"
                    <div><div class='input-group'>
                        <span class='input-group-addon'>{field.Name}</span>
                        {formControl}
                    </div></div>
                ";
            });
            htmlFields += "</td>";
            #endregion

            #region HtmlMarkup
            var htmlMarkup = $@"
                <div id='{name}' ng-controller='Controller as Model' ng-cloak>

                    <textarea name='{name}' class='form-control' style='direction: ltr; font-family: monospace; height: 300px; {(!debug ? " display: none; " : "")}' ng-bind='Model.Items|json'></textarea>

                    <div class='container-fluid container-fullw bg-white'>

                        <!-- [items] -->
                        <div class='row' ng-sortable='Model.SortableConfig'>
                            <div ng-repeat='item in Model.Items' class='_col-xs-12'>
                                <table class='table table-striped table-bordered table-valigned m-b-sm '>
                                    <tbody>
                                        <tr>
                                            <!-- handler -->
                                            <td class='text-center' width='2%'>
                                                <span class='sam-handler sam-handler-dotted module-field--handler'>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                    <span class='sam-handler-elem'></span>
                                                </span>
                                            </td>

                                            <!-- item number -->
                                            <td class='text-center' width='1%'>
                                                <span ng-bind='$index + 1'></span>
                                            </td>

                                            {(hasFile ? fileMarkup : "")}
                                            {(hasImage ? imageMarkup : "")}
                                            {htmlFields}

                                            <!-- item delete -->
                                            <td class='text-center' width='1%'>
                                                <button tabindex='-1' type='button' class='text-danger exir--transparent-btn' ng-click='Model.removeItem($index)'>
                                                    <i class='pe-7s-close-circle largeicon'></i>
                                                </button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- [/items] -->
                        <div class='row'>
                            <div class='btn btn-default pull-right m-b' ng-click='Model.addItem();'>
                                اضافه کردن آیتم جدید
                            </div>
                        </div>


                        <div style='display: none;'>
                            <button type='button' data-role='addFile' ng-click='Model.chooseFile()'></button>
                            <div data-role='RegateFileContainer'>
                                {RegateFile.Build($"{name}__File")}
                            </div>
                        </div>

                        <div style='display: none;'>
                            <button type='button' data-role='addImage' ng-click='Model.chooseImage()'></button>
                            <div data-role='RegateImageContainer'>
                                {RegateImage.Build($"{name}__Image")}
                            </div>
                        </div>
                    </div>

                </div>
            ";
            #endregion


            var finalMarkup = $@"
                {styleMarkup}
                {neededLibs}
                {definitionScript}
                {htmlMarkup}
                <script>try {{ initRegateJson('{name}', {(string.IsNullOrWhiteSpace(jsonValue) ? "[]" : jsonValue)}); }} catch (ex) {{ initRegateJson('{name}', '[]'); }}</script>
            ";

            return finalMarkup;
        }

        public static List<RegateJsonListItem> CreateListFromSSOT(Type ssot)
        {
            var list = new List<RegateJsonListItem>();

            var items = RegateJsonEnumToList(ssot).ToList();
            items.ForEach(item =>
            {
                list.Add(new RegateJsonListItem(item.Value, item.DisplayName));
            });

            return list;
        }

        public static List<RegateJsonEnumModel> RegateJsonEnumToList(Type enumType)
        {
            var items = new List<RegateJsonEnumModel>();

            var values = Enum.GetValues(enumType);
            items.AddRange(from Enum item in values
                select new RegateJsonEnumModel
                {
                    Value = Convert.ToInt32(item).ToString(),
                    DisplayName = RegateJsonGetEnumDisplayValue(item),
                    Name = item.ToString()
                });

            return items;
        }

        public static string RegateJsonGetEnumDisplayValue(Enum enumName)
        {
            var type = (Type)enumName.GetType();
            var field = type.GetField(enumName.ToString());
            if (field == null)
                return enumName.ToString();
            var display = ((System.ComponentModel.DataAnnotations.DisplayAttribute[])field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)).FirstOrDefault();
            return display != null
                ? @display.GetName()
                : enumName.ToString();
        }
    }

    public class RegateJsonEnumModel
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class RegateJsonOptions
    {
        public List<RegateJsonField> Fields { get; set; }
    }

    public class RegateJsonField
    {
        public RegateJsonField(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsList { get; set; }
        public List<RegateJsonListItem> ListItems { get; set; }
    }

    public class RegateJsonListItem
    {
        public RegateJsonListItem(string value, string title)
        {
            Value = value;
            Title = title;
        }

        public string Value { get; set; }
        public string Title { get; set; }
    }


}