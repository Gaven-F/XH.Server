using SqlSugar;
using XH.Core.Database.Entities;

namespace XH.Application.Work;
public partial class WorkService : IBusinessTripService
{
    private SimpleClient<BusinessTrip> _repository;

    public WorkService(SimpleClient<BusinessTrip> repository)
    {
        _repository = repository;
    }

    public long AddTrip(BusinessTrip trip)
    {
        _repository.InsertReturnSnowflakeId(trip);
        return 0;
    }

    public void ApprovalTrip(long tripId, int status)
    {
        throw new NotImplementedException();
    }

    public void DelTrip(long tripId)
    {
        throw new NotImplementedException();
    }

    public List<BusinessTrip> GetAllTrip()
    {
        throw new NotImplementedException();
    }
}
