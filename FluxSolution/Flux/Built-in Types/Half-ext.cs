namespace Flux
{
  public static partial class HalfExtensions
  {
    extension(System.Half)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Half"/> without losing precision is <c>16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Half"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers up to 16,777,216 = <c>(1 &lt;&lt; 24)</c> = 2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static System.Half MaxExactInteger => System.Half.CreateChecked(+2048);

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Half"/> without losing precision is <c>-16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Half"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers down to -16,777,216 = <c>-(1 &lt;&lt; 24)</c> = -2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static System.Half MinExactInteger => System.Half.CreateChecked(-2048);

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a <see cref="System.Half"/>.</para>
      /// </summary>
      public static System.Half MaxExactPrimeNumber => System.Half.CreateChecked(2039);

      /// <summary>
      /// <para>A <see cref="System.Half"/> has a precision of about 6-9 significant digits.</para>
      /// </summary>
      public static int MaxExactSignificantDigits => 3;

      /// <summary>
      /// <para>The default epsilon scalar (1e-6f) used for near-integer functions.</para>
      /// </summary>
      public static System.Half DefaultBaseEpsilon => System.Half.CreateChecked(1e-3);

      #region Native..

      public static System.Half NativeDecrement(System.Half value)
        => System.Half.IsNaN(value) || System.Half.IsNegativeInfinity(value)
        ? throw new System.ArithmeticException(value.ToString())
        : System.Half.IsPositiveInfinity(value)
        ? System.Half.MaxValue
        : System.Half.BitDecrement(value);

      public static System.Half NativeIncrement(System.Half value)
        => System.Half.IsNaN(value) || System.Half.IsPositiveInfinity(value)
        ? throw new System.ArithmeticException(value.ToString())
        : System.Half.IsNegativeInfinity(value)
        ? System.Half.MinValue
        : System.Half.BitIncrement(value);

      #endregion
    }
  }
}
