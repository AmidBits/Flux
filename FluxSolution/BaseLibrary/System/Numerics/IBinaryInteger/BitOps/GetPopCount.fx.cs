namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.PopCount(TValue)"/>.</para>
    /// </summary>
    /// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
    public static int GetPopCount<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => int.CreateChecked(TValue.PopCount(value));
  }
}
