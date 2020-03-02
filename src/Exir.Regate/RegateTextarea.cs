using System;
using System.Net;

namespace Exir.Regate
{
    public static class RegateTextarea
    {
        public static string Build(string name = ""
            , string value = ""
            , bool isRequired = false
            , string placeholder = ""
            , int maxWord = 0
            , int minWord = 0
            , bool canSubmit = false
            , RegateTextareaSizes size = RegateTextareaSizes.Large
        )
        {
            var uniqueId = $"RegateTextarea__{name}__{Guid.NewGuid().ToString()}";
            var hasMinMaxWord = minWord > 0 || maxWord > 0;
            value = WebUtility.HtmlEncode(value);

            string markup = "";

            if (hasMinMaxWord)
                markup += "<div class='previewable__comment'>";

            markup += $@"
                <textarea
                    name='{name}'
                    type='text'
                    class='form-control {(hasMinMaxWord ? "previewable__comment__write" : "")}'
                    placeholder='{placeholder}'
                    data-max-word='{maxWord}'
                    data-min-word='{minWord}'
                    data-word-control='{uniqueId}'
                    data-can-submit='{canSubmit.ToString().ToLower()}'
                    style='height: {((int) size).ToString()}px; resize: none;'
                    {(isRequired ? " required='required' " : "")}
                >{value}</textarea>

                {(hasMinMaxWord ? $"<div class='previewable__comment__statistic' data-word-control-info='{uniqueId}'></div>" : "")}
            ";

            if (hasMinMaxWord)
                markup += "</div>";

            return markup;
        }

        public enum RegateTextareaSizes
        {
            Small = 100,
            Medium = 200,
            Large = 300,
        }
    }
}
