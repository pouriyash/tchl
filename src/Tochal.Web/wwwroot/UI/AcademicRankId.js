window.__UI_AcademicRankId = window.__UI_AcademicRankId || (function () {
    // called once on domready
    $(function () {
        window.__UI_AcademicRankId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_AcademicRankId');

        var ids = [];
        var $elems = $('[data-ui="AcademicRankId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-AcademicRankId-init')) {
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

        var url = '/BankAcademicRank/GetByIds/';

        $.post(url, { ids: ids }, function (response) {
            if (response) {
                for (var i = 0, l = response.length; i < l; i++) {
                    var record = response[i];
                    var $elems = $('[data-ui="AcademicRankId"][data-id="' + record.Id + '"]');
                    $elems.each(function () {
                        var $elem = $(this);
                        var type = $elem.attr('data-replace');

                        $elem.text(record[type]);

                        $elem.attr('data-ui-AcademicRankId-init', true);
                    });
                }

                refreshEqualHeightBox();
            }
        });
    }
}());