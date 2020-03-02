using System;

namespace Exir.Regate
{
    public static class RegateFaculty
    {
        public static string Build(string name = "FacultyId", int? value = null)
        {
            var uniqueId = $"RegateFaculty__{Guid.NewGuid().ToString().Replace("-", "")}";
            var html = $@"
                <div id='{uniqueId}'>
                    <select data-role='select2' name='{name}' class='form-control' v-model='currentValue'>
                        <option v-for='item in list' v-bind:value='item.Id' v-bind:selected='item.Id == currentValue'>{{{{ item.Title }}}}</option>
                    </select>
                </div>
            ";

            var initScript = @"
                <script>
                    function initRegateFaculty(target, value) {
                        var app = new Vue({
                            el: target,

                            data: {
                                list: [],
                                currentValue: value
                            },

                            created: function () {
                                var _this = this;

                                $.get('/BankFaculty/GetAllSummary/').then(function (response) {
                                    _this.list = response;
                                });
                            }
                        });
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateFaculty('#{uniqueId}', {value});
                </script>
            ";

            return html + initScript + script;
        }
    }
}
