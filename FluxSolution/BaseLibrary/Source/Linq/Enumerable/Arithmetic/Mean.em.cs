using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Compute the <paramref name="mean"/> of all elements in <paramref name="source"/>, also return the <paramref name="count"/> and the <paramref name="sum"/> of elements as output parameters.</para>
    /// <see href="http://en.wikipedia.org/wiki/Mean"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Mean<TValue, TResult>(this System.Collections.Generic.IEnumerable<TValue> source, out TResult mean, out int count, out TValue sum)
      where TValue : System.Numerics.INumber<TValue>
      where TResult : System.Numerics.IFloatingPoint<TResult>
    {
      count = 0;
      sum = TValue.Zero;

      foreach (var item in source.ThrowIfNull())
      {
        count++;
        sum += item;
      }

      mean = count > 0 ? TResult.CreateChecked(sum) / TResult.CreateChecked(count) : TResult.Zero;
    }

#endif
  }
}
