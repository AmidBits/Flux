#if NET7_0_OR_GREATER
namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    public static TSelf RoundDownToPowerOf2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : BitFoldRight(value - TSelf.One) + TSelf.One >> 1;

    /// <summary>PREVIEW! Computes the smaller and larger power of 2, as well as the nearest of the two power of 2 values computed.</summary>
    /// <param name="value"></param>
    /// <param name="proper">When true, the nearest power of 2 is truly greater or less than, when false, it's greater or less than OR EQUAL.</param>
    /// <param name="greaterThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
    /// <param name="lessThan">Depending on the bool <paramref name="proper"/>, outputs the greater than (and/or equal to) power of 2.</param>
    /// <returns>The nearest power of 2, and both greater than and less than powers of 2.</returns>
    public static TSelf RoundToNearestPowerOf2<TSelf>(this TSelf value, bool proper, out TSelf greaterThan, out TSelf lessThan)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (IsPowerOf2(value))
      {
        greaterThan = (proper ? value << 1 : value);
        lessThan = (proper ? value >> 1 : value);
      }
      else
      {
        greaterThan = BitFoldRight(value - TSelf.One) + TSelf.One;
        lessThan = greaterThan >> 1;
      }

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>PREVIEW! Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    public static TSelf RoundUpToPowerOf2<TSelf>(this TSelf value, bool proper)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : BitFoldRight(value - TSelf.One) + TSelf.One;
  }
}
#endif
