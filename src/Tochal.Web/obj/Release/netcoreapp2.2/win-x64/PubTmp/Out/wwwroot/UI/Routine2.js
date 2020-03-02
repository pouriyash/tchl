$(function () {
    
    var $routineManageTable = $('[data-role="Routine2ManageTable"]');
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
    var request = $.get('/Routine2/GetRoutine/', { routineId: routineId });
    var steps = {};
    var actions = {};
    request.then(function (response) {
        
        console.log(response);
        response.steps.forEach(function (step) {
            
            steps[step.step] = step;
        });
        response.actions.forEach(function (action) {
            actions[action.step + '_' + action.action] = action;
        });

        console.log(actions);

        var $records = $routineManageTable.find('[data-role="record"]');
        console.log($records);
        console.log($routineManageTable);


        $records.each(function () {
            
            var $record = $(this);
            var recordId = $record.attr('data-record-id');
            var routinestep = $record.attr('data-record-routinestep');
            var step = steps[routinestep];

            // console.log(recordId, step);
            // if (!step) return true;
            // show buttons based on current step and routine fetched from database
            for (var i = 1; i <= 7; i++) {
                var d = step['f' + i];
                if (step['f' + i]) {
                    var $holder = $('#routine_record_action_F' + i + '_' + recordId);
                    // meta
                    var action = actions[step.step + '_F' + i];
                    // console.log(recordId, step, action);
                    if (action) {
                        var $popover = $('#Routine_Action_F' + i + '_' + recordId);
                        var $tooltip = $('#routine_record_action_tooltip_F' + i + '_' + recordId);
                        var $icon = $holder.find('[data-role="icon"]');
                        var $button = $popover.find('[data-role="button"]');

                        var $desc = $popover.find('[name="Description"]');

                        $tooltip.attr('title', action.title);
                        $tooltip.tooltip();

                        $icon.removeClass('pe-7s-play').addClass('pe-7s-' + action.icon);
                        $button.addClass('btn-' + action.color);
                        $button.text(action.title);
                        $holder.addClass('text-' + action.color);

                        if (action.isDescriptionRequired) {
                            $desc.prop('required', true);
                        }


                        if (action.shouldHideDescription) {
                            $desc.remove();
                        }

                        if (action.isDescriptionMultiline) {
                            console.log('multiline')
                            $popover.find('input[name="Description"]').remove();
                        } else {
                            console.log('single line')
                            $popover.find('textarea[name="Description"]').remove();
                        }

                        $tooltip.css('display', 'inline-block');
                    }
                }
            }






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
                    showBeautyMessage(result);
                });
            });
        });
    });
}());



window.__UI_Routine2StepId = window.__UI_Routine2StepId || (function () {
    
     // called once on domready
    $(function () {
        
        window.__UI_Routine2StepId();
    });

    // the main function to execute
    return function () {
        console.log('__UI_Routine2StepId');
        
        var routines = [];
        var $elems = $('[data-ui="Routine2StepId"]');
        $elems.each(function () {
            var $elem = $(this);
            if ($elem.attr('data-ui-Routine2StepId-init')) {
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
             $.get('/Routine2/GetRoutineStepsByRoutineId/', { routineId: routineId })
                 .then(function (response) {
                     
                     
                    if (response) {
                        var responseObject = {};
                        response.forEach(function (step) {
                            
                            // این قسمت توسط مبشر از
                            // Step
                            // به
                            // step
                            // تغییر کرده است
                            responseObject[step.step] = step;
                        });

                        var $elems = $('[data-ui="Routine2StepId"][data-routine="' + routineId + '"]');
                        
                        $elems.each(function () {
                            var $elem = $(this);
                            var step = $elem.attr('data-step');
                            // این قسمت توسط مبشر از
                            // Title
                            // به
                            // title
                            // تغییر کرده است
                            $elem.text(responseObject[step].title);
                        });

                        refreshEqualHeightBox();
                    }
                });
        });
    }
}());

