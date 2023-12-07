using XH_Server.Core.Database;
using XH_Server.Domain.Basic;

namespace XH_Server.Domain.Logging;
public class ELogging : BasicEntity
{
    public static readonly string LoggingTemplate =
        """
		------------
		[{className} - {methodName}]
		[{msg}]
		------------
		""";


    public string ClassName { get; set; } = string.Empty;
    public string MethodName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public LoggingLevel Level = LoggingLevel.Info;

    public static ELogging Create(string className = "NONE", string methodName = "NONE", string msg = "NONE")
    {
        var instance = new ELogging()
        {
            ClassName = className,
            MethodName = methodName,
            Message = msg
        };
        return instance;
    }

    public override int Update(DatabaseService dbService)
    {
        throw new Exception("日志记录禁止更新！");
    }

    public override string ToString() => string.Format(LoggingTemplate, ClassName, MethodName, Message);
}

[Flags]
public enum LoggingLevel
{
    Track, Info, Debug, Warn, Error, Fatal
}



