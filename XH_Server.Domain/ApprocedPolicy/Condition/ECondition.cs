using XH_Server.Domain.Basic;

namespace XH_Server.Domain.ApprocedPolicy.Condition;
[Obsolete]
public class ConditionV1
{
	private static readonly char[] _separator = ['-', ' ', ','];

	private string JudgmentField { get; set; } = string.Empty;
	private JudgmentType Type { get; set; } = JudgmentType.E;
	private string Value { get; set; } = string.Empty;

	private ConditionV1() { }

	public static ConditionV1 Default() => new()
	{
		JudgmentField = "NONE"
	};

	private static ArgumentOutOfRangeException GetException() => new("类型错误");

	public static ConditionV1 Parse(string raw)
	{
		var splitRaw = raw.Split(_separator);

		var c = new ConditionV1
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

	public static bool Check(ConditionV1 condition, object data, Exception exception)
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



public class ECondition : BasicEntity
{
	private static readonly char[] _separator = ['-', ' ', ','];
	public long PolicyId { get; set; }
	public string? FiledName { get; set; }
	public string? Value { get; set; }
	public Type T { get; set; }


	protected static Dictionary<Type, Func<string, string, bool>> _judgmentFunc = new()
	{
		{Type.E, (arg1, arg2) => arg1.Equals(arg2)},
		{Type.G, (arg1, arg2) => arg1.CompareTo(arg2) == 1},
		{Type.L, (arg1, arg2) => arg1.CompareTo(arg2) == -1},
		{Type.GE, (arg1, arg2) => arg1.CompareTo(arg2) >= 0 },
		{Type.LE, (arg1, arg2) => arg1.CompareTo(arg2) <= 0},
		{Type.NE, (arg1, arg2) => !arg1.Equals(arg2)},
	};

	public static ECondition Parse(string raw)
	{
		var splitRaw = raw.Split(_separator);

		var c = new ECondition
		{
			FiledName = splitRaw[0],
			T = (Type)Enum.Parse(typeof(JudgmentType), splitRaw[1].ToUpper()),
			Value = splitRaw[2]
		};
		return c;
	}
	public static Func<string, string, bool> GetJudgmentFunc(string t) => _judgmentFunc[Enum.Parse<Type>(t)];
	public static Func<string, string, bool> GetJudgmentFunc(Type t) => _judgmentFunc[t];

	public bool Check(object o)
	{
		ArgumentNullException.ThrowIfNull(Value);
		ArgumentNullException.ThrowIfNull(FiledName);

		var t = o.GetType();
		var property = t.GetProperty(FiledName);
		var filed = t.GetField(FiledName);

		var v = (property?.GetValue(o) ?? filed?.GetValue(o))?.ToString();
		//ArgumentNullException.ThrowIfNull(v);

		return v != null ? GetJudgmentFunc(T)(v, Value) : false;
	}

	public override string ToString()
	{
		return $"{FiledName}-{T}-{Value}";
	}

	public enum Type { E, G, L, GE, LE, NE }
}
