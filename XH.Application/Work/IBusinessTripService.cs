using XH.Core.Database.Entities;

namespace XH.Application.Work;
public interface IBusinessTripService
{
    public long AddTrip(BusinessTrip trip);
    public List<BusinessTrip> GetAllTrip();
    public void ApprovalTrip(long tripId, int status);
    public void DelTrip(long tripId);
}
