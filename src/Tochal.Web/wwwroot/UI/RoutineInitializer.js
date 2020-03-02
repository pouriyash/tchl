(function () {
    var $routineManageTable = $('[data-role="RoutineManageTable"]');
    if (!$routineManageTable.length) {
        return false;
    }

    if ($routineManageTable.attr('data-initialized')) {
        return false;
    }

    $routineManageTable.attr('data-initialized', true);

    // console.log('we have routine table');

    var routineId = $routineManageTable.attr('data-routine-id');

    // get steps
    var request = $.get('/Routine/GetRoutineStepsByRoutineId/', { routineId: routineId });
    var steps = {};
    request.then(function (response) {
        response.forEach(function (step) {
            steps[step.Step] = step;
        });

        var $records = $('[data-role="record"]');

        $records.each(function () {
            var $record = $(this);
            var recordId = $record.attr('data-record-id');
            var routinestep = $record.attr('data-record-routinestep');
            var step = steps[routinestep];

            // console.log(recordId, step);
            // show buttons based on current step and routine fetched from database
            if (step.Ok) $('#routine_record_action_Ok_' + recordId).show();
            if (step.Cancel) $('#routine_record_action_Cancel_' + recordId).show();
            if (step.Edit) $('#routine_record_action_Edit_' + recordId).show();
            if (step.Next) $('#routine_record_action_Next_' + recordId).show();

            // disable the forms (because we use ajax to submit them)
            $record.find('form[data-role="routine-ajax-form"]').on('submit', function (e) {
                e.preventDefault();

                var $form = $(this);
                var action = $form.attr('data-role');

                // send request via ajax
                var url = $form.attr('action') || ('/' + window.__CurrentModule + '/ChangeDashboard/');
                var model = $form.serialize();

                console.log('submitting form');
                console.log(url);
                console.log(model);

                var request = $.post(url, model);

                $record.addClass('exir--disabled');
                $('[data-popover]').webuiPopover('hide');
               
                request.then(function (result) {
                    debugger;
                    swal({
                        title: (result.succeed ? 'موفق' : 'خطا'),
                        text: result.message || (result.succeed ? 'عملیات با موفقیت انجام شد' : "عملیات با خطا مواجه شد"),
                        type: result.succeed ? 'success' : 'error',
                        confirmButtonText: "تایید",
                        html: true
                    });
                     
                });

            });
        });
    });
}());