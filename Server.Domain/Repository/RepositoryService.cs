using Server.Core.Database;
using Server.Domain.Basic;

namespace Server.Domain.Repository;

public class RepositoryService<T>(DatabaseService dbService) : IRepositoryService<T> where T : BasicEntity, new()
{
    public int DeleteData(long eId)
    {
        var entity = dbService.Instance.Queryable<T>().InSingle(eId) ?? throw new Exception("数据不存在，请检测ID是否正确！");

        entity.IsDeleted = true;
        return dbService.Instance.Updateable(entity).ExecuteCommand();
    }

    public IEnumerable<T> GetData(bool isDelete = false)
    {
        return dbService.Instance.Queryable<T>().Where(e => e.IsDeleted == isDelete).ToList();
    }

    public T GetDataById(long id)
    {
        return dbService.Instance.Queryable<T>().Single(e => e.Id == id);
    }

    public DatabaseService GetDb() => dbService;

    public long SaveData(T e)
    {
        return dbService.Instance.Insertable(e).ExecuteReturnSnowflakeId();
    }

    public int UpdateData(T e)
    {
        e.UpdateTime = DateTime.Now;
        return dbService.Instance.Updateable(e).ExecuteCommand();
    }
}