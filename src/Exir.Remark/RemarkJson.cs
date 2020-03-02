using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Exir.Remark
{
    public static class RemarkJson
    {
        public static string Build(
              string name
            , string jsonValue = "[]"
            , string items = "{}"
            , bool debug = false
            , bool hasFile = false
            , bool hasImage = false
            , string repositoryWebAddress = ""
        )
        {
            const string vueMarkup = @"
                <table class='table table-bordered table-striped'>
                    <thead>
                        <tr>
                            <th v-for='field in fields' width='25%'>
                                {{ field.Name }}
                            </th>
                            <th v-if='hasFile' width='25%'>
                                فایل
                            </th>
                            <th v-if='hasImage' width='25%'>
                                تصویر
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for='record in value'>
                            <td v-for='field in fields'>
                                {{ field.IsList ? field.ListItemsFlat[record[field.Key]] : record[field.Key] }}
                            </td>
                            <td v-if='hasFile'>
                                <a target='_blank' v-bind:href='repositoryWebAddress + record.File'><i class='fa fa-download'></i></a>
                            </td>
                            <td v-if='hasImage'>
                                <img v-bind:src='repositoryWebAddress + record.File' width='200' />
                            </td>
                        </tr>
                    </tbody>
                </table>
            ";

            const string javascriptFunction = @"
                <script>
                    function initRemarkJson(name, fields, value, hasImage, hasFile, repositoryWebAddress) {
                        hasImage = hasImage || false;
                        hasFile = hasFile || false;
                        repositoryWebAddress = repositoryWebAddress || '';
                        // value = value || [];
                        // fields = fields || {};

                        fields = fields.Fields.map(function(field) {
                            if (field.IsList) {
                                var listItems = {};
                                for (var i = 0; i < field.ListItems.length; i++) {
                                    var item = field.ListItems[i];
                                    listItems[item.Value] = item.Title;
                                }

                                field.ListItemsFlat = listItems;
                            }

                            return field;
                        });

                        var app = new Vue({
                            el: '#' + name,
                            data: {
                                hasImage: hasImage,
                                hasFile: hasFile,
                                repositoryWebAddress: repositoryWebAddress,
                                fields: fields,
                                value: value
                            }
                        });

                        // console.log(app);
                    }
                </script>
            ";

            var htmlMarkup = $"<div id='{name}'>{vueMarkup}</div>";
            var initFunction = $"<script>initRemarkJson('{name}', {(string.IsNullOrWhiteSpace(items) ? "{}" : items)}, {(string.IsNullOrWhiteSpace(jsonValue) ? "[]" : jsonValue)}, {hasImage.ToString().ToLower()}, {hasFile.ToString().ToLower()}, '{repositoryWebAddress}');</script>";



            return $"{javascriptFunction} {htmlMarkup} {initFunction}";
        }
    }
}
