using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core;
public static class UTILS
{
    /// <summary>
    /// 简单的空判断
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="Exception"></exception>
    public static void ThrowExpIfNull(this object? obj)
    {
        if (obj == null) ThrowNullExp();
    }

    /// <summary>
    /// 鬼知道我为啥抽风
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static void ThrowNullExp()
    {
        throw new Exception("对象为空！");
    }
}
