namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Appends any object as a string to a <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> Append(this SpanMaker<System.Text.Rune> source, object value)
      => source.Append(1, (value?.ToString() ?? string.Empty).ToSpanMakerOfRune());

    public static SpanMaker<System.Text.Rune> Append(this SpanMaker<System.Text.Rune> source, params object[] values)
      => source.Append(1, string.Concat(values.SelectMany(v => v.ToString() ?? string.Empty)).ToSpanMakerOfRune());

    /// <summary>
    /// <para>Concatenates and appends the members of a collection, using the specified separator between each member.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="separator"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendJoin<T>(this SpanMaker<System.Text.Rune> source, System.Text.Rune separator, System.Collections.Generic.IEnumerable<T> collection)
      where T : notnull
    {
      var count = 0;

      foreach (var item in collection)
      {
        if (count++ > 0)
          source = source.Append(separator);

        source = source.Append((item?.ToString() ?? string.Empty).ToSpanMakerOfRune());
      }

      return source;
    }

    /// <summary>
    /// <para>Appends a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source)
      => source.Append(System.Environment.NewLine.ToSpanMakerOfRune());

#if XNET9_0_OR_GREATER

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, int count, params System.ReadOnlySpan<System.Text.Rune> value)
    {
      source = source.Append(count, value);
      source = source.AppendLine();
      return source;
    }

    /// <summary>
    /// <para>Appends <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, params System.ReadOnlySpan<System.Text.Rune> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#else

    /// <summary>
    /// <para>Appends <paramref name="count"/> <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, int count, System.ReadOnlySpan<System.Text.Rune> value)
    {
      source = source.Append(count, value);
      source = source.AppendLine();
      return source;
    }

    /// <summary>
    /// <para>Appends <paramref name="value"/> and a <see cref="System.Environment.NewLine"/> to the <see cref="SpanMaker{T}"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SpanMaker<System.Text.Rune> AppendLine(this SpanMaker<System.Text.Rune> source, System.ReadOnlySpan<System.Text.Rune> value)
    {
      source = source.Append(value);
      source = source.AppendLine();
      return source;
    }

#endif

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static SpanMaker<System.Text.Rune> PrefixCapWords(this SpanMaker<System.Text.Rune> source, System.Text.Rune prefix)
    {
      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        if (index == 0 || !System.Text.Rune.IsUpper(source[index])) continue; // If, on first or c is not upper-case, then advance.

        if (!System.Text.Rune.IsLower(source[index - 1]) && (index < maxIndex) && !System.Text.Rune.IsLower(source[index + 1])) continue; // If, (above ensured previous) previous is not lower-case and (ensure next) next is not lower-case, then advance.

        source = source.Insert(index, 1, prefix);
      }

      return source;
    }

    /// <summary>Join CamelCase of words separated by the specified predicate. The first character</summary>
    public static SpanMaker<System.Text.Rune> UnprefixCapWords(this SpanMaker<System.Text.Rune> source, System.Text.Rune prefix)
    {
      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index > 0; index--)
      {
        if (source[index] != prefix) continue; // If, c is not prefix, then advance.

        if ((index < maxIndex) && !System.Text.Rune.IsUpper(source[index + 1])) continue; // If, (ensure next) next is not upper-case, then advance.

        source = source.Remove(index, 1);
      }

      return source;
    }
  }
}
