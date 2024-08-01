namespace Flux
{
  public static partial class Fx
  {
    /// <summary>All expressions are unanchored (for now).</summary>
    public static System.Collections.Generic.IDictionary<string, string> GetNamedGroups(this System.Text.RegularExpressions.Match source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var dictionary = new System.Collections.Generic.Dictionary<string, string>();

      for (var index = 0; index < source.Groups.Count; index++)
      {
        var group = source.Groups[index];

        if (!group.Name.Equals(index.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.InvariantCulture))
          dictionary.Add(group.Name, group.Value);
      }

      return dictionary;
    }
  }
}
