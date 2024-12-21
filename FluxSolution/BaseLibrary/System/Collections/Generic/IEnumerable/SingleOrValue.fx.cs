namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T SingleOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
    {
      using var e = source.GetEnumerator();

      return e.MoveNext() && e.Current is var single && !e.MoveNext()
        ? single
        : value;
    }
  }
}
