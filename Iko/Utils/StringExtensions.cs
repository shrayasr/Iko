namespace Iko.Utils
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string instance)
            => string.IsNullOrEmpty(instance);
    }
}
