namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="value"/> is of a <paramref name="multiple"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value">The number for which the nearest <paramref name="multiple"/> will be found.</param>
    /// <param name="multiple">The multiple to which <paramref name="value"/> is measured.</param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsMultipleOf<TNumber>(this TNumber value, TNumber multiple)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsZero(value % multiple);

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest <paramref name="multiple"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="multiple"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber MultipleOfAwayFromZero<TNumber>(this TNumber value, TNumber multiple, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CopySign(multiple, value) is var msv && value - (value % multiple) is var motz && (motz != value || (unequal || !TNumber.IsInteger(value))) ? motz + msv : motz;

    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the closest <paramref name="multiple"/> (i.e. of <paramref name="multipleOfTowardsZero"/> and <paramref name="multipleOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
    /// </summary>
    /// <param name="value">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="unequal">Proper means nearest but not <paramref name="value"/> if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="multipleOfTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="multipleOfAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static TNumber MultipleOfNearest<TNumber>(this TNumber value, TNumber multiple, bool unequal, UniversalRounding mode, out TNumber multipleOfTowardsZero, out TNumber multipleOfAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      multipleOfTowardsZero = value.MultipleOfTowardZero(multiple, unequal);
      multipleOfAwayFromZero = value.MultipleOfAwayFromZero(multiple, unequal);

      return value.RoundToNearest(mode, multipleOfTowardsZero, multipleOfAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest <paramref name="multiple"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="multiple"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber MultipleOfTowardZero<TNumber>(this TNumber value, TNumber multiple, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      //=> MultipleOfTowardZero(value, multiple, unequal && TNumber.IsInteger(value)); // value - (value % multiple) is var motz && unequal && motz == value ? motz - TSelf.CopySign(multiple, value) : motz;
      => value - (value % multiple) is var motz && (unequal && TNumber.IsInteger(value)) && motz == value ? motz - TNumber.CopySign(multiple, value) : motz;
  }
}
