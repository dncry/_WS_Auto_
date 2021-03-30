/*----------------------------------------------------------------
** 项目名称: _Tools
** 文件名称：IntExtension
** 创 建 者：万硕
** 创建时间：2021年03月30日 星期二 14:32
** 文件版本：V1.0.0
* ===============================================================
** 功能描述：
**		
**
**----------------------------------------------------------------*/


namespace WS.Auto
{
    public static class IntExtension
    {
        public static string ToLevelString(this int level)
        {
            return $"Level_{level.ToString().PadLeft(4, '0')}";
        }
    }
}