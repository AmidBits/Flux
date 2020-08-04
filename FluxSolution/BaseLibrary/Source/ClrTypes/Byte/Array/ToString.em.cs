using System.Linq;

namespace Flux
{
  public static partial class XtensionsByte
  {
    public static string ToBinaryString(this byte[] source, string separator = @" ") => string.Join(separator, source.Select(b => System.Convert.ToString(b, 2).PadLeft(8, '0')));
    public static string ToDecimalString(this byte[] source, string separator = @" ") => string.Join(separator, source.Select(b => b.ToString()));
    public static string ToHexString(this byte[] source, string separator = @" ") => string.Join(separator, source.Select(b => b.ToString(@"X2").PadLeft(2, '0')));
  }
}
