namespace Server.Core;
public static class COMMON
{
    public static readonly string TC = "T_C";

    /// <summary>
    /// 检测是否有重复值
    /// </summary>
    /// <exception cref="Exception"></exception>
    static COMMON()
    {
        var c = typeof(COMMON);
        var fs =
            c.GetFields(System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Default)
            .ToList();
        var set = new HashSet<string>();

        fs.ForEach(f =>
        {
            if (f.FieldType != typeof(string))
            {
                return;
            }
            var value = Convert.ToString(f.GetValue(null));
            if (value == null)
            {
                return;
            }
            if (!set.Add(value))
            {
                throw new Exception($"值重复出现！（{f.Name}：{value}）");
            }
        });
        set = null;
    }

}
