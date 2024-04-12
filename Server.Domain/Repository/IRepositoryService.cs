using Server.Core.Database;
using Server.Domain.Basic;

namespace Server.Domain.Repository;

public interface IRepositoryService<T> where T : BasicEntity, new()
{
    T GetDataById(long id);

    IEnumerable<T> GetData(bool isDelete = false);

    long SaveData(T e);

    int UpdateData(T e);

    int DeleteData(long eId);

    DatabaseService GetDb();
}