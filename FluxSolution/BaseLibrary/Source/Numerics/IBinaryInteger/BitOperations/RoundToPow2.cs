#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Find the next largest power of 2 less than or equal to <paramref name="value"/>.</summary>
    public static TSelf RoundDownToPow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value) ? value : (BitFoldRight(value - TSelf.One) + TSelf.One) >> 1;
    /// <summary>PREVIEW! Find the next largest power of 2 proper (i.e. always) less than <paramref name="value"/>.</summary>
    public static TSelf RoundDownToPow2Proper<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value) ? value >> 1 : BitFoldRight(value - TSelf.One) + TSelf.One >> 1;

    /// <summary>PREVIEW! Computes the smallest and largest power of 2 greater than or equal to and less than or equal to, respectively, and also returns the nearest (to <paramref name="value"/>) of the two.</summary>
    /// <param name="value"></param>
    /// <param name="greaterThanOrEqual">Outputs the power of 2 greater than or equal to <paramref name="value"/>.</param>
    /// <param name="lessThanOrEqual">Outputs the power of 2 less than or equal to <paramref name="value"/>.</param>
    /// <returns>The nearest power of 2.</returns>
    public static TSelf RoundToNearestPow2<TSelf>(this TSelf value, out TSelf greaterThanOrEqual, out TSelf lessThanOrEqual)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsPow2(value))
      {
        greaterThanOrEqual = value;
        lessThanOrEqual = value;
      }
      else
      {
        greaterThanOrEqual = BitFoldRight(value - TSelf.One) + TSelf.One;
        lessThanOrEqual = greaterThanOrEqual >> 1;
      }

      return (greaterThanOrEqual - value) > (value - lessThanOrEqual) ? lessThanOrEqual : greaterThanOrEqual;
    }
    /// <summary>PREVIEW! Computes the smallest and largest power of 2 greater than and larger than, respectively, and also returns the nearest (to <paramref name="value"/>) of the two.</summary>
    /// <param name="value"></param>
    /// <param name="greaterThan">Outputs the power of 2 proper (i.e. always) greater than <paramref name="value"/>.</param>
    /// <param name="lessThan">Outputs the power of 2 proper (i.e. always) less than <paramref name="value"/>.</param>
    /// <returns>The nearest power of 2.</returns>
    public static TSelf RoundToNearestPow2Proper<TSelf>(this TSelf value, out TSelf greaterThan, out TSelf lessThan)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsPow2(value))
      {
        greaterThan = value << 1;
        lessThan = value >> 1;
      }
      else
      {
        greaterThan = BitFoldRight(value - TSelf.One) + TSelf.One;
        lessThan = greaterThan >> 1;
      }

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>PREVIEW! Find the next smallest power of 2 greater than or equal to <paramref name="value"/>.</summary>
    public static TSelf RoundUpToPow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value) ? value : BitFoldRight(value - TSelf.One) + TSelf.One;
    /// <summary>PREVIEW! Find the next smallest power of 2 proper (i.e. always) greater than <paramref name="value"/>.</summary>
    public static TSelf RoundUpToPow2Proper<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value) ? value << 1 : BitFoldRight(value - TSelf.One) + TSelf.One;
  }
}
#endif
