namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Gets the infimum (the largest number that is less than <paramref name="value"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Infimum, for integer types equal (<paramref name="value"/> - 1) and for floating point types equal (<paramref name="value"/> - epsilon). Other types are not implemented at this time.</remarks>
    public static TNumber GetInfimum<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value.GetType().IsIntegerNumericType(false)) // All integers are the same, and therefor subtract one.
        return checked(value - TNumber.One);
      else if (value is double d) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(double.BitDecrement(d));
      else if (value is float f) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(float.BitDecrement(f));
      else if (value is System.Half h) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(System.Half.BitDecrement(h));
      else if (value is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-decrement.
        return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(nf));
      else
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// <para>Gets the supremum (the smallest number that is greater than <paramref name="value"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Supremum, for integer types equal (<paramref name="value"/> + 1) and for floating point types equal (<paramref name="value"/> + epsilon). Other types are not implemented at this time.</remarks>
    public static TNumber GetSupremum<TNumber>(this TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (value.GetType().IsIntegerNumericType(false)) // All integers are the same, and therefor add one.
        return checked(value + TNumber.One);
      else if (value is double d) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(double.BitIncrement(d));
      else if (value is float f) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(float.BitIncrement(f));
      else if (value is System.Half h) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(System.Half.BitIncrement(h));
      else if (value is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-increment.
        return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(nf));
      else
        throw new System.NotImplementedException();
    }
  }
}
