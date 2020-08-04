using System.Linq;

namespace Flux
{
  public static partial class XtensionsChar
  {
    /// <summary>Computes the Luhn check digit from a source which must not contain a check digit. The Luhn algorithm is a simple checksum formula used to validate a variety of identification numbers.</summary>
    /// <param name="source">Calculates the check digit from the source string. The source must not contain a check digit.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Luhn_algorithm"/>
    public static int LuhnChecksum(this System.Collections.Generic.IEnumerable<char> source)
      => 10 - source.Reverse().Select(c => c - '0').Select((c, i) => (i & 1) == 0 ? (c > 4 ? c * 2 - 9 : c * 2) : c).Sum() % 10;
    /// <summary>Verifies the source against its included check digit. The Luhn algorithm is a simple checksum formula used to validate a variety of identification numbers.</summary>
    /// <param name="source">Calculates the check digit and verifies the source against it. The source must contain the check digit.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Luhn_algorithm"/>
    public static bool LuhnVerify(this System.Collections.Generic.IEnumerable<char> source, out int checkDigit)
      => (checkDigit = source.SkipLast(1).LuhnChecksum()) == source.Last() - '0';
  }
}
