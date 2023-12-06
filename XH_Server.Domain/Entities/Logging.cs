using XH_Server.Core.Database;

namespace XH_Server.Domain.Entities;
public class Logging : BaseEntity
{
	public static readonly string LoggingTemplate =
		"[{className} - {methodName}]:\r\n" +
		"[{msg}]\r\n" +
		"------------";

	public string ClassName { get; set; } = string.Empty;
	public string MethodName { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public LoggingLevel Level = LoggingLevel.Info;

	public static Logging Create(string className = "NONE", string methodName = "NONE", string msg = "NONE")
	{
		var instance = new Logging()
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



