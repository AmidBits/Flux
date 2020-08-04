using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Concatenates strings with a delimiter from the sequence based on the return from the valueSelector</summary>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<T, string> selector)
      => string.Join(delimiter, source.Select(t => selector(t)));

    /// <summary>Concatenates strings with a delimiter from the sequence.</summary>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter)
      => ToDelimitedString(source ?? throw new System.ArgumentNullException(nameof(source)), delimiter, (sb, e) => sb.Append(e));

    private static string ToDelimitedString<T>(System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<System.Text.StringBuilder, T, System.Text.StringBuilder> append)
    {
      var sb = new System.Text.StringBuilder();

      var index = 0;

      foreach (var value in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (index++ > 0) sb.Append(delimiter);

        append(sb, value);
      }

      return sb.ToString();
    }
  }
}
