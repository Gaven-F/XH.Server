using Furion;
using System.Reflection;

namespace XH_Server.Web;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return [];
    }

    public string[] IncludeAssemblyNames()
    {
        return [
            "XH_Server.Core",
            "XH_Server.Common",
            "XH_Server.Domain",
            "XH_Server.Application",
        ];
    }
}
