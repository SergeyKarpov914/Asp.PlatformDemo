namespace Clio.Demo.Core.Lib.Extension
{
    public static class BoolEx
    {
        public static bool Toggle(this bool flag)
        {
            return !flag;
        }
    }
}
