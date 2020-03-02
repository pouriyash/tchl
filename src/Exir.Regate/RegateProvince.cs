namespace Exir.Regate
{
    public static class RegateProvince
    {
        public static string Build(string name = "ProvinceId", int? value = null)
        {
            var uniqueId = $"RegateProvince__{System.Guid.NewGuid().ToString().Replace("-", "")}";
            var html = $@"
                <div id='{uniqueId}'>
                    <select data-role='select2' name='{name}' class='form-control' v-model='currentValue'>
                        <option v-for='item in list' v-bind:value='item.Id' v-bind:selected='item.Id == currentValue'>{{{{ item.Title }}}}</option>
                    </select>
                </div>
            ";
            var initScript = @"
                <script>
                    function initRegateProvince(selector, value) {
                        new Vue({
                            el: selector,

                            data: {
                                list: [],
                                currentValue: value
                            },

                            created: function () {
                                var _this = this;

                                $.get('/BankProvince/GetAllSummary/').then(function(response) {
                                    _this.list = response;
                                });
                            }
                        });
                    }
                </script>
            ";
            var script = $@"
                <script>
                    initRegateProvince('#{uniqueId}', '{value}')
                </script>
            ";
            return html + initScript + script;
        }
    }
}
