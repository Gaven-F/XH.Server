using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH.Core.Database.Entities;

namespace XH.Application.Work;
public interface IBusinessTripService
{
    public long AddTrip(BusinessTrip trip);
    public List<BusinessTrip> GetAllTrip();
    public void ApprovalTrip(long tripId, int status);
    public void DelTrip(long tripId);
}
