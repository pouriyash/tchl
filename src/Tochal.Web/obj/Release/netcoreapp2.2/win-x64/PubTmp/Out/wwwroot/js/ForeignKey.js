window.__UI_ForeignKey = window.__UI_ForeignKey || (function () {
    // called once on domready
    $(function () {
        window.__UI_ForeignKey();
    });

    // the main function to execute
    return function () {
        console.log('__UI_ForeignKey');

        var ids = {};
        var $elems = $('[data-ui="RemarkForeignKey"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-RemarkForeignKey-init')) {
                return;
            }

            var url = $elem.attr('data-url');
            var id = $elem.attr('data-id');

            ids[url] = ids[url] || [];

            if ($.inArray(id, ids[url]) === -1) {
                ids[url].push(id);
            }
        });

        $.each(ids, function(url, ids) {
            $.post(url, {ids: ids}).then(function(response) {
                var $elems = $('[data-ui="RemarkForeignKey"][data-url="' + url + '"]');

                var responseObject = {};
                response.forEach(function(item) {
                    responseObject[item.Id] = item;
                });

                $elems.each(function() {
                    var $elem = $(this);
                    var id = $elem.attr('data-id');
                    if (typeof responseObject[id] !== typeof undefined) {
                        $elem.text(responseObject[id].Title);
                    } else {
                        $elem.html('<i style="opacity: 0.5;">(خالی)</i>');
                    }
                });
            });
        });
    }
}());

window.__UI_RemarkSubSystemType = window.__UI_RemarkSubSystemType || (function () {
    // called once on domready
    $(function () {
        window.__UI_RemarkSubSystemType();
    });

    // the main function to execute
    return function () {
        console.log('__UI_RemarkSubSystemType');

        var $elems = $('[data-ui="RemarkSubSystemType"]');

        if (!$elems.length) return;

        $.get('/SubSystemType/GetUserSubSystemTypes/').then(function (subSystemTypes) {
            // console.log(subSystemTypes);
            $.each(subSystemTypes, function (i, module) {
                $elems.filter('[data-id="' + module.Id + '"]').text(module.Title);
            });
        });
    }
}());