using System;

namespace Exir.Regate
{
    public static class RegateForeignKey
    {
        public static string Build(string name, string url, int? value = null)
        {
            var uniqueId = $"RegateForeignKey__{name}__{Guid.NewGuid().ToString()}";

            var initScript = @"
                <script>
                    function initRegateForeignKey(target, url, currentValue) {
                        $.get(url).then(function (response) {
                            {
                                var $select = $('<select></select>');
                                $select.attr('data-val', 'false');
                                $select.prop('required', true);
                                $select.addClass('form-control');
                                $('<option>---</option>').attr('value', '').appendTo($select);

                                response.forEach(function (item) {
                                    {
                                        var $option = $('<option></option>');
                                        $option.val(item.Id);
                                        $option.text(item.Title);
                                        $option.appendTo($select);
                                    }
                                });

                                $select.on('change', function () {
                                    {
                                        var value = this.value;
                                        $(target + '__hidden').val(value);
                                    }
                                });

                                $select.find('[value=""' + currentValue + '""]').prop('selected', true);

                                $select.appendTo(target);
                            }
                        });
                    }
                </script>
            ";

            var html = $@"
                <div id='{uniqueId}'>
				    <input type='hidden' name='{name}' value='{value}' id='{uniqueId}__hidden' />
				</div>
            ";

            var script = $"<script>initRegateForeignKey('#{uniqueId}', '{url}', '{value}');</script>";

            return html + initScript + script;
        }
    }
}
