using System.Linq.Expressions;
using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Repository;

public class RepositoryService<T>(DatabaseService databaseService) : SimpleClient<T>(databaseService.Instance)
	where T : BasicEntity, new()
{
	public override List<T> GetList() => base.GetList()
		.Where(it => !it.IsDelete).ToList();

	public override List<T> GetList(Expression<Func<T, bool>> whereExpression) => base.GetList(whereExpression)
		.Where(it => !it.IsDelete).ToList();

	public override Task<List<T>> GetListAsync() => base.GetListAsync()
		.ContinueWith(it => it.Result.Where(e => !e.IsDelete).ToList());
}
