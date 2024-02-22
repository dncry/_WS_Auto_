﻿/*----------------------------------------------------------------
** 项目名称: _Tools
** 文件名称：IntExtension
** 创 建 者：dncry
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
            if (level % 10000 > 500)
            {
                return "Lv_MAX";
            }
            
            return $"Lv_{level.ToString().PadLeft(5, '0')}";
        }
    }
}