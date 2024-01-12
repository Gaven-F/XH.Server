using Mapster;

namespace MpasterDemo.Entities;

[AdaptTo("[name]Dto"), GenerateMapper]
public class Demo
{
	public string Data { get; set; } = string.Empty;
}
