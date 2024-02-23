using UnityEngine;

namespace WS.Auto
{
    public static class CharExtension
    {
        public static int ToInt(this char c)
        {
            return c - '0';
        }
    }
}