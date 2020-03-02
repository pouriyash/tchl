namespace Exir.Regate
{
    public static class RegateUniversity
    {
        public static string Build(string name = "UniversityId", int? value = null, bool? isRequired = false)
        {
            var uniqueId = $"RegateUniversity__{System.Guid.NewGuid().ToString().Replace("-", "")}";
            var required = "";

            if (isRequired.HasValue && (bool) isRequired)
                required = " required='required' ";

            var html = $@"
                <div id='{uniqueId}'>
                    <select data-role='select2' name='{name}' class='form-control' v-model='currentValue' {required}>
                        <option v-for='item in list' v-bind:value='item.Id' v-bind:selected='item.Id == currentValue'>{{{{ item.Title }}}}</option>
                    </select>
                </div>
            ";
            var initScript = @"
                <script>
                    function initRegateUniversity(selector, value) {
                        new Vue({
                            el: selector,

                            data: {
                                list: [],
                                currentValue: value
                            },

                            created: function () {
                                var _this = this;

                                $.get('/BankUniversity/GetAllSummary/').then(function(response) {
                                    _this.list = response;
                                });
                            }
                        });
                    }
                </script>
            ";
            var script = $@"
                <script>
                    initRegateUniversity('#{uniqueId}', '{value}')
                </script>
            ";
            return html + initScript + script;
        }
    }
}
