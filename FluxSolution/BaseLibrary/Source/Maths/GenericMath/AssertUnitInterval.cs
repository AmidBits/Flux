namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Asserts the number is a valid unit interval (throws an exception if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertUnitInterval<TSelf>(TSelf unitInterval, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => IsUnitInterval(unitInterval) ? unitInterval : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(unitInterval), "Must be a value greater-than-or-equal to 0 and less-than-or-equal to 1.");

    /// <summary>Returns whether the number is a valid unit interval, i.e. a value in the range [0, 1].</summary>
    public static bool IsUnitInterval<TSelf>(TSelf unitInterval)
      where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so any type can be validated used if needed.
      => unitInterval >= TSelf.Zero && unitInterval <= TSelf.One;

#else

    /// <summary>Asserts the number is a valid unit interval (throws an exception if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double AssertUnitInterval(this double unitInterval)
      => IsUnitInterval(unitInterval) ? unitInterval : throw new System.ArgumentOutOfRangeException(nameof(unitInterval));

    /// <summary>Returns whether the number is a valid unit interval, i.e. a value in the range [0, 1].</summary>
    public static bool IsUnitInterval(this double unitInterval)
      => unitInterval >= 0 && unitInterval <= 1;

#endif
  }
}
