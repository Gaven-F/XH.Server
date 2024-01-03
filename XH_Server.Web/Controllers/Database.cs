﻿using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Entities;
using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Web.Controllers;

public class Database(DatabaseService databaseService) : ControllerBase
{
	public void DatabaseInit()
	{
		databaseService.InitDatabase();
		List<Type> tables =
		[
			.. typeof(EBussinessTrip).Assembly.GetTypes().Where(t => t.BaseType == typeof(BasicEntity)).ToList(),
			.. typeof(BasicEntity).Assembly.GetTypes().Where(t => t.BaseType == typeof(BasicEntity)).ToList(),
		];
		databaseService.InitTable(tables.Distinct());
	}
}
