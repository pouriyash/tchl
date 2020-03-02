namespace Exir.Regate
{
    public static class RegateWysiwyg
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            var id = "RegateWysiwyg__" + name;

            var height = 500;

            var html = $@"
                <textarea
                    name='{name}'
                    id='{id}'
                    type='text'
                    class='form-control'
                    style='height: {height}px; resize: none'
                    {(isRequired ? " required='required' " : "")}
                >{value}</textarea>
            ";

            var initScript = @"
                <script src='/lib/ckeditor/ckeditor.js'></script>
                <script>
                    function initRegateWysiwyg(targetId, height) {
                        if (typeof CKEDITOR === typeof undefined) {
                            console.log('RegateWysiwyg needs ckeditor');
                            return;
                        }
                        
                        function ckeditorIsLoaded() {
                            if (typeof $.fn.matchHeight !== typeof undefined) {
                                $.fn.matchHeight._update();
                            }
                        }

                        CKEDITOR.replace(targetId, {
                            language: 'fa',
                            height: height + 'px',
                            contentsLangDirection: 'rtl',
                            toolbarGroups: [
                                { name: 'basicstyles', 'groups': ['basicstyles', 'cleanup'] },
                                { name: 'paragraph', 'groups': ['list', 'indent', 'blocks', 'align', 'bidi'] },
                                '/',
                                { name: 'links' },
                                { name: 'insert' },
                            ],
                            removeButtons: 'Language',
                            on: {
                                instanceReady: function( evt ) {
                                    ckeditorIsLoaded();
                                }
                            }
                        });
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateWysiwyg('{id}', '{height}')
                </script>
            ";

            return html + initScript + script;
        }
    }
}
