using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateSwitch
    {
        public static string Build(string name, bool? value, RegateSwitchColors color = RegateSwitchColors.green)
        {
            var originalValue = false;
            if (value.HasValue) originalValue = (bool) value;
            return Build(name, originalValue, color);
        }

        public static string Build(string name, bool value = false, RegateSwitchColors color = RegateSwitchColors.green)
        {
            var uniqueId = $"RegateSwitch__{name}__{Guid.NewGuid().ToString().Replace("-", "")}";

            var styleMarkup = @"
                <style>
                    .reg-switch-container {
                        display: inline-block;
                        vertical-align: middle;
                    }

                    .reg-switch-container *, .reg-switch-container *:before, .reg-switch-container *:after {
                        box-sizing: border-box;
                        margin: 0;
                        padding: 0;
                        -webkit-transition: .25s ease-in-out;
                        -moz-transition: .25s ease-in-out;
                        -o-transition: .25s ease-in-out;
                        transition: .25s ease-in-out;
                        outline: none;
                    }

                    .checkbox-label {
                        display: block;
                        position: relative;
                        padding: 10px;
                        margin-bottom: 20px;
                        font-size: 12px;
                        line-height: 16px;
                        width: 64px;
                        margin: auto;
                        height: 36px;
                        -webkit-border-radius: 18px;
                        -moz-border-radius: 18px;
                        border-radius: 18px;
                        background: #f8f8f8;
                        cursor: pointer;
                    }

                        .checkbox-label.is-small {
                            transform: scale(0.8)
                        }

                        .checkbox-label:before {
                            content: '';
                            display: block;
                            position: absolute;
                            z-index: 1;
                            line-height: 34px;
                            text-indent: 40px;
                            height: 36px;
                            width: 36px;
                            /*border-radius*/
                            -webkit-border-radius: 100%;
                            -moz-border-radius: 100%;
                            border-radius: 100%;
                            top: 0px;
                            left: 0px;
                            right: auto;
                            background: white;
                            /*box-shadow*/
                            -webkit-box-shadow: 0 3px 3px rgba(0,0,0,.2),0 0 0 2px #dddddd;
                            -moz-box-shadow: 0 3px 3px rgba(0,0,0,.2),0 0 0 2px #dddddd;
                            box-shadow: 0 3px 3px rgba(0,0,0,.2),0 0 0 2px #dddddd;
                        }
                    
                    .ios-toggle {
                        display: none;
                    }

                    .ios-toggle:checked + .checkbox-label {
                        -webkit-box-shadow: inset 0 0 0 20px rgba(19,191,17,1),0 0 0 2px rgba(19,191,17,1);
                        -moz-box-shadow: inset 0 0 0 20px rgba(19,191,17,1),0 0 0 2px rgba(19,191,17,1);
                        box-shadow: inset 0 0 0 20px rgba(19,191,17,1),0 0 0 2px rgba(19,191,17,1);
                        background: #13bf11;
                    }

                        .ios-toggle:checked + .checkbox-label:before {
                            left: calc(100% - 36px);
                            /*box-shadow*/
                            -webkit-box-shadow: 0 0 0 2px transparent,0 3px 3px rgba(0,0,0,.3);
                            -moz-box-shadow: 0 0 0 2px transparent,0 3px 3px rgba(0,0,0,.3);
                            box-shadow: 0 0 0 2px transparent,0 3px 3px rgba(0,0,0,.3);
                        }

                    .ios-toggle + .checkbox-label {
                        -webkit-box-shadow: inset 0 0 0 0px rgba(19,191,17,1),0 0 0 2px #dddddd;
                        -moz-box-shadow: inset 0 0 0 0px rgba(19,191,17,1),0 0 0 2px #dddddd;
                        box-shadow: inset 0 0 0 0px rgba(19,191,17,1),0 0 0 2px #dddddd;
                    }

                        .ios-toggle + .checkbox-label.is-orange {
                            -webkit-box-shadow: inset 0 0 0 0px #f35f42,0 0 0 2px #dddddd;
                            -moz-box-shadow: inset 0 0 0 0px #f35f42,0 0 0 2px #dddddd;
                            box-shadow: inset 0 0 0 0px #f35f42,0 0 0 2px #dddddd;
                        }

                    .ios-toggle:checked + .checkbox-label.is-orange {
                        -webkit-box-shadow: inset 0 0 0 20px #f35f42,0 0 0 2px #f35f42;
                        -moz-box-shadow: inset 0 0 0 20px #f35f42,0 0 0 2px #f35f42;
                        box-shadow: inset 0 0 0 20px #f35f42,0 0 0 2px #f35f42;
                        background: #f35f42;
                    }

                        .ios-toggle:checked + .checkbox-label.is-orange:after {
                            color: #f35f42;
                        }

                    .ios-toggle + .checkbox-label.is-cyan {
                        -webkit-box-shadow: inset 0 0 0 0px #1fc1c8,0 0 0 2px #dddddd;
                        -moz-box-shadow: inset 0 0 0 0px #1fc1c8,0 0 0 2px #dddddd;
                        box-shadow: inset 0 0 0 0px #1fc1c8,0 0 0 2px #dddddd;
                    }

                    .ios-toggle:checked + .checkbox-label.is-cyan {
                        -webkit-box-shadow: inset 0 0 0 20px #1fc1c8,0 0 0 2px #1fc1c8;
                        -moz-box-shadow: inset 0 0 0 20px #1fc1c8,0 0 0 2px #1fc1c8;
                        box-shadow: inset 0 0 0 20px #1fc1c8,0 0 0 2px #1fc1c8;
                        background: #1fc1c8;
                    }

                        .ios-toggle:checked + .checkbox-label.is-cyan:after {
                            color: #1fc1c8;
                        }
                </style>
            ";

            var htmlMarkup = $@"
                <div class='reg-switch-container'>
                    <input type='checkbox' name='{name}' id='{uniqueId}' class='ios-toggle' value='true' {(value ? " checked " : "")} />
                    <label for='{uniqueId}' class='checkbox-label is-{color} is-small'></label>
                </div>
            ";

            return styleMarkup + htmlMarkup;
        }
    }

    public enum RegateSwitchColors
    {
        green,
        cyan,
        orange
    }
}