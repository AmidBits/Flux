using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Change any specified keys in the query dictionary. If the value of a key is the same as the default value of that key, then omit the key altogether.</summary>
    public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> ChangeInQueryStringDictionary(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source, System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<string>>> change, System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>>? defaults = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (change is null) throw new System.ArgumentNullException(nameof(change));

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
        else if (changed.ContainsKey(kvp.Key)) changed.Remove(kvp.Key);

      return changed;
    }

    /// <summary>Remove any specified keys in the query dictionary. If the value of a key is the same as the default value of that key, then omit the key altogether.</summary>
    public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> RemoveInQueryStringDictionary(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source, System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<string>>> remove, System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>>? defaults = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (remove is null) throw new System.ArgumentNullException(nameof(remove));

      var removed = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

      foreach (var kvp in source)
        if (kvp.Value.Count > 0 && (defaults is null || !defaults.ContainsKey(kvp.Key) || !kvp.Value.SequenceEqual(defaults[kvp.Key])))
          removed.Add(kvp.Key, kvp.Value);

      foreach (var kvp in remove)
        if (removed.ContainsKey(kvp.Key))
          removed.Remove(kvp.Key);

      return removed;
    }

    /// <summary>Generate a query string from the 'query string dictionary'.</summary>
    public static string ToQueryString(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return source.Count > 0 ? $"{'?'}{string.Join(@"&", source.OrderBy(kvp => kvp.Key).Where(kvp => kvp.Value.Count > 0).SelectMany(kvp => kvp.Value.Select(v => $"{kvp.Key}={v}")))}" : string.Empty;
    }

    /// <summary>Generate a 'query string dictionary' from the specified query string.</summary>
    public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> ToQueryStringDictionary(this string source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return (source.Length > 0 && source[0] == '?' ? source[1..] : source).Split('&').Select(s => s.Split('=')).Where(a => a.Length == 2).GroupBy(a => a[0]).ToDictionary(g => System.Net.WebUtility.UrlDecode(g.Key), g => g.Select(a => System.Net.WebUtility.UrlDecode(a[1])).ToList());
    }

    /// <summary>Generate a 'simplified query string dictionary' from the 'query string dictionary'.</summary>
    public static System.Collections.Generic.IDictionary<string, string> ToSimplifiedQueryDictionary(this System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      return source.ToDictionary(kvp => kvp.Key, kvp => string.Join('|', kvp.Value));
    }
  }
}
