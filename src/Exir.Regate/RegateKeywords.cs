using System.Net;

namespace Exir.Regate
{
    public static class RegateKeywords
    {
        public static string Build(string name, string jsonValue = "[]", bool debug = false)
        {
            var styleTag = @"
                <style>
                    .origin-container {
                        background: white;
                        display: flex;
                        align-items: center;
                    }

                        .origin-container [data-dynalist='origin'] {
                            margin-left: 10px;
                        }

                    [data-sortable='handle'] {
                        cursor: move;
                        cursor: -webkit-grab;
                        margin-left: 15px;
                    }
                </style>
            ";

            var initScript = @"
                <script src='/lib/sortable-1.4.2/Sortable.min.js'></script>
                <script>
                    function initRegateKeywords() {
                        if (window.____dynalist) {
                            console.warn('tried to initialize dynamic inputs more than once, please see your code');
                            return;
                        }
                        window.____dynalist = true;

                        // fill target inpus using orign values
                        function fillTargets(wrapper) {
                            var $origins = wrapper.find('[data-dynalist=origin]');
                            var $targets = wrapper.find('[data-dynalist=target]');
                            var originValues = $origins.toArray()
                                .filter(function (origin) {
                                    return origin.value.trim() !== '';
                                })
                                .map(function (origin) {
                                    return origin.value;
                                });
                            $targets.val(JSON.stringify( $.grep(originValues, function(v, k){ return $.inArray(v, originValues) === k; }) ));
                        }

                        // make origin inputs from target input
                        function makeOrigins(wrapper, json) {
                            json = json || '[]';
                            var originSection = wrapper.find('[data-dynalist=origin-section]');
                            try {
                                json = JSON.parse(json);
                            } catch (err) {
                                console.warn(err);
                                originSection.empty();
                            }
                            originSection.empty();
                            if (!Array.isArray(json))
                                return;
                            json.forEach(function (v) {
                                var originContainer = $('<div class=\'origin-container\' data-dynalist=\'origin-container\'></div>');
                                var origin = $('<input data-dynalist=\'origin\' type=\'text\' name=\'\' value=\'\' class=\'form-control\' />');
                                var removeButton = $('<button tabindex=\'-1\' data-dynalist=\'remove\' type=\'button\' class=\'text-danger exir--transparent-btn\'><i class=\'pe-7s-close-circle largeicon\' title=\'\' data-toggle=\'tooltip\' data-original-title=\'حذف\'></i></button>');
                                var handle =
                                    $('<div data-sortable=\'handle\'><i class=\'fa fa-bars size-14\' style=\'color:#888\'></i></div>');
                                origin.val(v);
                                originContainer.append(handle);
                                originContainer.append(origin);
                                originContainer.append(removeButton);
                                originSection.append(originContainer);
                            });
                            wrapper.find('[data-dynalist=add]').closest('[data-dynalist=origin-container]')
                                .find('[data-dynalist=origin]').val('');

                        }

                        // add an origin item after current origin container
                        function addOrigin(originContainer, isInSection) {
                            var elemClone = originContainer.clone();
                            var remove = originContainer.find('[data-dynalist=remove]');
                            var wrapper = originContainer.closest('[data-dynalist=wrapper]');
                            var originSection = wrapper.find('[data-dynalist=origin-section]');
                            if (!remove.length)
                                elemClone.find('[data-dynalist=origin]')
                                    .after(
                                    ' <button tabindex=\'-1\' type=\'button\' data-dynalist=\'remove\' class=\'text-danger exir--transparent-btn\'><i class=\'pe-7s-close-circle largeicon\'></i></button>'
                                    );
                            originContainer.find('[data-dynalist=origin]').val('');
                            elemClone.find('[data-dynalist=add]').remove();

                            if (isInSection)
                                originContainer.before(elemClone);
                            else {
                                elemClone.find('.fa-bars').css('visibility', 'visible');
                                originSection.append(elemClone);

                            }
                            originContainer.find('[data-dynalist=origin]').focus();
                        }

                        // fill targets with orgin values
                        $('body').on('input', '[data-dynalist=origin]', function () {
                            var wrapper = $(this).closest('[data-dynalist=wrapper]');
                            fillTargets(wrapper);
                        });

                        // add new origin on add ctrl click
                        $('body').on('click', '[data-dynalist=add]', function () {
                            var originContainer = $(this).closest('[data-dynalist=origin-container]');
                            addOrigin(originContainer);
                        });

                        // remove origins
                        $('body').on('click', '[data-dynalist=remove]', function () {
                            var wrapper = $(this).closest('[data-dynalist=wrapper]');
                            $(this).closest('[data-dynalist=origin-container]').remove();
                            fillTargets(wrapper);
                        });

                        // make origins from target
                        $('[data-dynalist=target]').on('change', function () {
                            var wrapper = $(this).closest('[data-dynalist=wrapper]');
                            var json = $(this).val();
                            makeOrigins(wrapper, json);
                        });

                        // make origins on load
                        $('[data-dynalist=target]').change();

                        // add origin item on enter
                        $('body').on('keypress', '[data-dynalist=origin]', function (e) {
                            if (e.key === 'Enter') {
                                e.stopPropagation();
                                e.preventDefault();
                                var originContainer = $(this).closest('[data-dynalist=origin-container]');
                                var isInSection = $(this).closest('[data-dynalist=origin-section]').length !== 0;
                                addOrigin(originContainer, isInSection);
                            }
                        });

                        // sortable initialization
                        var sortables = $('[data-sortable=container]').map(function (i, container) {
                            return Sortable.create(container,
                                {
                                    onEnd: function () {
                                        var wrapper = $(container).closest('[data-dynalist=wrapper]');
                                        fillTargets(wrapper);
                                    },
                                    animation: 220,
                                    handle: '.fa-bars'
                                });
                        });
                    }
                </script>
            ";

            var script = "<script>$(initRegateKeywords);</script>";

            var sortableMarkup = $@"
                <div>
                    <div data-dynalist='origin-section' data-sortable='container'></div>
                    <div class='origin-container' data-dynalist='origin-container'>
                        <div data-sortable='handle' style='visibility: hidden;'>
                            <i class='fa fa-bars size-14' style='color:#888'></i>
                        </div>
                        <input data-dynalist='origin' type='text' value='' class='form-control' />
                        <button class='text-success exir--transparent-btn' type='button' data-dynalist='add'>
                            <i class='pe-7s-plus largeicon'></i>
                        </button>
                    </div>
                </div>
            ";

            var hiddenTextbox = $"<textarea {(! debug ? " style='display: none;' " : "")} dir='ltr' data-dynalist='target' rows='10' type='text' name='{name}' class='form-control'>{jsonValue}</textarea>";

            var finalMarkup = $@"
                {styleTag}
                {initScript}

                <div data-dynalist='wrapper'>
                    {hiddenTextbox}
                    {sortableMarkup}
                </div>
                {script}
            ";

            return finalMarkup;
        }
    }
}