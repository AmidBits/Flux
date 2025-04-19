namespace Flux
{
  public static partial class IEnumerables
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

      if (!e.MoveNext()) // If the sequence is empty..
        return value; // ..return the specified value.

      var first = e.Current; // Save the first item so that we can check for more.

      if (!e.MoveNext()) // If the sequence have no more elements..
        return first; // ..return the first and only item.

      throw new System.InvalidOperationException("The sequence has more than one element.");
    }
  }
}
