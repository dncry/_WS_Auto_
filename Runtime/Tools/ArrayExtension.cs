/*----------------------------------------------------------------
** Creator：Dncry
** Time：2024年02月23日 星期五 12:01
----------------------------------------------------------------*/

namespace WS.Auto
{
    public static class ArrayExtension
    {
        public static T GetLatest<T>(this T[] group, int value = 1)
        {
            return group[group.Length - value];
        }
        
        
        
    }
}
