namespace Exir.Regate
{
    public static class RegateFile
    {
        public static string Build(string name = "", string value = "", bool isRequired = false, long? length = null,string fileType="image")
        {
            var defaultFileValue = "";
            if (!string.IsNullOrWhiteSpace(value))
            {
                defaultFileValue += $"document.getElementById('fileupload__file__{name}').href = window.__Configuration['RepositoryUrl'] + '{value}';";
                defaultFileValue += $"document.getElementById('fileupload__file__remove__{name}').style.display = '';";
                defaultFileValue += $"document.getElementById('fileupload__file__{name}').style.display = '';";
            }

            var markup = $@"
                <input type='text' style='position: absolute; pointer-events: none; z-index: -1; opacity: 0;' tabindex='-1' id='file-field-{name}' name='{name}' class='notrequired' value='{value}' {(isRequired ? " required='required' " : "")} />


                <a href='/file/file/?field={name}&length={length}&type={fileType}'
                   onclick='PopupCenter(this.href, ""ScyllaUploadFile"", 400, 600); return false;'
                   class='btn btn-default red-flamingo'
                   id='uploadFileButton__{name}'
                >انتخاب فایل</a>

                <button type='button' class='btn btn-default' style='display: none;' id='fileupload__file__remove__{name}'>حذف فایل</button>

                <a id='fileupload__file__{name}' href='' target='_blank' style='display: none;'>مشاهده فایل</a>

                <script>
                    {defaultFileValue}

                    window['__setFile_{name}'] = function (fileName) {{
                        document.getElementById('file-field-{name}').value = fileName;
                        document.getElementById('fileupload__file__{name}').href = window.__Configuration['RepositoryUrl'] + fileName;
                        document.getElementById('fileupload__file__{name}').style.display = '';
                        document.getElementById('fileupload__file__remove__{name}').style.display = '';
                    }};
                    
                    document.getElementById('fileupload__file__remove__{name}').onclick = function () {{
                        document.getElementById('file-field-{name}').value = '';
                        document.getElementById('fileupload__file__{name}').href = '';
                        document.getElementById('fileupload__file__{name}').style.display = 'none';
                        document.getElementById('fileupload__file__remove__{name}').style.display = 'none';
                    }};
                </script>
            ";

            return markup;
        }
    }
}
