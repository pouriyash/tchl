namespace Exir.Regate
{
    public static class RegateIsPublished
    {
        public static string Build(bool value = false)
        {
            return $@"<input
                name='IsPublished'
                type='checkbox'
                value='true'
                {(value ? " checked='checked' " : "")}
            />";
        }
    }
}