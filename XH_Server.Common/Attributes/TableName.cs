namespace XH_Server.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NameAttribute(string name) : Attribute
{
	public string Name { get; set; } = name;
}
