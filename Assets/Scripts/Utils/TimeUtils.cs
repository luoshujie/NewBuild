using System;

public static class TimeUtils
{
    /// <summary>
    /// 获取当前时间的时间戳
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentTimestamp()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentTimestamp = (int)(DateTime.UtcNow - epochStart).TotalMilliseconds;
        return currentTimestamp;
    }
}
