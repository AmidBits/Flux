using System;

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

  public class RulesDictionary<T>
    : System.Collections.Generic.IDictionary<Flux.Model.Rule, System.Func<T, bool>>
  {
    public System.Collections.Generic.IDictionary<Flux.Model.Rule, System.Func<T, bool>> m_rules = new System.Collections.Generic.Dictionary<Flux.Model.Rule, System.Func<T, bool>>();

    //public System.Collections.Generic.IList<Flux.Model.Rule> m_rules = new System.Collections.Generic.List<Flux.Model.Rule>();


    //public void Add(Rule rule)
    //{
    //	m_rules.Add(rule, Flux.Model.RulesEngine.CompileRule<T>(rule));
    //}

    //public bool Evaluate(T value)
    //{
    //	var exceptions = new System.Collections.Generic.List<System.Exception>();

    //	foreach (var rule in m_rules)
    //		if (!rule.Value(value))
    //			exceptions.Add(new System.ArgumentOutOfRangeException(nameof(value), $"{rule.Key}."));

    //	try
    //	{
    //		if (exceptions.Count > 0)
    //			throw new System.AggregateException($"{value} passed {m_rules.Count - exceptions.Count} and failed {exceptions.Count} rules.", exceptions);
    //	}
    //	catch (System.Exception se)
    //	{ }

    //	return exceptions.Count == 0;
    //}

    #region IDictionary implementation
    public Func<T, bool> this[Rule key] { get => m_rules[key]; set => throw new System.NotImplementedException(); }

    public System.Collections.Generic.ICollection<Rule> Keys
      => m_rules.Keys;
    public System.Collections.Generic.ICollection<Func<T, bool>> Values
      => m_rules.Values;
    public int Count
      => m_rules.Count;
    public bool IsReadOnly
      => false;

    public void Add(Rule key, Func<T, bool> value)
      => m_rules.Add(key, value);
    public void Add(System.Collections.Generic.KeyValuePair<Rule, Func<T, bool>> item)
      => m_rules.Add(item);
    public void Clear()
      => m_rules.Clear();
    public bool Contains(System.Collections.Generic.KeyValuePair<Rule, Func<T, bool>> item)
      => m_rules.Contains(item);
    public bool ContainsKey(Rule key)
      => m_rules.ContainsKey(key);
    public void CopyTo(System.Collections.Generic.KeyValuePair<Rule, Func<T, bool>>[] array, int arrayIndex)
      => m_rules.CopyTo(array, arrayIndex);
    public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<Rule, Func<T, bool>>> GetEnumerator()
      => m_rules.GetEnumerator();
    public bool Remove(Rule key)
      => m_rules.Remove(key);
    public bool Remove(System.Collections.Generic.KeyValuePair<Rule, Func<T, bool>> item)
      => m_rules.Remove(item);
    public bool TryGetValue(Rule key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Func<T, bool> value)
      => m_rules.TryGetValue(key, out value);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => m_rules.GetEnumerator();
    #endregion IDictionary implementation

    //public bool All()
    //public bool Any()
  }
}
