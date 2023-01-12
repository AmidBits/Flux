namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Enables concatenation of <typeparamref name="T"/> data from the <paramref name="source"/> sequence into a <see cref="System.Text.StringBuilder"/> using the specified <paramref name="builder"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<System.Text.StringBuilder, T, int, System.Text.StringBuilder> builder)
    {
      if (builder is null) throw new System.ArgumentNullException(nameof(builder));

      var sb = new System.Text.StringBuilder();

      var index = 0;

      foreach (var item in source)
        builder(sb, item, index++);

      return sb.ToString();
    }

    /// <summary>Concatenates strings obtained by a <paramref name="stringSelector"/> from each element in the <paramref name="source"/> sequence, delimited by <paramref name="delimiter"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<T, int, string> stringSelector)
      => ToDelimitedString(source, (sb, e, i) => sb.Append(stringSelector(e, i) is var s && i > 0 ? delimiter + s : s));

    /// <summary>Concatenates <typeparamref name="T"/> data from the <paramref name="source"/> sequence using the specified <paramref name="delimiter"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter)
      => ToDelimitedString(source, delimiter, (e, i) => e?.ToString() ?? string.Empty);
  }
}
