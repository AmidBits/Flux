namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public static bool IsConsideredPlural(TNumber value)
        => !(value == TNumber.One && typeof(TNumber).IsIBinaryInteger()); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

      #endregion

      #region NormalizeZero

      /// <summary>
      /// <para>Normalize any (including negative) zero to <see cref="TNumber.Zero"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TNumber NormalizeZero(TNumber value)
        => TNumber.IsZero(value) ? TNumber.Zero : value;

      #endregion
    }
  }
}
