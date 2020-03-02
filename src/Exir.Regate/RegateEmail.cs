namespace Exir.Regate
{
    public static class RegateEmail
    {
        public static string Build(string name = "", string value = "", bool isRequired = false, string placeholder = "")
        {
            return $@"<input
                name='{name}'
                type='email'
                class='form-control'
                placeholder='{placeholder}'
                value='{value}'
                {(isRequired ? " required='required' " : "")}
            />";
        }
    }
}
