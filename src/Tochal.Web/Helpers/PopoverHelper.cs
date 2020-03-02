using System;
using System.IO;
using Tochal.Core.DomainModels.Entity.Routine2;
using Tochal.Core.DomainModels.SSOT; 
using Microsoft.AspNetCore.Http;

namespace Tochal.Web.Helpers
{
    public static class PopoverHelper
    {
        public static string Button(string id, string uniqueId = "")
        {
            return $" data-popover='popover__generated__id__{id}__{uniqueId}' ";
        }

        public static string Panel(string id, string uniqueId = "")
        {
            return $" data-role='popover-content' id='popover__generated__id__{id}__{uniqueId}' ";
        }

        public static string InitializeFormSubmission(string id, int modelId, RoutineActions routineAction)
        {
            return $@"
                <script>
                    $('#popover__generated__id__{id}__{modelId} form').on('submit', function (e) {{
                        e.preventDefault();

                        var $form = $(this);
                        $form.prop('disabled', true);
                        $form.find(':submit').prop('disabled', true);

                        var url = $form.attr('action');
                        var model = $form.serialize();
                        var routineAction = '{routineAction}';

                        var description = '';
                        var $description = $form.find('[name=\'Description\']');
                        if ($description.length) {{
                            description = $description.val();
                        }}

                        console.log('submitting form');
                        console.log(routineAction);
                        console.log(url);
                        console.log(model);
                        console.log(description);

                        var request = $.post(url, model);
                        request.then(function(response) {{
                            if (! response.Succeed) {{
                                showBeautyMessage(response);
                                $form.prop('disabled', false);
                                $form.find(':submit').prop('disabled', false);
                                return;
                            }}
                            
                            window['routineSubmit' + routineAction]('{modelId}', description);
                        }});
                    }});
                </script>
            ";
        }
    }
}