using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

	public class RulesDictionary
		: System.Collections.Generic.IDictionary<string, Flux.Model.Rule>
	{
		private System.Collections.Generic.IDictionary<string, Flux.Model.Rule> m_rules = new System.Collections.Generic.Dictionary<string, Flux.Model.Rule>();

		#region IDictionary implementation
		public Rule this[string key] { get => m_rules[key]; set => m_rules[key] = value; }

		public ICollection<string> Keys
			=> m_rules.Keys;

		public ICollection<Rule> Values
			=> m_rules.Values;

		public int Count
			=> m_rules.Count;

		public bool IsReadOnly
			=> m_rules.IsReadOnly;

		public void Add(string key, Rule value)
			=> m_rules.Add(key, value);

		public void Add(KeyValuePair<string, Rule> item)
			=> m_rules.Add(item);

		public void Clear()
			=> m_rules.Clear();

		public bool Contains(KeyValuePair<string, Rule> item)
			=> m_rules.Contains(item);

		public bool ContainsKey(string key)
			=> throw new System.NotImplementedException();

		public void CopyTo(KeyValuePair<string, Rule>[] array, int arrayIndex)
			=> m_rules.CopyTo(array, arrayIndex);

		public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string, Rule>> GetEnumerator()
			=> m_rules.GetEnumerator();

		public bool Remove(string key)
			=> m_rules.Remove(key);

		public bool Remove(KeyValuePair<string, Rule> item)
			=> m_rules.Remove(item);

		public bool TryGetValue(string key, [MaybeNullWhen(false)] out Rule value)
			=> m_rules.TryGetValue(key, out value);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> m_rules.GetEnumerator();
		#endregion IDictionary implementation

		public RulesDictionary<T> CompileRules<T>(params string[] ruleName)
			=> new RulesDictionary<T>(m_rules.Where(kvp => ruleName.Contains(kvp.Key)));
		public RulesDictionary<T> CompileRules<T>()
			=> new RulesDictionary<T>(this);
	}

	public class RulesDictionary<T>
		: System.Collections.Generic.IReadOnlyDictionary<string, System.Func<T, bool>>
	{
		private System.Collections.Generic.IDictionary<string, System.Func<T, bool>> m_rules = new System.Collections.Generic.Dictionary<string, System.Func<T, bool>>();

		public RulesDictionary(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, Rule>> rulesDictionary)
		{
			foreach (var kvp in rulesDictionary ?? throw new System.ArgumentNullException(nameof(rulesDictionary)))
				Add(kvp.Key, kvp.Value);
		}

		#region IDictionary implementation
		public System.Func<T, bool> this[string key] { get => m_rules[key]; set => throw new System.NotImplementedException(); }

		public IEnumerable<string> Keys
			=> m_rules.Keys;
		public IEnumerable<Func<T, bool>> Values
			=> m_rules.Values;
		public int Count
			=> m_rules.Count;
		public bool IsReadOnly
			=> false;

		public void Add(string key, System.Func<T, bool> lambda)
			=> m_rules.Add(key, lambda);
		public void Add(string key, Rule rule)
			=> Add(key, rule.Compile<T>());
		public void Add(System.Collections.Generic.KeyValuePair<string, System.Func<T, bool>> item)
			=> m_rules.Add(item);
		public void Clear()
			=> m_rules.Clear();
		public bool Contains(System.Collections.Generic.KeyValuePair<string, System.Func<T, bool>> item)
			=> m_rules.Contains(item);
		public bool ContainsKey(string key)
			=> m_rules.ContainsKey(key);
		public void CopyTo(System.Collections.Generic.KeyValuePair<string, System.Func<T, bool>>[] array, int arrayIndex)
			=> m_rules.CopyTo(array, arrayIndex);
		public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string, System.Func<T, bool>>> GetEnumerator()
			=> m_rules.GetEnumerator();
		public bool Remove(string key)
			=> m_rules.Remove(key);
		public bool Remove(System.Collections.Generic.KeyValuePair<string, System.Func<T, bool>> item)
			=> m_rules.Remove(item);
		public bool TryGetValue(string key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out System.Func<T, bool> value)
			=> m_rules.TryGetValue(key, out value);
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> m_rules.GetEnumerator();
		#endregion IDictionary implementation

		public System.Collections.Generic.IDictionary<string, bool> RulesEvaluation(T value)
			=> m_rules.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Invoke(value));
	}
}
