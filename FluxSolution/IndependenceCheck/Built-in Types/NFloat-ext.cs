namespace Flux
{
  public static partial class NFloat
  {
    extension(System.Runtime.InteropServices.NFloat)
    {
      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Runtime.InteropServices.NFloat"/> without losing precision.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat MaxExactInteger
        => System.Runtime.InteropServices.NFloat.Size == 8 ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MaxExactInteger)
        : System.Runtime.InteropServices.NFloat.Size == 4 ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MaxExactInteger)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Runtime.InteropServices.NFloat"/> without losing precision.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat MinExactInteger
        => System.Runtime.InteropServices.NFloat.Size == 8 ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MinExactInteger)
        : System.Runtime.InteropServices.NFloat.Size == 4 ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MinExactInteger)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a <see cref="System.Runtime.InteropServices.NFloat"/>.</para>
      /// </summary>
      public static double MaxExactPrimeNumber
        => System.Runtime.InteropServices.NFloat.Size == 8 ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MaxExactPrimeNumber)
        : System.Runtime.InteropServices.NFloat.Size == 4 ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MaxExactPrimeNumber)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>A <see cref="System.Single"/> has a precision of about 6-9 significant digits.</para>
      /// </summary>
      public static int MaxExactSignificantDigits
        => System.Runtime.InteropServices.NFloat.Size == 8 ? double.MaxExactSignificantDigits
        : System.Runtime.InteropServices.NFloat.Size == 4 ? float.MaxExactSignificantDigits
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The default epsilon scalar (1e-6f) used for near-integer functions.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat DefaultBaseEpsilon
        => System.Runtime.InteropServices.NFloat.Size == 8 ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DefaultBaseEpsilon)
        : System.Runtime.InteropServices.NFloat.Size == 4 ? System.Runtime.InteropServices.NFloat.CreateChecked(float.DefaultBaseEpsilon)
        : throw new System.NotImplementedException();

      #region Native..

      public static System.Runtime.InteropServices.NFloat NativeDecrement(System.Runtime.InteropServices.NFloat value)
        => System.Runtime.InteropServices.NFloat.IsNaN(value) || System.Runtime.InteropServices.NFloat.IsNegativeInfinity(value)
        ? throw new System.ArithmeticException(value.ToString())
        : System.Runtime.InteropServices.NFloat.IsPositiveInfinity(value)
        ? System.Runtime.InteropServices.NFloat.MaxValue
        : System.Runtime.InteropServices.NFloat.BitDecrement(value);

      public static System.Runtime.InteropServices.NFloat NativeIncrement(System.Runtime.InteropServices.NFloat value)
        => System.Runtime.InteropServices.NFloat.IsNaN(value) || System.Runtime.InteropServices.NFloat.IsPositiveInfinity(value)
        ? throw new System.ArithmeticException(value.ToString())
        : System.Runtime.InteropServices.NFloat.IsNegativeInfinity(value)
        ? System.Runtime.InteropServices.NFloat.MinValue
        : System.Runtime.InteropServices.NFloat.BitIncrement(value);

      #endregion
    }
  }
}
