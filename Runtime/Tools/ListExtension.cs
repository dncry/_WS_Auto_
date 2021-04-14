/*----------------------------------------------------------------
** 项目名称: _Tools
** 文件名称：ListExtension
** 创 建 者：万硕
** 创建时间：2021年04月14日 星期三 09:19
** 文件版本：V1.0.0
* ===============================================================
** 功能描述：
**		
**
**----------------------------------------------------------------*/


using System;
using System.Collections.Generic;

namespace WS.Auto
{
    public static class ListExtension
    {
        public static List<T> FluentAdd<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> FluentClear<T>(this List<T> list)
        {
            list.Clear();
            return list;
        }

        public static List<T> FluentForEach<T>(this List<T> list, Action<T> action)
        {
            list.ForEach(action);
            return list;
        }

        public static List<T> FluentInsert<T>(this List<T> list, int index, T item)
        {
            list.Insert(index, item);
            return list;
        }

        public static List<T> FluentRemoveAt<T>(this List<T> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public static List<T> FluentReverse<T>(this List<T> list)
        {
            list.Reverse();
            return list;
        }
    }
}