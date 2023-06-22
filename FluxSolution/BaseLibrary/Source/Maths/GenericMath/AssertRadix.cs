namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertRadix<TSelf>(TSelf radix, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), "Must be an integer, greater than or equal to 2.");

    /// <summary>Asserts the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertRadix<TSelf>(TSelf radix, TSelf upperLimit, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsRadix(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be an integer, greater than or equal to 2 and less than or equal to {upperLimit}.");

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
    public static bool IsRadix<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
      => radix > TSelf.One && TSelf.IsInteger(radix);

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/>.</summary>
    public static bool IsRadix<TSelf>(TSelf radix, TSelf upperLimit)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
      => radix > TSelf.One && radix <= upperLimit && TSelf.IsInteger(radix);

#else

    /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Numerics.BigInteger AssertRadix(this System.Numerics.BigInteger radix)
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int AssertRadix(this int radix)
      => IsRadix(radix) ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static int AssertRadix(this int radix, int upperLimit)
      => IsRadix(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
    public static bool IsRadix(this System.Numerics.BigInteger radix)
      => radix > 1;

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
    public static bool IsRadix(this int radix)
      => radix > 1;

    /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
    public static bool IsRadix(this int radix, int upperLimit)
      => radix > 1 && radix <= upperLimit;

#endif
  }
}
