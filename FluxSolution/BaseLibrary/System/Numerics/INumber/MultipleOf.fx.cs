namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="value"/> is of a <paramref name="multiple"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value">The value for which the nearest <paramref name="multiple"/> will be found.</param>
    /// <param name="multiple">The multiple to which <paramref name="value"/> is measured.</param>
    /// <returns></returns>
    public static bool IsMultipleOf<TSelf>(this TSelf value, TSelf multiple)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsZero(value % multiple);

    /// <summary>Find the <paramref name="multiple"/> closest to <paramref name="value"/> away-from-zero, optionally <paramref name="unequal"/> to <paramref name="value"/>. Negative resilient.</summary>
    /// <param name="value">The value for which the nearest multiple away-from-zero will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="unequal">Unequal means nearest but not equal to <paramref name="value"/> if it's a multiple-of, i.e. the multiple-of will be properly "nearest" (but not the same), in other words LT/GT rather than LTE/GTE.</param>
    /// <returns>The <paramref name="multiple"/> closest to <paramref name="value"/>.</returns>
    public static TSelf MultipleOfAwayFromZero<TSelf>(this TSelf value, TSelf multiple, bool unequal)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.CopySign(multiple, value) is var msv && value - (value % multiple) is var motz && (motz != value || unequal) ? motz + msv : motz;

    /// <summary>Find the <paramref name="multiple"/> closest to <paramref name="value"/> toward-zero, optionally <paramref name="unequal"/> to <paramref name="value"/>. Negative resilient.</summary>
    /// <param name="value">The value for which the nearest multiple toward-zero will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="unequal">Unequal means nearest but not equal to <paramref name="value"/> if it's a multiple-of, i.e. the multiple-of will be properly "nearest" (but not the same), in other words LT/GT rather than LTE/GTE.</param>
    /// <returns>The <paramref name="multiple"/> closest to <paramref name="value"/>.</returns>
    public static TSelf MultipleOfTowardZero<TSelf>(this TSelf value, TSelf multiple, bool unequal)
      where TSelf : System.Numerics.INumber<TSelf>
      => value - (value % multiple) is var motz && unequal && motz == value ? motz - TSelf.CopySign(multiple, value) : motz;
  }
}
