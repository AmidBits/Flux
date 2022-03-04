namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToFalse(this System.Collections.BitArray source)
      => System.Linq.Enumerable.Select(System.Linq.Enumerable.Cast<bool>(source).GetIndicesInt32(b => !b), l => (int)l);
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToTrue(this System.Collections.BitArray source)
      => System.Linq.Enumerable.Select(System.Linq.Enumerable.Cast<bool>(source).GetIndicesInt32(b => b), l => (int)l);
  }
}
