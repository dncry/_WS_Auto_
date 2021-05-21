/*----------------------------------------------------------------
** Creator：万硕
** Time：2021年05月21日 星期五 15:04
----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WS.Auto
{
    public static class RandomExtension
    {
        public static T GetRandomValue<T>(this List<T> group)
        {
            return group[Random.Range(0, group.Count)];
        }

        public static T GetRandomValue<T>(this T[] group)
        {
            return group[Random.Range(0, group.Length)];
        }

        public static List<T> GetRandomCountGroup<T>(this List<T> group, int count)
        {
            var groupLength = group.Count;

            if (groupLength < count)
            {
                Debug.LogWarning("越界");
                return null;
            }

            return GetRandomCountGroupWithList(group.ToList(),
                groupLength - count);
        }

        public static List<T> GetRandomCountGroup<T>(this T[] group, int count)
        {
            var groupLength = group.Length;

            if (groupLength < count)
            {
                Debug.LogWarning("越界");
                return null;
            }

            return GetRandomCountGroupWithList(group.ToList(), groupLength - count);
        }

        private static List<T> GetRandomCountGroupWithList<T>(List<T> group, int removeCount)
        {
            for (int i = 0; i < removeCount; i++)
            {
                group.Remove(group.GetRandomValue());
            }

            return group;
        }
    }
}