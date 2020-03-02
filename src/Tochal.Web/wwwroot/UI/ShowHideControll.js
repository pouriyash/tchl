(function () {
    var ctrls = $('[data-shide-ctrl]');
    var checkedCtrlsNames = $('[data-shide-ctrl]:not(:checked)').map(
        function (i, ctrl) {
            return $(ctrl).data('shide-ctrl');
        }
    );

    checkedCtrlsNames.each(function (i, name) {
        var targets = $('[data-shide-target=' + name + ']');
        targets.each(function (i, target) {
            $(target).css('display', 'none');
        })
    });

    ctrls.each(function (i, ctrl) {
        $(ctrl).on('change', function () {
            var name = $(this).data('shide-ctrl');
            var checked = this.checked;
            var targets = $('[data-shide-target=' + name + ']');
            $(targets).css('display', checked ? '' : 'none');
        })
    })
})()
