namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the <paramref name="mean"/> of all elements in <paramref name="source"/>, also return the <paramref name="count"/> and the <paramref name="sum"/> of elements as output parameters.</para>
    /// <para><see href="http://en.wikipedia.org/wiki/Mean"/></para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="mean"></param>
    /// <param name="sum"></param>
    /// <param name="count"></param>
    public static void Mean<TValue, TResult>(this System.Collections.Generic.IEnumerable<TValue> source, out TResult mean, out TValue sum, out int count)
      where TValue : System.Numerics.INumber<TValue>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      sum = TValue.Zero;
      count = 0;

      foreach (var item in source.ThrowOnNull())
      {
        sum += item;
        count++;
      }

      mean = count > 0 ? TResult.CreateChecked(sum) / TResult.CreateChecked(count) : TResult.Zero;
    }
  }
}
