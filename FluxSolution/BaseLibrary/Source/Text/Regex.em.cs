namespace Flux
{
  /// <summary>All expressions are unanchored (for now).</summary>
  public static partial class XtendRegex
  {
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, string>> GetNamedGroups(this System.Text.RegularExpressions.Match source)
    {
      if (source is null) throw new System.Exception(nameof(source));

      for (var index = 0; index < source.Groups.Count; index++)
      {
        var group = source.Groups[index];

        if (!group.Name.Equals(index.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.InvariantCulture))
        {
          yield return new System.Collections.Generic.KeyValuePair<string, string>(group.Name, group.Value);
        }
      }
    }
  }
}
