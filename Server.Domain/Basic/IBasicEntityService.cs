using Server.Core.Database;

namespace Server.Domain.Basic;

public interface IBasicEntityService<T>
    where T : BasicEntity
{
    long Create(T e);

    int Update(T e);

    int Delete(long eId);

    T GetEntityById(long id);

    IEnumerable<T> GetEntities(bool isDelete = false);

    DatabaseService GetDb();
}
