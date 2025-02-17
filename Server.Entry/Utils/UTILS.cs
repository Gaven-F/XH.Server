﻿using Utils;

namespace Server.Entry.Utils;

public static class UTILS
{
    /// <summary>
    /// 简单的空判断
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="Exception"></exception>
    public static void ThrowExpIfNull(this object? obj)
    {
        if (obj == null)
            ThrowNullExp();
    }

    /// <summary>
    /// 鬼知道我为啥抽风
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static void ThrowNullExp()
    {
        throw new Exception("对象为空！");
    }

    public static string? GetUserNameById(BaseFunc baseFunc, string userId)
    {
        return baseFunc
            .GetAllUserInfo(["姓名"], [userId])
            .ToArray()[0]
            .FieldDataList[0]
            .FieldValueList[0]
            .Value;
    }
}
