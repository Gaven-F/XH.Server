using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH.Core.Database.Tables;

namespace XH.Application.Work;
internal interface IBusinessTrip
{
    public long AddTrip(BusinessTrip trip);
    public List<BusinessTrip> GetAllTrip();
    public void AuditTrip(long tripId, int status);
    public void DelTrip(long tripId);
}
