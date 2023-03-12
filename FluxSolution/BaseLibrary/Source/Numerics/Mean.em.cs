using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>
    /// <para>Calculate the mean of a sequence, also return the count and the sum of values in the sequence as output parameters.</para>
    /// <see href="http://en.wikipedia.org/wiki/Mean"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static void Mean<TValue, TResult>(this System.Collections.Generic.IEnumerable<TValue> source, out int count, out TValue sum, out TResult mean)
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
  }
}
