namespace Exir.Regate
{
    public static class RegateHiddenId
    {
        public static string Build(int value)
        {
            return $@"<input
                name='Id'
                type='hidden'
                value='{value}'
            />";
        }
    }
}
