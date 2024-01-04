using Furion;
using System.Reflection;

namespace XH.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
	public Assembly[] IncludeAssemblies()
	{
		return Array.Empty<Assembly>();
	}

	public string[] IncludeAssemblyNames()
	{
		return new[]
		{
			"XH.Application",
			"XH.Core",
			"XH.Web.Core"
		};
	}
}