using System.Linq;

namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Create a string with each byte in the array as a binary number, separated by the specified string.</summary>
    public static string ToBinaryString(this byte[] source, string separator = @" ")
      => string.Join(separator, source.Select(b => System.Convert.ToString(b, 2).PadLeft(8, '0')));
    /// <summary>Create a string with each byte in the array as a decimal number, separated by the specified string.</summary>
    public static string ToDecimalString(this byte[] source, string separator = @" ")
      => string.Join(separator, source.Select(b => b.ToString(System.Globalization.CultureInfo.InvariantCulture)));
    /// <summary>Create a string with each byte in the array as a hexadecimal number, separated by the specified string.</summary>
    public static string ToHexString(this byte[] source, string separator = @" ")
      => string.Join(separator, source.Select(b => b.ToString(@"X2", System.Globalization.CultureInfo.InvariantCulture).PadLeft(2, '0')));
  }
}
