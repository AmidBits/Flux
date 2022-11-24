namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Reverses the bytes of an integer.</summary>
    public static TSelf ReverseBytes<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var byteCount = value.GetByteCount();

      if (int.IsOddInteger(byteCount))
        throw new System.NotSupportedException();

      var bytes = new byte[byteCount];
      value.WriteLittleEndian(bytes);

      for (int lo = 0, hi = byteCount - 1; hi >= 0; lo += 2, hi -= 2)
      {
        var tmplo = bytes[lo];
        var tmphi = bytes[hi];

        bytes[lo] = tmphi;
        bytes[hi] = tmplo;
      }

      return TSelf.ReadLittleEndian(bytes, false);
    }
  }
}
