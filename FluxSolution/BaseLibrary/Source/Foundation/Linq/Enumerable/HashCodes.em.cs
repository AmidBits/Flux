using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Combines the hash codes in the sequence. This is the .NET Core version.</summary>
    public static int CombineHashCodes(this System.Collections.Generic.IEnumerable<int?> source)
      => ThrowOnNull(source).Aggregate(new System.HashCode(), (hc, i) => { hc.Add(i); return hc; }, hc => hc.ToHashCode());

    /// <summary>Combines the hash codes in the sequence. This is the 'boost' version from C++.</summary>
    public static int CombineHashCodesBoost(this System.Collections.Generic.IEnumerable<int?> source, int seed = 0)
      => ThrowOnNull(source).Aggregate(seed, (hc, i) => unchecked(hc ^ (i ?? 0 + (int)0x9e3779b9 + (hc << 6) + (hc >> 2))), hc => hc);
    /// <summary>Combines the hash codes in the sequence, using a custom simplicity by Special Sauce (see link).</summary>
    /// <see cref="https://stackoverflow.com/a/34006336/3178666"/>
    public static int CombineHashCodesCustom(this System.Collections.Generic.IEnumerable<int?> source, int seed = 1009, int factor = 9176)
      => ThrowOnNull(source).Aggregate(seed, (hc, i) => unchecked(hc * factor + i ?? 0), (hc) => hc);

    public static System.Collections.Generic.IEnumerable<int?> GetHashCodes<T>(this System.Collections.Generic.IEnumerable<T> source)
      => ThrowOnNull(source).Select(t => t?.GetHashCode());
  }
}
