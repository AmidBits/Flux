namespace Flux
{
  public static partial class Em
  {
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
  }
}
