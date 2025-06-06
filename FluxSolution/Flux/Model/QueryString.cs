namespace Flux.Net
{
  public static class QueryString
  {
    /// <summary>Change any specified keys in the query dictionary. If the value of a key is the same as the default value of that key, then omit the key altogether.</summary>
    public static System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> ChangeInDictionary(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source, System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<string>>> change, System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>>? defaults = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(change);

      var changed = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

      foreach (var kvp in source)
        if (kvp.Value.Count > 0 && (defaults is null || !defaults.ContainsKey(kvp.Key) || !kvp.Value.SequenceEqual(defaults[kvp.Key])))
          changed.Add(kvp.Key, kvp.Value);

      foreach (var kvp in change)
        if (kvp.Value.Count > 0 && (defaults is null || !defaults.ContainsKey(kvp.Key) || !kvp.Value.SequenceEqual(defaults[kvp.Key])))
        {
          if (changed.ContainsKey(kvp.Key)) changed[kvp.Key] = kvp.Value;
          else changed.Add(kvp.Key, kvp.Value);
        }
        else changed.Remove(kvp.Key);

      return changed;
    }

    /// <summary>Remove any specified keys in the query dictionary. If the value of a key is the same as the default value of that key, then omit the key altogether.</summary>
    public static System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> RemoveInDictionary(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source, System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<string>>> remove, System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>>? defaults = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(remove);

      var removed = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

      foreach (var kvp in source)
        if (kvp.Value.Count > 0 && (defaults is null || !defaults.ContainsKey(kvp.Key) || !kvp.Value.SequenceEqual(defaults[kvp.Key])))
          removed.Add(kvp.Key, kvp.Value);

      foreach (var kvp in remove)
        removed.Remove(kvp.Key);

      return removed;
    }

    /// <summary>Generate a query string from the 'query string dictionary'.</summary>
    public static string ToQueryString(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.Count > 0 ? $"{'?'}{string.Join(@"&", source.OrderBy(kvp => kvp.Key).Where(kvp => kvp.Value.Count > 0).SelectMany(kvp => kvp.Value.Select(v => $"{kvp.Key}={v}")))}" : string.Empty;
    }

    /// <summary>Generate a 'query string dictionary' from the specified query string.</summary>
    public static System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> Parse(this string source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return (source.Length > 0 && source[0] == '?' ? source[1..] : source).Split('&').Select(s => s.Split('=')).Where(a => a.Length == 2).GroupBy(a => a[0]).ToDictionary(g => System.Net.WebUtility.UrlDecode(g.Key), g => g.Select(a => System.Net.WebUtility.UrlDecode(a[1])).ToList());
    }

    /// <summary>Generate a 'simplified query string dictionary' from the 'query string dictionary'.</summary>
    public static System.Collections.Generic.IDictionary<string, string> ToSimplifiedDictionary(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source, string delimiter = "|")
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.ToDictionary(kvp => kvp.Key, kvp => string.Join(delimiter, kvp.Value));
    }
  }
}
