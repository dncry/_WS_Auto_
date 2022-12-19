/*----------------------------------------------------------------
** Creator：Dncry
** Time：2022年11月01日 星期二 15:24
----------------------------------------------------------------*/

using UnityEngine;

namespace WS.Auto
{
    public static class LongExtension
    {
        public static string ToSimpleString(this long num)
        {
            var numStr = num.ToString();
            var numLength = numStr.Length;
            
            if (numLength < 4)
            {
                return numStr;
            }

            if (numLength < 7)
            {
                if (numLength % 3 == 0)
                {
                    return $"{numStr[0]}{numStr[1]}{numStr[2]}K";
                }
                if (numLength % 3 == 1)
                {
                    return $"{numStr[0]}.{numStr[1]}{numStr[2]}K";
                }
                if (numLength % 3 == 2)
                {
                    return $"{numStr[0]}{numStr[1]}.{numStr[2]}K";
                }
            }

            if (numLength < 10)
            {
                if (numLength % 3 == 0)
                {
                    return $"{numStr[0]}{numStr[1]}{numStr[2]}M";
                }
                if (numLength % 3 == 1)
                {
                    return $"{numStr[0]}.{numStr[1]}{numStr[2]}M";
                }
                if (numLength % 3 == 2)
                {
                    return $"{numStr[0]}{numStr[1]}.{numStr[2]}M";
                }
            }
            
            if (numLength < 13)
            {
                if (numLength % 3 == 0)
                {
                    return $"{numStr[0]}{numStr[1]}{numStr[2]}B";
                }
                if (numLength % 3 == 1)
                {
                    return $"{numStr[0]}.{numStr[1]}{numStr[2]}B";
                }
                if (numLength % 3 == 2)
                {
                    return $"{numStr[0]}{numStr[1]}.{numStr[2]}B";
                }
            }
            
            if (numLength < 16)
            {
                if (numLength % 3 == 0)
                {
                    return $"{numStr[0]}{numStr[1]}{numStr[2]}T";
                }
                if (numLength % 3 == 1)
                {
                    return $"{numStr[0]}.{numStr[1]}{numStr[2]}T";
                }
                if (numLength % 3 == 2)
                {
                    return $"{numStr[0]}{numStr[1]}.{numStr[2]}T";
                }
            }

            return numStr;
        }
    }
}