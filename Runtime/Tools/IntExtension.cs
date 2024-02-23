
namespace WS.Auto
{
    public static class IntExtension
    {
        public static string ToLevelString(this int level)
        {
            if (level % 10000 > 500)
            {
                return "Lv_MAX";
            }
            
            return $"Lv_{level.ToString().PadLeft(5, '0')}";
        }
    }
}