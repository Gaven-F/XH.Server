namespace Server.Entry.Utils;

public static class Extensions
{
    public static List<T> C_Where<T>(this List<T>? thisValue, Func<T, bool> whereExpression) where T : BasicEntity, new() =>
        thisValue == null ? [] : thisValue.Where(whereExpression).ToList();
}
