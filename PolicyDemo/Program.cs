var factory = BasicPolicyFactory.GetInstance();

var policies = new List<Policy>
{
	BasicPolicyFactory.GetInstance().AddCondition(new Condition
	{
		FiledName = "Demo",
		T = Condition.Type.E,
		Value = "30"
	}).Create()
};
var data = new { Demo = "Hello" };

policies.OrderBy((it) => it.Order, new CompaterAdapter<byte>((x, y) => y - x));

policies[0].Event += () => Console.WriteLine("Hello");
policies[0].Event += () => Console.WriteLine("Hello");
policies[0].Event += () => Console.WriteLine("Hello");
policies[0].Event += () => Console.WriteLine("Hello");
policies[0].Event += () => Console.WriteLine("Hello");
policies[0].Event += () => Console.WriteLine("Hello");
policies[0].Event += () => Console.WriteLine("Hello");

policies.ForEach(p =>
{
	if (!p.Condition.Any(c => c.Check(data)))
	{
		return;
	}
});

class CompaterAdapter<T> : IComparer<T>
{
	private readonly Func<T?, T?, int> _comparer;
	public CompaterAdapter(Func<T?, T?, int> func) => (_comparer) = (func);
	public int Compare(T? x, T? y) => _comparer(x, y);
}

class Condition
{
	public string? FiledName { get; set; }
	public string? Value { get; set; }
	public Type T { get; set; }


	protected static Dictionary<Type, Func<string, string, bool>> _judgmentFunc = new()
	{
		{Type.E, (string arg1, string arg2) => arg1.Equals(arg2)},
		{Type.G, (string arg1, string arg2) => arg1.CompareTo(arg2) == 1},
		{Type.L, (string arg1, string arg2) => arg1.CompareTo(arg2) == -1},
		{Type.GE, (string arg1, string arg2) => arg1.CompareTo(arg2) >= 0 },
		{Type.LE, (string arg1, string arg2) => arg1.CompareTo(arg2) <= 0},
		{Type.NE, (string arg1, string arg2) => !arg1.Equals(arg2)},
	};

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
		ArgumentNullException.ThrowIfNull(v);

		return GetJudgmentFunc(T)(Value, v);
	}

	[Flags]
	public enum Type { E, G, L, GE, LE, NE }
}

internal interface IPolicy
{
	public List<Condition> Condition { get; set; }
	public byte Order { get; set; }

	public delegate void PolciyAction();
	public event PolciyAction? Event;
	void OnEvent();
}

class Policy : IPolicy
{
	public List<Condition> Condition { get; set; } = [];
	public byte Order { get; set; }

	public event IPolicy.PolciyAction? Event;

	void IPolicy.OnEvent() => Event?.Invoke();

}

class BasicPolicyFactory
{
	private static readonly Lazy<BasicPolicyFactory> instance = new(() => new BasicPolicyFactory());
	public static BasicPolicyFactory GetInstance() => instance.Value;

	private BasicPolicyFactory() { }
	private Policy cachePolicy = new();

	public BasicPolicyFactory AddCondition(Condition condition)
	{
		cachePolicy.Condition.Add(condition);
		return this;
	}

	public Policy Create()
	{
		var p = cachePolicy;
		cachePolicy = new();
		return p;
	}
}

