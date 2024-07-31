namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.PopCount(TSelf)"/>.</para>
    /// </summary>
    /// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
    public static int GetPopCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.PopCount(value));
  }
}
