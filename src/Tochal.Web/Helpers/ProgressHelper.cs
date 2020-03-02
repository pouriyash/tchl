using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tochal.Core.DomainModels.SSOT; 
using Microsoft.AspNetCore.Http;

namespace Tochal.Web.Helpers
{
    public static class ProgressHelper
    {
        public static string Link(string basename, string title, string controller, string id)
        {
            return $@"
                <a class='progress_info' href='/{controller}/Edit{basename}/{id}' data-role='{basename}'>
                    <div class='row'>
                        <div class='col-md-5 m-t-xxs'>
                            {title}
                        </div>
                        <div class='col-md-7'>
                            <i class='fa fa_margined fa-lg pull-right fa-check text-success' style='display: none' v-bind:style=""{{ display: (step{basename}._loaded && step{basename}.IsComplete ? '' : 'none')}}""></i>
                            <i class='fa fa-times fa_margined fa-lg pull-right text-danger' style='display: none' v-bind:style=""{{ display: (step{basename}._loaded && ! step{basename}.IsComplete ? '' : 'none')}}""></i>
                            <i class='fa fa-spinner fa_margined fa-spin fa-lg pull-right text-primary' v-bind:style=""{{ display: (step{basename}._loaded ? 'none' : '')}}""></i>

                            <span data-role='steps-indicator' class='pull-right' style='visibility: hidden; position: relative; top: 2px; display: inline-block; width: 40px; text-align: center;' v-bind:style=""{{ visibility: (step{basename}._loaded ? 'visible' : 'hidden')}}"">{{{{ step{basename}.CompletedSteps }}}}/{{{{ step{basename}.TotalSteps }}}}</span>

                            <div class='progress progress_margined m-t-xs full progress-small'>
                                <div style='width: 0'
                                     v-bind:style=""{{ width: step{basename}.ProgressPercentage + '%'}}""
                                     class='progress-bar progress-bar-success'>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>

                <script>
                    $.get('/{controller}/GetProgress{basename}', {{ id: '{id}' }}).done(function (res) {{
                        // console.log(res);
                        app__progress.step{basename} = res;
                        app__progress.step{basename}._loaded = true;
                    }});
                </script>
            ";
        }

        public static string ActiveCurrentTab(string basename)
        {
            return $@"<script>$('.progress_info[data-role=""{basename}""]').addClass('active');</script>";
        }

        public static string ActiveCurrentTab2(string basename)
        {
            return $@"<script>$('.step[data-name=""{basename}""]').addClass('active');</script>";
        }

        public static string Initializer(List<string> list)
        {
            return $@"
                <script>
                    (function () {{
                        window.app__progress = new Vue({{
                            el: '#progressForEntity',

                            data: {{
                                initialized: true
                                {", step" + string.Join(": { _loaded: false }, step", list) + ": { _loaded: false }"}
                            }}
                        }});
                    }}());
                </script>
            ";
        }
    }
}