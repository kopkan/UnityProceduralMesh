using System;

namespace Shared
{
    class ArrayHelp
    {
        static public void Cut<T>(in T[] source, out T[] result, int start, int len)
        {
            int newSize = source.Length - len;

            result = new T[newSize];
            Array.Copy(source, 0, result, 0, start);
            Array.Copy(source, start + len, result, start, newSize - start);
        }
    }
}
