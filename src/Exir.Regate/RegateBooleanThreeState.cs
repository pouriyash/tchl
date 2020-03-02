using System;
using System.Collections.Generic;

namespace Exir.Regate
{
    public static class RegateBooleanThreeState
    {
        public static string Build(
              string name = ""
            , bool? value = null
            , string trueTitle = "true"
            , string falseTitle = "false"
            , string nullTitle = "همه"
        )
        {
            var uniqueId = $"RegateBooleanThreeState__{Guid.NewGuid().ToString().Replace("-", "")}";

            var finalMarkup = $@"
                <select class='form-control' name='{name}'>
                    <option value=''>{nullTitle}</option>
                    <option {(value.HasValue && (bool)value ? " selected " : "")} value='true'>{trueTitle}</option>
                    <option {(value.HasValue && (bool)!value ? " selected " : "")} value='false'>{falseTitle}</option>
                </select>
            ";

            return finalMarkup;
        }
    }
}
