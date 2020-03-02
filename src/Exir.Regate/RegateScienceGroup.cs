using System;

namespace Exir.Regate
{
    public static class RegateScienceGroup
    {
        public static string Build(
            string name = "ScienceGroupId",
            int? value = null,
            bool initSelect2 = true,
            bool isRequired = true
        )
        {
            var uniqueId = $"RegateScienceGroup{Guid.NewGuid().ToString().Replace("-", "")}";

            var html = $@"
                <div id='{uniqueId}'>
                    <select class='form-control' name='{name}' {(initSelect2 ? " data-role='select2' " : "")} v-model='currentValue' {(isRequired ? " required " : "")}>
                        <option v-for='item in list' v-bind:value='item.Id' v-bind:selected='item.Id == currentValue'>{{{{ item.Title }}}}</option>
                    </select>
                </div>
            ";

            var initScript = @"
                <script>
                    function initRegateScienceGroup(selector, value) {
                        var app = new Vue({
                            el: selector,

                            data: {
                                list: [],
                                currentValue: value
                            },

                            created: function () {
                                var _this = this;

                                $.get('/ScienceGroup/GetAllSummary/').then(function(response) {
                                    _this.list = response;
                                });
                            }
                        });
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateScienceGroup('#{uniqueId}', '{value}');
                </script>
            ";

            return html + initScript + script;
        }
    }
}
