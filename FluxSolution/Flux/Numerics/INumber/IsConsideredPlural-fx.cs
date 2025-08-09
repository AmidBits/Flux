namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="value"/> is considered plural in terms of writing.</para>
    /// <para></para>
    /// </summary>
    /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.) except the <c>integer</c> 1 to be plural.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static bool IsConsideredPlural<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
      => value != TNumber.One || !value.GetType().IsIntegerNumericType(); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.
  }
}
