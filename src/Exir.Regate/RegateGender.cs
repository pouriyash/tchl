namespace Exir.Regate
{
    public static class RegateGender
    {
        public static string Build(bool value = true)
        {
            return $@"
                <label class='radio-inline'><input
                    name='IsMale'
                    type='radio'
                    value='True'
                    {(value ? " checked " : "")}
                /> مرد </label>

                <label class='radio-inline'><input
                    name='IsMale'
                    type='radio'
                    value='False'
                    {(! value ? " checked " : "")}
                /> زن </label>
            ";
        }
    }
}
