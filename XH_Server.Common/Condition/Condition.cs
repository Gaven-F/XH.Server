namespace XH_Server.Common.Condition;
public class Condition
{
	private static readonly char[] _separator = ['-', ' ', ','];

	private string JudgmentField { get; set; } = string.Empty;
	private JudgmentType Type { get; set; } = JudgmentType.E;
	private string Value { get; set; } = string.Empty;

	private Condition() { }

	private static Exception GetException()
	{
		return new ArgumentOutOfRangeException("类型错误");
	}

	public static Condition Parse(string raw)
	{
		var splitRaw = raw.Split(_separator);

		var c = new Condition
		{
			JudgmentField = splitRaw[0],
			Type = (JudgmentType)Enum.Parse(typeof(JudgmentType), splitRaw[1].ToUpper()),
			Value = splitRaw[2]
		};
		return c;
	}

	public bool Check(object data)
	{
		return Check(this, data, GetException());
	}

	public static bool Check(string condition, object data)
	{
		return Check(Parse(condition), data, GetException());
	}

	public static bool Check(Condition condition, object data, Exception exception)
	{
		var property = data.GetType().GetProperty(condition.JudgmentField)
			?? throw new ArgumentException($"Property {condition.JudgmentField} not found in {data.GetType().Name}");
		var value = property.GetValue(obj: data)!.ToString() ?? "";

		return condition.Type switch
		{
			JudgmentType.E => value == condition.Value,
			JudgmentType.G => value.CompareTo(condition.Value) > 0,
			JudgmentType.L => value.CompareTo(condition.Value) < 0,
			JudgmentType.GE => value.CompareTo(condition.Value) >= 0,
			JudgmentType.LE => value.CompareTo(condition.Value) <= 0,
			JudgmentType.NE => value != condition.Value,
			_ => throw exception
		};
	}


	public override string ToString()
	{
		return $"{JudgmentField}-{Type}-{Value}";
	}

}

[Flags]
public enum JudgmentType
{
	/// <summary>
	/// Equal,Greate,Less,GreateEqual,LessEqual,NotEqual
	/// </summary>
	E, G, L, GE, LE, NE
}
