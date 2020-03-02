namespace Exir.Regate
{
    public static class RegateNumber
    {
        public static string Build(string name = "", bool isRequired = false)
        {
            return $@"<input
                name='{name}'
                type='number'
                class='form-control'
                value=''
                {(isRequired ? " required='required' " : "")}
            />";
        }

        public static string Build(string name = "", int? value = 0, bool isRequired = false, float? min = 0F, float? max = 9999999F, float? step = 1F)
        {
            return $@"<input
                name='{name}'
                type='number'
                min='{min.ToString().Replace("/", ".")}' max='{max.ToString().Replace("/", ".")}' step='{step.ToString().Replace("/", ".")}'
                class='form-control'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }

        public static string Build(string name = "", float? value = 0F, bool isRequired = false, float? min = 0F, float? max = 9999999F, float? step = 0.5F)
        {
            return $@"<input
                name='{name}'
                type='number'
                min='{min.ToString().Replace("/", ".")}' max='{max.ToString().Replace("/", ".")}' step='{step.ToString().Replace("/", ".")}'
                class='form-control'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }
        public static string Build(string name = "", int? value = 0, bool isRequired = false, float? min = 0F, float? max = 9999999F, double? step = 1,string placeholder = "")
        {
            return $@"<input
                name='{name}'
                type='number'
                min='{min.ToString().Replace("/", ".")}' max='{max.ToString().Replace("/", ".")}' step='{step.ToString().Replace("/", ".")}'
                class='form-control'
                placeholder = '{placeholder}'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }

    }
}
