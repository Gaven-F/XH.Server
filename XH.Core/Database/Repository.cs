using SqlSugar;

namespace XH.Core.DataBase;
public class Repository<T> : SimpleClient<T> where T : class, new()
{
    public Repository(ISqlSugarClient context) : base(context)
    {
    }
}
