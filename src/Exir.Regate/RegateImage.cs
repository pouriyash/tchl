using System;

namespace Exir.Regate
{
    public static class RegateImage
    {
        public static string Build(string name = "", string value = "", bool isRequired = false)
        {
            var uniqueId = "RegateImage__" + name + "__" + Guid.NewGuid();

            var defaultImageValue = "";
            if (!string.IsNullOrWhiteSpace(value))
            {
                defaultImageValue += $"document.getElementById('{uniqueId}').src = window.__Configuration['RepositoryUrl'] + '{value}';";
                defaultImageValue += $"document.getElementById('{uniqueId}__remove').style.display = '';";
            }

            var callbackTryCatch = @"
                (function () {
	                if (typeof refreshEqualHeightBox === typeof undefined) return false;

	                var counter = 0;
	                var intervalId = setInterval(function () {
		                console.log('refreshEqualHeightBox', counter);
		                refreshEqualHeightBox();
		                counter++;

		                if (counter > 10) {
			                clearInterval(intervalId);
		                }
	                }, 100)
                })
            ";

            var markup = $@"
                <input type='text' style='display: none;' id='image-field-{name}' name='{name}' value='{value}' />


                <a href='/file/image/?field={name}'
                   onclick='PopupCenter(this.href, ""ScyllaUploadFile"", 400, 600); return false;'
                   class='btn btn-default red-flamingo'
                   id='{uniqueId}__upload'
                >انتخاب عکس</a>

                <button type='button' class='btn btn-default' style='display: none;' id='{uniqueId}__remove'>حذف عکس</button>

                <div style='margin-top: 5px;'>
                    <img id='{uniqueId}' src='' />
                </div>

                <script>
                    {defaultImageValue}

                    window['__setImage_{name}'] = function (fileName) {{
                        document.getElementById('image-field-{name}').value = fileName;
                        document.getElementById('{uniqueId}').src = window.__Configuration['RepositoryUrl'] + fileName;
                        document.getElementById('{uniqueId}').style.display = '';
                        document.getElementById('{uniqueId}__remove').style.display = '';
                        {callbackTryCatch}()
                    }};
                    
                    document.getElementById('{uniqueId}__remove').onclick = function () {{
                        document.getElementById('image-field-{name}').value = '';
                        document.getElementById('{uniqueId}').src = '';
                        document.getElementById('{uniqueId}').style.display = 'none';
                        document.getElementById('{uniqueId}__remove').style.display = 'none';
                        {callbackTryCatch}()
                    }};
                </script>

                <style>
                    #{uniqueId} {{
                        max-width: 300px;
                        max-height: 300px;
                    }}
                </style>
            ";

            return markup;
        }
    }
}
