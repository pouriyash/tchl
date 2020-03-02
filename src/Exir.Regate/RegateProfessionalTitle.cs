namespace Exir.Regate
{
    public static class RegateProfessionalTitle
    {
        public static string Build(string name = "ProfessionalTitle", string value = "")
        {
            return $@"<select name='{name}' class='form-control'>
                <option {(value == "دکتر" ? " selected " : "")} value='دکتر'>دکتر</option>
                <option {(value == "مهندس" ? " selected " : "")} value='مهندس'>مهندس</option>
            </select>";
        }
    }
}
