using System.Linq;

namespace Flux.RulesEngine
{
	public sealed class RulesDictionary
		: System.Collections.Generic.IDictionary<string, Rule>
	{
		private readonly System.Collections.Generic.IDictionary<string, Rule> m_rules = new System.Collections.Generic.Dictionary<string, Rule>();

		#region IDictionary implementation
		public Rule this[string key] { get => m_rules[key]; set => m_rules[key] = value; }

		public System.Collections.Generic.ICollection<string> Keys
			=> m_rules.Keys;

		public System.Collections.Generic.ICollection<Rule> Values
			=> m_rules.Values;

		public int Count
			=> m_rules.Count;

		public bool IsReadOnly
			=> m_rules.IsReadOnly;

		public void Add(string key, Rule value)
			=> m_rules.Add(key, value);

		public void Add(System.Collections.Generic.KeyValuePair<string, Rule> item)
			=> m_rules.Add(item);

		public void Clear()
			=> m_rules.Clear();

		public bool Contains(System.Collections.Generic.KeyValuePair<string, Rule> item)
			=> m_rules.Contains(item);

		public bool ContainsKey(string key)
			=> throw new System.NotImplementedException();

		public void CopyTo(System.Collections.Generic.KeyValuePair<string, Rule>[] array, int arrayIndex)
			=> m_rules.CopyTo(array, arrayIndex);

		public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string, Rule>> GetEnumerator()
			=> m_rules.GetEnumerator();

		public bool Remove(string key)
			=> m_rules.Remove(key);

		public bool Remove(System.Collections.Generic.KeyValuePair<string, Rule> item)
			=> m_rules.Remove(item);

		public bool TryGetValue(string key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Rule value)
			=> m_rules.TryGetValue(key, out value);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> m_rules.GetEnumerator();
		#endregion IDictionary implementation

		/// <summary>Create a new dictionary with the specified rules compiled.</summary>
		[System.Diagnostics.Contracts.Pure]
		public RulesDictionary<T> CompileRules<T>(params string[] ruleName)
			=> new(m_rules.Where(kvp => ruleName.Contains(kvp.Key)));
		/// <summary>Create a new dictionary with all rules compiled.</summary>
		[System.Diagnostics.Contracts.Pure]
		public RulesDictionary<T> CompileRules<T>()
			=> new(this);
	}

	public sealed class RulesDictionary<T>
		: System.Collections.Generic.IReadOnlyDictionary<string, System.Func<T, bool>>
	{
		private readonly System.Collections.Generic.IDictionary<string, System.Func<T, bool>> m_rules = new System.Collections.Generic.Dictionary<string, System.Func<T, bool>>();

		public RulesDictionary(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, Rule>> rulesDictionary)
		{
			foreach (var kvp in rulesDictionary ?? throw new System.ArgumentNullException(nameof(rulesDictionary)))
				Add(kvp.Key, kvp.Value);
		}

		#region IDictionary implementation
		public System.Func<T, bool> this[string key] { get => m_rules[key]; set => throw new System.NotImplementedException(); }

		public System.Collections.Generic.IEnumerable<string> Keys
			=> m_rules.Keys;
		public System.Collections.Generic.IEnumerable<System.Func<T, bool>> Values
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

		/// <summary>Create a new dictionary by evaluating the specified value against the compiled rules. The result contains the rule name and whether it succeeded.</summary>
		[System.Diagnostics.Contracts.Pure]
		public System.Collections.Generic.IDictionary<string, bool> EvaluateRules(T value)
			=> m_rules.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Invoke(value));
	}
}
