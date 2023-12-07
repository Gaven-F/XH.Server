using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.DataEntities;
public class EStaff : BasicEntity
{
	public string UserId { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Department { get; set; } = string.Empty;

	public static EStaff GetStaffFromDb(DatabaseService dbService, string userId)
	{
		// TODO 当用户数据未录入时考虑录入数据
		return dbService.Instance.Queryable<EStaff>().Single(it => it.UserId == userId);
	}
}
