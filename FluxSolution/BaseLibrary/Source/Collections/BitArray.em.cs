using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsBitArray
  {
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToFalse(this System.Collections.BitArray source)
      => source.Cast<bool>().SelectWhere((e, i) => !e, (e, i) => i);
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToTrue(this System.Collections.BitArray source)
      => source.Cast<bool>().SelectWhere((e, i) => e, (e, i) => i);
  }
}
