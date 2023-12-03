namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToFalse(this System.Collections.BitArray source)
      => source.Cast<bool>().SelectWhere((e, i) => !e, (e, i) => i);
    public static System.Collections.Generic.IEnumerable<int> GetIndicesEqualToTrue(this System.Collections.BitArray source)
      => source.Cast<bool>().SelectWhere((e, i) => e, (e, i) => i);
  }
}
