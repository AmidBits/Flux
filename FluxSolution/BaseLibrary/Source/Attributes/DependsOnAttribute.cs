namespace Flux
{
  /// <summary>Dependancy attribute to build automatic dependency notifications using INotifyPropertyChanged for methods and properties.</summary>
  /// <remarks>When used on ICommand.Execute then the Parameters will always be null.</remarks>
  [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Property, AllowMultiple = true)]
  public sealed class DependsOnAttribute
    : System.Attribute
  {
    private readonly string m_dependencyMemberName;

    public DependsOnAttribute(string dependencyMemberName) => m_dependencyMemberName = dependencyMemberName;

    public string DependencyMemberName => m_dependencyMemberName;

    /// <summary>Reverse dependency names based on the DependsOn attribute.</summary>

    public static System.Collections.Generic.IDictionary<string, string[]> MapDependencies(System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> source)
    {
      // mapping = Dictionary<memberName, dependencyMemberNames[]> (one-to-many)
      var mapping = source.ToDictionary(mi => mi.Name, mi => mi.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>().Select(a => a.m_dependencyMemberName).Distinct().ToArray());

      // flatten = KeyValuePair<memberName, dependencyMemberName> (one-to-one)
      var flatten = mapping.SelectMany(kvp => kvp.Value.Select(v => new System.Collections.Generic.KeyValuePair<string, string>(kvp.Key, v)));

      // reverse = Dictionary<dependencyMemberName, memberNames[]> (one-to-many)
      var reverse = flatten.Select(kvp => kvp.Value).Distinct().ToDictionary(dmn => dmn, dmn => flatten.Where(kvp => kvp.Value == dmn).Select(kvp => kvp.Key).Distinct().ToArray());

      return reverse;

      #region Old version, works!
      // Map explicit method names to the names of their dependents.
      //var map = source.ToDictionary(mi => mi.Name, mi => mi.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>().Select(a => a.DependencyMemberName));

      // Flatten all keys and values.
      //var flat = from key in map.Keys from value in map[key] select new { Key = key, Value = value };

      // Re-map distinct values as keys and distinct keys as values.
      //return flat.Select(f => f.Value).Distinct().ToDictionary(d => d, d => flat.Where(f => f.Value == d).Select(f => f.Key).Distinct().ToArray());
      #endregion Old version, works!
    }
  }
}
