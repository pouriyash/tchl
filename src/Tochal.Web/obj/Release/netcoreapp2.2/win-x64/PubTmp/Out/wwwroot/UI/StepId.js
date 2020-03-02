window.__UI_StepId = window.__UI_StepId || (function () {
    // called once on domready
    $(function () {
        window.__UI_StepId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_StepId');

        var routines = [];
        var $elems = $('[data-ui="StepId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-StepId-init')) {
                return;
            }

            var routine = $elem.attr('data-routine');

            if ($.inArray(routine, routines) === -1) {
                routines.push(routine);
            }
        });

        if (!routines.length) {
            return;
        }

        routines.forEach(function (routineId) {
            // console.log(routineId);
            $.get('/Routine/GetRoutineStepsByRoutineId/', { routineId: routineId })
                .then(function (response) {
                    if (response) {
                        var responseObject = {};
                        response.forEach(function (step) {
                            responseObject[step.Step] = step;
                        });

                        var $elems = $('[data-ui="StepId"][data-routine="' + routineId + '"]');
                        $elems.each(function() {
                            var $elem = $(this);
                            var step = $elem.attr('data-step');
                            $elem.text( responseObject[step].Title );
                        });

                        refreshEqualHeightBox();
                    }
                });
        });
    }
}());