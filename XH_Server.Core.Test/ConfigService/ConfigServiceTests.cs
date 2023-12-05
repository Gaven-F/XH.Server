using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XH_Server.Core.ConfigService.Tests;

[TestClass()]
public class ConfigServiceTests
{
	[TestMethod()]
	public void ConfigServiceTest()
	{
		var configService = new ConfigService();
		Assert.IsNotNull(configService);
	}
}