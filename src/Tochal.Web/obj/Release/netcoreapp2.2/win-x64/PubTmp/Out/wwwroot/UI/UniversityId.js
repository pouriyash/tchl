window.__UI_UniversityId = window.__UI_UniversityId || (function () {
    // called once on domready
    $(function () {
        window.__UI_UniversityId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_UniversityId');

        var ids = [];
        var $elems = $('[data-ui="UniversityId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-UniversityId-init')) {
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

        var url = '/BankUniversity/GetByIds/';

        $.post(url, { ids: ids }, function (response) {
            if (response) {
                for (var i = 0, l = response.length; i < l; i++) {
                    var record = response[i];
                    var $elems = $('[data-ui="UniversityId"][data-id="' + record.Id + '"]');
                    $elems.each(function () {
                        var $elem = $(this);
                        var type = $elem.attr('data-replace');

                        $elem.text(record[type]);

                        $elem.attr('data-ui-UniversityId-init', true);
                    });
                }

                refreshEqualHeightBox();
            }
        });
    }
}());

window.__UI_FacultyId = window.__UI_FacultyId || (function () {
    // called once on domready
    $(function () {
        window.__UI_FacultyId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_FacultyId');

        var ids = [];
        var $elems = $('[data-ui="FacultyId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-FacultyId-init')) {
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

        var url = '/BankFaculty/GetByIds/';

        $.post(url, { ids: ids }, function (response) {
            if (response) {
                for (var i = 0, l = response.length; i < l; i++) {
                    var record = response[i];
                    var $elems = $('[data-ui="FacultyId"][data-id="' + record.Id + '"]');
                    $elems.each(function () {
                        var $elem = $(this);
                        var type = $elem.attr('data-replace');

                        $elem.text(record[type]);

                        $elem.attr('data-ui-FacultyId-init', true);
                    });
                }

                refreshEqualHeightBox();
            }
        });
    }
}());

window.__UI_ScienceGroupId = window.__UI_ScienceGroupId || (function () {
    // called once on domready
    $(function () {
        window.__UI_ScienceGroupId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_ScienceGroupId');

        var ids = [];
        var $elems = $('[data-ui="ScienceGroupId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-ScienceGroupId-init')) {
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

        var url = '/ScienceGroup/GetByIds/';

        $.post(url, { ids: ids }, function (response) {
            if (response) {
                for (var i = 0, l = response.length; i < l; i++) {
                    var record = response[i];
                    var $elems = $('[data-ui="ScienceGroupId"][data-id="' + record.Id + '"]');
                    $elems.each(function () {
                        var $elem = $(this);
                        var type = $elem.attr('data-replace');

                        $elem.text(record[type]);

                        $elem.attr('data-ui-ScienceGroupId-init', true);
                    });
                }

                refreshEqualHeightBox();
            }
        });
    }
}());

window.__UI_ProvinceId = window.__UI_ProvinceId || (function () {
    // called once on domready
    $(function () {
        window.__UI_ProvinceId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_ProvinceId');

        var ids = [];
        var $elems = $('[data-ui="ProvinceId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-ProvinceId-init')) {
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

        var url = '/BankProvince/GetByIds/';

        $.post(url, { ids: ids }, function (response) {
            if (response) {
                for (var i = 0, l = response.length; i < l; i++) {
                    var record = response[i];
                    var $elems = $('[data-ui="ProvinceId"][data-id="' + record.Id + '"]');
                    $elems.each(function () {
                        var $elem = $(this);
                        var type = $elem.attr('data-replace');

                        $elem.text(record[type]);

                        $elem.attr('data-ui-ProvinceId-init', true);
                    });
                }

                refreshEqualHeightBox();
            }
        });
    }
}());