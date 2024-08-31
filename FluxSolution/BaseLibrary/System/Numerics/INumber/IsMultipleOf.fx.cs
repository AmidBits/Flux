namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is of a <paramref name="multiple"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number">The number for which the nearest <paramref name="multiple"/> will be found.</param>
    /// <param name="multiple">The multiple to which <paramref name="number"/> is measured.</param>
    /// <returns></returns>
    public static bool IsMultipleOf<TNumber>(this TNumber number, TNumber multiple)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsZero(number % multiple);

    ///// <summary>Find the <paramref name="multiple"/> closest to <paramref name="number"/> away-from-zero, optionally <paramref name="unequal"/> to <paramref name="number"/>. Negative resilient.</summary>
    ///// <param name="number">The number for which the nearest multiple away-from-zero will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="unequal">Unequal means nearest but not equal to <paramref name="number"/> if it's a multiple-of, i.e. the multiple-of will be properly "nearest" (but not the same), in other words LT/GT rather than LTE/GTE.</param>
    ///// <returns>The <paramref name="multiple"/> closest to <paramref name="number"/>.</returns>
    //public static TNumber MultipleOfAwayFromZero<TNumber>(this TNumber number, TNumber multiple, bool unequal)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  => TNumber.CopySign(multiple, number) is var msv && number - (number % multiple) is var motz && (motz != number || unequal) ? motz + msv : motz;

    ///// <summary>Find the <paramref name="multiple"/> closest to <paramref name="number"/> toward-zero, optionally <paramref name="unequal"/> to <paramref name="number"/>. Negative resilient.</summary>
    ///// <param name="number">The number for which the nearest multiple toward-zero will be found.</param>
    ///// <param name="multiple">The multiple to which the results will align.</param>
    ///// <param name="unequal">Unequal means nearest but not equal to <paramref name="number"/> if it's a multiple-of, i.e. the multiple-of will be properly "nearest" (but not the same), in other words LT/GT rather than LTE/GTE.</param>
    ///// <returns>The <paramref name="multiple"/> closest to <paramref name="number"/>.</returns>
    //public static TNumber MultipleOfTowardZero<TNumber>(this TNumber number, TNumber multiple, bool unequal)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  => number - (number % multiple) is var motz && unequal && motz == number ? motz - TNumber.CopySign(multiple, number) : motz;
  }
}
