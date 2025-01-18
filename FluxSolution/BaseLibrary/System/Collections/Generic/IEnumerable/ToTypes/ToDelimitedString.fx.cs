namespace Flux
{
  public static partial class Fx
  {
#if NET9_0_OR_GREATER

    /// <summary>Enables concatenation of <typeparamref name="T"/> data from the <paramref name="source"/> sequence into a <see cref="SpanMaker{T}"/> using the specified <paramref name="builder"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<SpanMaker<char>, T, int, SpanMaker<char>> builder)
    {
      System.ArgumentNullException.ThrowIfNull(builder);

      var sm = new SpanMaker<char>();

      var index = 0;

      foreach (var item in source)
        builder(sm, item, index++);

      return sm.ToString();
    }

    /// <summary>Concatenates strings obtained by a <paramref name="stringSelector"/> from each element in the <paramref name="source"/> sequence, delimited by <paramref name="delimiter"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<T, int, string> stringSelector)
      => ToDelimitedString(source, (sm, e, i) => { sm = sm.Append(stringSelector(e, i) is var s && i > 0 ? delimiter + s : s); return sm; });

#else

    /// <summary>Enables concatenation of <typeparamref name="T"/> data from the <paramref name="source"/> sequence into a <see cref="SpanMaker{T}"/> using the specified <paramref name="builder"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<System.Text.StringBuilder, T, int, System.Text.StringBuilder> builder)
    {
      System.ArgumentNullException.ThrowIfNull(builder);

      var sb = new System.Text.StringBuilder();

      var index = 0;

      foreach (var item in source)
        builder(sb, item, index++);

      return sb.ToString();
    }

    /// <summary>Concatenates strings obtained by a <paramref name="stringSelector"/> from each element in the <paramref name="source"/> sequence, delimited by <paramref name="delimiter"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter, System.Func<T, int, string> stringSelector)
      => ToDelimitedString(source, (sm, e, i) => sm.Append(stringSelector(e, i) is var s && i > 0 ? delimiter + s : s));

#endif

    /// <summary>Concatenates <typeparamref name="T"/> data from the <paramref name="source"/> sequence using the specified <paramref name="delimiter"/>.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, string delimiter)
      => ToDelimitedString(source, delimiter, (e, i) => e?.ToString() ?? string.Empty);
  }
}
