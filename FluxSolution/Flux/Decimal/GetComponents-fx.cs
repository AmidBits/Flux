namespace Flux
{
  public static partial class Decimals
  {
    /// <summary>
    /// <para>Gets the components that make up a decimal type, i.e. mantissa, scale and sign.</para>
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static (System.Numerics.BigInteger Mantissa, byte Scale, bool Sign) GetComponents(this decimal number)
    {
      var bits = decimal.GetBits(number);

      var bytes = System.Runtime.InteropServices.MemoryMarshal.Cast<int, byte>(bits);

      var mantissa = new System.Numerics.BigInteger(bytes[..12]);
      var scale = bytes[14];
      var sign = bytes[15] != 0;

      return (mantissa, scale, sign);
    }
  }
}
