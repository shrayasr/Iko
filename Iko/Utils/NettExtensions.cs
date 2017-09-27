using Nett;

namespace Iko.Utils
{
    public static class NettExtensions
    {
        public static bool TryGetValue<T>(this TomlTable instance, string key, out T value)
        {
            if (instance.TryGetValue(key, out TomlObject obj))
            {
                value = obj.Get<T>();
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }
    }
}
