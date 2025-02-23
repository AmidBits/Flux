namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Gets the infimum (the largest number that is less than <paramref name="number"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <remarks>Infimum, for integer types equal (<paramref name="number"/> - 1) and for floating point types equal (<paramref name="number"/> - epsilon). Other types are not implemented at this time.</remarks>
    public static TNumber GetInfimum<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (number.IsBinaryInteger()) // All integers are the same, and therefor subtract one.
        return number - TNumber.One;
      else if (number is double d) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(double.BitDecrement(d));
      else if (number is float f) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(float.BitDecrement(f));
      else if (number is System.Half h) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(System.Half.BitDecrement(h));
      else if (number is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(nf));
      else
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// <para>Gets the supremum (the smallest number that is greater than <paramref name="number"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <remarks>Supremum, for integer types equal (<paramref name="number"/> + 1) and for floating point types equal (<paramref name="number"/> + epsilon). Other types are not implemented at this time.</remarks>
    public static TNumber GetSupremum<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (number.IsBinaryInteger()) // All integers are the same, and therefor add one.
        return number + TNumber.One;
      else if (number is double d) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(double.BitIncrement(d));
      else if (number is float f) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(float.BitIncrement(f));
      else if (number is System.Half h) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(System.Half.BitIncrement(h));
      else if (number is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(nf));
      else
        throw new System.NotImplementedException();
    }
  }
}
