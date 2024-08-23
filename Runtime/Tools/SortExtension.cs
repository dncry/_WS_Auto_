/*----------------------------------------------------------------
** Creator：Dncry
** Time：2024年08月23日 星期五 15:56
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace _Tools
{
    public static class SortExtension
    {
        public static List<T> ToShuffleList<T>(this List<T> list)
        {
            List<T> newList = new List<T>(list);

            System.Random random = new System.Random();

            int n = newList.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(0, n + 1);
                T value = newList[k];
                newList[k] = newList[n];
                newList[n] = value;
            }

            return newList;
        }
        
    }
}