namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is considered plural in terms of writing.</para>
    /// <para></para>
    /// </summary>
    /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.) except the <c>integer</c> 1 to be plural.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsConsideredPlural<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => number != TNumber.One || !number.IsBinaryInteger(); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.
  }
}
