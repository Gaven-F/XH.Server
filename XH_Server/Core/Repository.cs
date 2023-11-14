using SqlSugar;
using System.Linq.Expressions;
using XH_Server.Domain.Entities;

namespace XH_Server.Core;

public class Repository<T> : SimpleClient<T> where T : BaseEntity, new()
{
	public Repository(ISqlSugarClient context) : base(context)
	{

	}

	public override List<T> GetList()
	{
		return Context.Queryable<T>()
			.Where(it => !it.IsDelete)
			.ToList();
	}

	public override List<T> GetList(Expression<Func<T, bool>> whereExpression)
	{
		return Context.Queryable<T>()
			.Where(it => !it.IsDelete)
			.Where(whereExpression)
			.ToList();
	}
}
