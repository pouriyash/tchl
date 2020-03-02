$(function () {
    var mainSelector = $('[data-malt="main"]');

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
        function (e) {
            var id = $(this).data('id');
            var value = this.value;
            var childSelector = $('[data-malt=child][data-id=' + id + ']');
            var childValue = childSelector.val();
            var relatedMains = $('[data-related=' + id + ']');
            var url = $(this).attr('data-url');

            if ($(this).attr('data-firsttimefired') !== 'true') {
                // console.log('this is the first time, do not clean related items');
                $(this).attr('data-firsttimefired', 'true');
            } else {
                var relatedId = $('[data-related="' + id + '"]').attr('data-id');
                if (relatedId) window[relatedId + '__setter']({ Id: null, Title: null });
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

window.RegateMainAltTextBoxSetter = function (id, idTitleObject) {
    console.log('setter is calling', id, idTitleObject);

    $('[data-id="' + id + '"][data-malt="main"]')
        .val(idTitleObject.Id)
        .trigger('change');
}