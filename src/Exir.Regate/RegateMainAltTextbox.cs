using System;

namespace Exir.Regate
{
    public static class RegateMainAltTextbox
    {
        public static string Main(string id = "", string name = "", string currentValue = "", string ajaxUrl = "", string parentId = "")
        {
            var mainTextboxTag = $@"
                <input
                    data-id='{id}'
                    data-malt='main'
                    data-url='{ajaxUrl}'
                    type='text'
                    name='{name}'
                    {(!string.IsNullOrWhiteSpace(parentId) ? $"data-related='{parentId}'" : "")}
                    value='{currentValue}'
                />";

            var childTextboxTag = $"<input data-id='{id}' type='hidden' data-malt='child' />";

            return mainTextboxTag + (! string.IsNullOrWhiteSpace(parentId) ? childTextboxTag : "");
        }

        public static string Alt(string id = "", string name = "", string fieldName = "Title")
        {
            return $"<input data-id='{id}' data-malt-slave='{fieldName}' type='text' name='{name}' />";
        }

        public static string Button(string id = "", string popupUrl = "", string parentId = "")
        {
            var setter = $"{id}__setter";

            var setterScriptTag = $@"
                <script>
                    window['{setter}'] = function(idTitleObject) {{
                        $('[data-id=""{id}""][data-malt=""main""]')
                            .val(idTitleObject.Id)
                            .trigger('change');
                    }}
                </script>
            ";

            var linkButtonTag = $@"
                <a
                    href='{popupUrl}?__setter={setter}'
                    data-id='{id}'
                    data-malt='link'
                    {(! string.IsNullOrWhiteSpace(parentId) ? $"data-related='{parentId}'" : "")}
                    onclick='return PopupCenter(this.href, ""RegateForeignKeyPopup"", 600, 700)'
                ></a>";

            return setterScriptTag + linkButtonTag;
        }


        public static string Initialize()
        {
            return @"
                <script>
                    $(function () {
                        var mainSelector = $('[data-malt=main]');

                        function setAltVal(val, altSelector) {
                            altSelector.val(val);
                        }

                        function fetchAltText(url, id, parentId) {
                            return $.get(url, { id: id, __parentId: parentId });
                        }

                        function setLoadingState(state, altSelector) {
                            altSelector.attr('disabled', state);
                        }

                        function setLinkHref(link, parentId) {
                            parentId = parentId || '';
                            var href = $(link).attr('href') || '';
                            var pathname = href.substr(0, href.indexOf('?'));
                            var params = href.substr(href.indexOf('?'));
                            var qs = new URLSearchParams(params);
                            qs.set('__parentId', parentId);

                            return pathname + '?' + qs;
                        }


                        mainSelector.on('change',
                            function () {
                                var id = $(this).data('id');
                                var value = this.value;
                                var childSelector = $('[data-malt=child][data-id=' + id + ']');
                                var childValue = childSelector.val();
                                var relatedMains = $('[data-related=' + id + ']');
                                var url = $(this).attr('data-url');

                                if (url === '') {
                                    return true;
                                }

                                relatedMains.each(function (i, relatedElem) {
                                    var relatedElemId = $(relatedElem).data('id');
                                    var relatedElemChild = $('[data-malt=child][data-id=' + relatedElemId + ']');
                                    var relatedElemLinks = $('[data-malt=link][data-id=' + relatedElemId + ']');
                                    var href = setLinkHref(relatedElemLinks, value);
                                    relatedElemLinks.attr('href', href);
                                    relatedElemChild.val(value);
                                });

                                setLoadingState(true, $('[data-malt-slave][data-id=' + id + ']'));
                                $('[data-malt-slave][data-id=' + id + ']').attr('tabindex', -1);

                                fetchAltText(url, value, childValue).then(function (res) {
                                    //console.log(res);
                                    _.forEach(res, function (value, key) {
                                        setAltVal(value, $('[data-malt-slave=' + key + '][data-id=' + id + ']'));
                                    });

                                    setLoadingState(false, $('[data-malt-slave][data-id=' + id + ']'));
                                });
                            }
                        );

                        mainSelector.change();
                    });
                </script>
            ";
        }
    }
}
