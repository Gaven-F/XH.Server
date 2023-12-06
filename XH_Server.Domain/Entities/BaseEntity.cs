using XH_Server.Core.Database;

namespace XH_Server.Domain.Entities;

public class BaseEntity
{
	[SugarColumn(IsPrimaryKey = true, ColumnDescription = "数据唯一Id")]
	public long Id { get; set; }

	[SugarColumn(ColumnDataType = "DATETIME", ColumnDescription = "数据创建时间")]
	public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

	[SugarColumn(ColumnDataType = "DATETIME", ColumnDescription = "数据更新时间")]
	public DateTimeOffset UpdateTime { get; set; } = DateTimeOffset.Now;

	[SugarColumn(ColumnDataType = "BOOL", ColumnDescription = "数据逻辑删除")]
	public bool IsDelete { get; set; } = false;

	[SugarColumn(IsEnableUpdateVersionValidation = true, ColumnDescription = "版本标识")]
	public long Ver { get; set; }

	/// <summary>
	/// 删除数据
	/// </summary>
	/// <param name="dbService">数据库服务</param>
	/// <remarks>仅进行逻辑删除，若需要彻底在数据库中删除请重写此方法</remarks>
	/// <returns></returns>
	public virtual int Delete(DatabaseService dbService)
	{
		IsDelete = true;
		Logging.Create(msg: $"删除数据：{this}").Save(dbService);
		return Update(dbService);
	}

	public virtual int Update(DatabaseService dbService)
	{
		UpdateTime = DateTimeOffset.Now;
		Logging.Create(msg: $"更新数据：{this}").Save(dbService);
		return dbService.Instance.Updateable(this).ExecuteCommandWithOptLock();
	}

	public virtual long Save(DatabaseService dbService)
	{
		Logging.Create(msg: $"存储数据：{this}").Save(dbService);
		return dbService.Instance.Insertable(this).ExecuteReturnSnowflakeId();
	}

	public static IEnumerable<T> GetAllEntities<T>(DatabaseService dbService, bool hasDelete = false) where T : BaseEntity
	{
		return dbService.Instance.Queryable<T>().WhereIF(!hasDelete, it => !it.IsDelete).ToList();
	}
}
