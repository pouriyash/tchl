using System;
using System.Collections.Generic;
using Exir.Remark;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exir.Regate
{
    public static class RegateUserMulti
    {
        public static string Build(string name = "", string value = "[]")
        {
            if (value == null)
            {
                value = "[]";
            }

            var ids = JArray.FromObject(JsonConvert.DeserializeObject(value));
            var defaultListNames = "";

            if (ids != null && ids.Count > 0)
            {
                defaultListNames += "<ul class='line-height-25 margin-top-6'>";

                foreach (string id in ids)
                {
                    defaultListNames += $@"<li>{RemarkUserId.Build(int.Parse(id))}</li>";
                }

                defaultListNames += "</ul>";
            }

            var markup = $@"
                <div>
                    <a class='btn btn-info'
                        href='/User/SelectUserMulti/?field={name}&ids={string.Join(",", ids)}'
                        onclick='return PopupCenter(this.href, ""ExirUserMultiSelect"", 900, 700)'>انتخاب</a>
                </div>
                            
                <input type='hidden' name='{name}' value='{value}' id='RegateUserMulti_{name}' />
                            
                <div id='RegateUserMulti_{name}_List'>{defaultListNames}</div>
            ";

            var initScript = @"
                <script>
                    function initRegateUserMulti(name) {
                        window['__setUserMulti_' + name] = function(users) {
                            console.log(users);
                            var $list = $('#RegateUserMulti_' + name + '_List');
                            var $input = $('#RegateUserMulti_' + name);

                            if (users.length === 0) {
                                $input.val('[]');
                                $list.html('');
                                return true;
                            }

                            var $ul = $('<ul></ul>');
                            var ids = [];
                            $.each(users, function(index, user) {
                                $('<li></li>').text(user.FullName).appendTo($ul);
                                ids.push(user.Id + '');
                            });

                            $list.html('');
                            $ul.appendTo($list);
                            $input.val(JSON.stringify(ids));

                            $.fn.matchHeight._update();
                        }
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateUserMulti('{name}');
                </script>
            ";

            return markup + initScript + script;
        }
    }
}
