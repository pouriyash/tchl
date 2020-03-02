using Exir.Remark;

namespace Exir.Regate
{
    public static class RegateUser
    {
        public static string Build(string name = "", int? value = null, bool isRequired = false)
        {
            var markup = $@"
                <input type='text' style='position: absolute; pointer-events: none; z-index: -1; opacity: 0;' tabindex='-1' name='{name}' value='{value}' id='RegateUser_{name}' {(isRequired ? " required='required' " : "")} />

                <a class='btn btn-info'
                    href='/Identity/User/SelectUser/?field={name}'
                    onclick='return PopupCenter(this.href, ""ExirUserSelect"", 600, 700)'>انتخاب</a>

                <span id='RegateUser_{name}_Name'>
                    {RemarkUserId.Build(value)}
                </span>
            ";

            var initScript = @"
                <script>
                    function initRegateUser(name) {
                        window['__setUser_' + name] = function(person) {
                        debugger;
                            document.getElementById('RegateUser_' + name).value = person.id;
                            document.getElementById('RegateUser_' + name + '_Name').innerText = person.displayName;
                        }
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateUser('{name}');
                </script>
            ";

            return markup + initScript + script;
        }
    }
}
