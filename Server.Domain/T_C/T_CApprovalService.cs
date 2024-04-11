using Server.Core.Database;
using Server.Domain.Basic;

namespace Server.Domain.T_C;


#pragma warning disable IDE1006
public class T_CApprovalService(DatabaseService bd)
#pragma warning restore
{
    public void CreateApproval(BasicEntity tC)
    {
        bd.Instance.Insertable(tC).ExecuteCommand();
    }
}

