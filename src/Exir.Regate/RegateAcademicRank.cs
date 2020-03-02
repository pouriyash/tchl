using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Exir.Regate
{
    public static class RegateAcademicRank
    {
        public static string BuildEnum<TEnum>(string name, int? value)
        {
            return Build(name, value, Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(v => v.ToString()));
        }


        public static string Build(string name = "AcademicRankId", int? value = null, IEnumerable<string> whiteList = null, bool isRequired = true)
        {
            var required = isRequired ? " required " : "";
            var uniqueId = $"RegateAcademicRank__{Guid.NewGuid().ToString().Replace("-", "")}";

            var whiteListJsonString = "[]";

            if (whiteList != null)
            {
                whiteListJsonString = JsonConvert.SerializeObject(whiteList);
            }

            var html = $@"
                <div id='{uniqueId}'>
                    <input type='hidden' name='{name}' v-bind:value='currentValue' />

                    <select class='form-control' v-model='currentValue' {required}>
                        <option v-for='item in list' v-bind:value='item.Id' v-bind:selected='item.Id == currentValue'>{{{{ item.Title }}}}</option>
                    </select>
                </div>
            ";

            var initScript = @"
                <script>
                    function initRegateAcademicRank(url, target, whiteList, currentValue) {
                        whiteList = whiteList || [];
                        new Vue({
                            el: target,

                            data: {
                                list: [],
                                currentValue: currentValue
                            },

                            created: function () {
                                var _this = this;

                                $.get(url).then(function (response) {
                                    if (whiteList.length === 0) {
                                        _this.list = response;
                                    }

                                    else {
                                        _this.list = response.filter(checkWhiteList);
                                    }
                                });
                            }
                        });

                        function checkWhiteList(element, index, array) {
                            return whiteList.indexOf(element.Key) !== -1;
                        }
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateAcademicRank('/BankAcademicRank/GetAllSummary/', '#{uniqueId}', {whiteListJsonString}, '{value}');
                </script>
            ";

            return html + initScript + script;
        }
    }
}
