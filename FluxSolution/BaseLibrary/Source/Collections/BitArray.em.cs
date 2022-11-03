namespace Flux
{
  public static partial class BitArrayEm
  {
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToFalse(this System.Collections.BitArray source)
      => System.Linq.Enumerable.Cast<bool>(source).GetElementsAndIndices((e, i) => !e).Select(e => e.index);
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToTrue(this System.Collections.BitArray source)
      => System.Linq.Enumerable.Cast<bool>(source).GetElementsAndIndices((e, i) => e).Select(e => e.index);
  }
}
