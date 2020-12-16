namespace Flux.Model
{
	public struct Rule
		: System.IEquatable<Rule>
	{
		public string Name { get; }
		public string Operator { get; }
		public string Value { get; }

		public Rule(string Name, string Operator, string Value)
		{
			this.Name = Name;
			this.Operator = Operator;
			this.Value = Value;
		}

		// Statics
		//public static bool IsBinaryBoolean(string value)
		//{
		//	try
		//	{
		//		if (System.Linq.Expressions.ExpressionType.TryParse(value, out System.Linq.Expressions.ExpressionType expressionType))
		//		{
		//			var binaryExpression = System.Linq.Expressions.Expression.MakeBinary(expressionType, System.Linq.Expressions.Expression.Constant(0), System.Linq.Expressions.Expression.Constant(0));

		//			var lambda = System.Linq.Expressions.Expression.Lambda(binaryExpression, null);

		//			return lambda.ReturnType == typeof(bool);
		//		}
		//	}
		//	catch { }

		//	return false;
		//}

		// Operators
		public static bool operator ==(Rule a, Rule b)
			=> a.Equals(b);
		public static bool operator !=(Rule a, Rule b)
			=> !a.Equals(b);

		// IEquatable
		public bool Equals(Rule other)
			=> Name == other.Name && Operator == other.Operator && Value == other.Value;

		// Object (overrides)
		public override bool Equals(object? obj)
			=> obj is Rule o && Equals(o);
		public override int GetHashCode()
			=> System.HashCode.Combine(Name, Operator, Value);
		public override string? ToString()
			=> $"<{nameof(Rule)}: \"{Name}\" {Operator} '{Value}'>";
	}

	public class RuleCollection
	{
		public System.Collections.Generic.IList<Flux.Model.Rule> m_rules = new System.Collections.Generic.List<Flux.Model.Rule>();

		public void Add(Rule rule)
		{
			m_rules.Add(rule);
		}

		public void Evaluate<T>(T value)
		{
			foreach (var rule in m_rules)
				if (!Flux.Model.RulesEngine.CompileRule<T>(rule)(value))
					throw new System.ArgumentOutOfRangeException(nameof(value));
		}
	}
	public class RuleCollection<T>
	{
		public System.Collections.Generic.IDictionary<Flux.Model.Rule, System.Func<T, bool>> m_rules = new System.Collections.Generic.Dictionary<Flux.Model.Rule, System.Func<T, bool>>();

		public void Add(Rule rule)
		{
			m_rules.Add(rule, Flux.Model.RulesEngine.CompileRule<T>(rule));
		}

		public bool Evaluate(T value)
		{
			var exceptions = new System.Collections.Generic.List<System.Exception>();

			foreach (var rule in m_rules)
				if (!rule.Value(value))
					exceptions.Add(new System.ArgumentOutOfRangeException(nameof(value), $"{rule.Key}."));

			try
			{
				if (exceptions.Count > 0)
					throw new System.AggregateException($"{value} passed {m_rules.Count - exceptions.Count} and failed {exceptions.Count} rules.", exceptions);
			}
			catch (System.Exception se)
			{ }

			return exceptions.Count == 0;
		}
	}
}
