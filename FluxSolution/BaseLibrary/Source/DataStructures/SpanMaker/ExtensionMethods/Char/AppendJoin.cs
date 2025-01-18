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
    public static SpanMaker<char> AppendJoin<T>(this SpanMaker<char> source, string separator, System.Collections.Generic.IEnumerable<T> collection)
      => source.Append(string.Join(separator, collection));
  }
}
