using System.Reflection;
using Furion;

namespace Server.Web;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return [];
    }

    public string[] IncludeAssemblyNames()
    {
        return ["Server.Core", "Server.Common", "Server.Domain", "Server.Application",];
    }
}
