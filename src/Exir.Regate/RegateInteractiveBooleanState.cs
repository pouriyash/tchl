using System;

namespace Exir.Regate
{
    public static class RegateInteractiveBooleanState
    {
        public static string Build(int id
            , bool? value
            , string url
            , string iconTrue = "cogs"
            , string iconFalse = "cogs"
            , string iconNull = "circle"
            , string title = ""
            , string titleTrue = ""
            , string titleFalse = ""
            , string titleNull = ""
            , bool reverseColors = false
            , string colorTrue = "Green"
            , string colorFalse = "Red"
            , string colorNull = "Orange"
            , string className = ""
            , bool showConfirmAlert = false
        )
        {
            var guid = Guid.NewGuid().ToString();
            var uniqueId = $"RegateInteractiveBooleanState__{id}__{guid}";

            var trueColor = reverseColors ? "text-danger" : "text-success";
            var falseColor = reverseColors ? "text-success" : "text-danger";
            var nullColor = "text-warning";

            var html = $@"
                <div style='white-space: nowrap; display: inline-block' id='{uniqueId}' v-cloak><span id='{uniqueId}state__null' class='{nullColor}' style='font-size: 25px;display: none; cursor: pointer; color: {colorNull}' title='{(string.IsNullOrWhiteSpace(titleNull) ? title : titleNull)}'><i class='fa fa-{iconNull} {className}'></i></span><span id='{uniqueId}state__0' class='{falseColor}' style='font-size: 25px;display: none; cursor: pointer; color: {colorFalse}' title='{(string.IsNullOrWhiteSpace(titleFalse) ? title : titleFalse)}'><i class='fa fa-{iconFalse} {className}'></i></span>

                    <span id='{uniqueId}state__1' class='{trueColor}' style='font-size: 25px;display: none; cursor: pointer; color: {colorTrue}' title='{(string.IsNullOrWhiteSpace(titleTrue) ? title : titleTrue)}'>
                        <i class='fa fa-{iconTrue} {className}'></i>
                    </span>

                    <span id='{uniqueId}state__loading'>
                        <i class='fa fa-spin fa-spinner'></i>
                    </span>
                </div>
            ";

            var initScript = @"
                <script>
                    function initRegateInteractiveBooleanState(targetId, value, url, id) {
                        var container = document.getElementById(targetId);
                        var state_0 = document.getElementById(targetId + 'state__0');
                        var state_1 = document.getElementById(targetId + 'state__1');
                        var state_null = document.getElementById(targetId + 'state__null');
                        var state_loading = document.getElementById(targetId+ 'state__loading');

                        state_loading.style.display = 'none';

                        if (value !== null) {
                            if (value) {
                                state_1.style.display = '';
                            } else {
                                state_0.style.display = '';
                            }
                        }
                        else {
                            state_null.style.display = '';
                        }

                        var changeState = function(id, status) {
                            // hide current states
                            console.log(status);
                            state_null.style.display = 'none';
                            state_0.style.display = 'none';
                            state_1.style.display = 'none';

                            // show loading
                            state_loading.style.display = '';

                            if(" + (showConfirmAlert ? "'true'" : "'false'" ) + @" === 'true'){
                                swal({
                                            title: 'توجه',
                                            text: 'ایا مطمین هستید؟',
                                            type: 'warning',
                                            showCancelButton: true,
                                            confirmButtonColor: '#28a745',
                                            cancelButtonColor: '#dc3545',
                                            confirmButtonText: 'بلی',
                                            cancelButtonText: 'خیر',
                                        }, function(result)
                                        {
                                            if (result)
                                            {
                                                // send ajax request
                                                
                                                var data = { id: id, status: status };
                                                
                                                var request = $.post(url, data);
                                                
                                                request.then(function(response) {
                                                    if (!response.Succeed) return alert('خطا! لطفا مجددا تلاش کنید');

                                                    // hide loading
                                                    state_loading.style.display = 'none';

                                                    // show current state
                                                    if (response.Data)
                                                    {
                                                        state_0.style.display = 'none';
                                                        state_1.style.display = '';
                                                    }
                                                    else
                                                    {
                                                        state_1.style.display = 'none';
                                                        state_0.style.display = '';
                                                    }
                                                });
                                            }
                                            else{
                                                  // hide loading
                                                  state_loading.style.display = 'none';                  
                                                  if(status === true){
                                                     state_1.style.display = 'none';
                                                     state_0.style.display = '';
                                                  }
                                                  else
                                                  {
                                                        state_0.style.display = 'none';
                                                        state_1.style.display = '';
                                                  }

                                            }
                                })
                            }
                            else{
                                // send ajax request
                                var data = { id: id, status: status };
debugger;
                                var request = $.post(url, data);
                                request.then(function(response) {
debugger;
                                    if (!response.succeed) return alert('خطا! لطفا مجددا تلاش کنید');

                                    // hide loading
                                    state_loading.style.display = 'none';

                                    // show current state
                                    if (response.data) {
                                        state_0.style.display = 'none';
                                        state_1.style.display = '';
                                
                                    } else {
                                        state_1.style.display = 'none';
                                        state_0.style.display = '';
                                    }
                                   
                                });
                            }

                            

                            
                            
                            

                        };

                        state_null.onclick = function() {
                            changeState(id, true);
                        };

                        state_0.onclick = function() {
                            changeState(id, true);
                        };

                        state_1.onclick = function() {
                            changeState(id, false);
                        };
                    }
                </script>
            ";

            var script = $@"
                <script>
                    initRegateInteractiveBooleanState('{uniqueId}', {(value.HasValue ? value.ToString().ToLower() : "null")}, '{url}', '{id}')
                </script>
            ";

            return html + initScript + script;
        }
    }
}
