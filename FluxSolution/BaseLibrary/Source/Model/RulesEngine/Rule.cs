namespace Flux.Model
{
	public struct Rule
		: System.IEquatable<Rule>
	{
		public string Name { get; }
		public string Operator { get; }
		public object Value { get; }

		public Rule(string name, string @operator, string value)
		{
			Name = name;
			Operator = @operator;
			Value = value;
		}
		[System.CLSCompliant(false)]
		public Rule(string name, string @operator, System.IConvertible value)
		{
			Name = name;
			Operator = @operator;
			Value = value;
		}

		public System.Func<T, bool> Compile<T>()
			=> RulesEngine.CompileRule<T>(this);

		// Operators
		public static bool operator ==(Rule a, Rule b)
			=> a.Equals(b);
		public static bool operator !=(Rule a, Rule b)
			=> !a.Equals(b);
		// System.IEquatable<Rule>
		public bool Equals(Rule other)
			=> Name == other.Name && Operator == other.Operator && Value == other.Value;
		// Overrides
		public override bool Equals(object? obj)
			=> obj is Rule o && Equals(o);
		public override int GetHashCode()
			=> System.HashCode.Combine(Name, Operator, Value);
		public override string? ToString()
			=> $"<{nameof(Rule)}: \"{Name}\" {Operator} '{Value}'>";
	}
}
