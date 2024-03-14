namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Gets the infimum (the largest value that is less than <paramref name="value"/>).</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Infimum, for integer types equal (<paramref name="value"/> - 1) and for floating point types equal (<paramref name="value"/> - epsilon). Other types are not implemented at this time.</remarks>
    public static TSelf GetInfimum<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (value.IsBinaryInteger())
        return value - TSelf.One;
      else if (value is System.Double d)
        return TSelf.CreateChecked(System.Double.BitDecrement(d));
      else if (value is System.Half h)
        return TSelf.CreateChecked(System.Half.BitDecrement(h));
      else if (value is System.Single f)
        return TSelf.CreateChecked(System.Single.BitDecrement(f));
      else if (value is System.Runtime.InteropServices.NFloat nf)
        return TSelf.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(nf));
      else
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// <para>Gets the supremum (the smallest value that is greater than <paramref name="value"/>).</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>Supremum, for integer types equal (<paramref name="value"/> + 1) and for floating point types equal (<paramref name="value"/> + epsilon). Other types are not implemented at this time.</remarks>
    public static TSelf GetSupremum<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (value.IsBinaryInteger())
        return value + TSelf.One;
      else if (value is System.Double d)
        return TSelf.CreateChecked(System.Double.BitIncrement(d));
      else if (value is System.Half h)
        return TSelf.CreateChecked(System.Half.BitIncrement(h));
      else if (value is System.Single f)
        return TSelf.CreateChecked(System.Single.BitIncrement(f));
      else if (value is System.Runtime.InteropServices.NFloat nf)
        return TSelf.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(nf));
      else
        throw new System.NotImplementedException();
    }
  }
}
