window.__UI_UserId = window.__UI_UserId || (function () {
    // called once on domready
    $(function () {
        window.__UI_UserId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_UserId');

        var ids = [];
        var $elems = $('[data-ui="UserId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-UserId-init')) {
                return;
            }

            var id = $elem.attr('data-id');

            if ($.inArray(id, ids) === -1) {
                ids.push(id);
            }
        });

        if (!ids.length) {
            return;
        }

        var url = 'User/GetByIds/';

        $.post(url, { ids: ids }, function (response) {
            var list = {};
            if (response) {
                for (var i = 0, l = response.length; i < l; i++) {
                    var record = response[i];
                    list[record.Id] = record;
                }

                
            }

            var $elems = $('[data-ui="UserId"]');
            $elems.each(function () {
                var $elem = $(this);
                var id = $elem.attr('data-id');
                var type = $elem.attr('data-replace');
                var item = list[id];

                var text = "---";
                if (typeof item !== typeof undefined) {
                    text = item[type];
                }

                $elem.html(text);
                $elem.attr('data-ui-UserId-init', true);
            });

            refreshEqualHeightBox();
        });
    }
}());