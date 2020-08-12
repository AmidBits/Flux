namespace Flux
{
  /// <summary>All expressions are unanchored (for now).</summary>
  public static partial class XtensionsText
  {
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1 // Dependency on [group].Name
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, string>> GetNamedGroups(this System.Text.RegularExpressions.Match source)
    {
      for (var index = 0; index < source.Groups.Count; index++)
      {
        var group = source.Groups[index];

        if (!group.Name.Equals(index.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal))
        {
          yield return new System.Collections.Generic.KeyValuePair<string, string>(group.Name, group.Value);
        }
      }
    }
#endif
  }
}
