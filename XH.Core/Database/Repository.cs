using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XH.Core.DataBase.Tables;

namespace XH.Core.DataBase;
public class Repository<T> : SimpleClient<T> where T : BaseEntry, new()
{
    public Repository(ISqlSugarClient context) : base(context)
    {
    }

    public override List<T> GetList()
    {
        return Context.Queryable<T>().Where(it => !it.IsDelete).ToList();
    }

    public override List<T> GetList(Expression<Func<T, bool>> whereExpression)
    {
        return Context.Queryable<T>().Where(it => !it.IsDelete).Where(whereExpression).ToList();
    }
}
