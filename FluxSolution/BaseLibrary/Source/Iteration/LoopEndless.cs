namespace Flux
{
  public static partial class Iteration
  {
    /// <summary>
    /// <para>Creates an endless sequence starting at <paramref name="source"/> with an iterative change of <paramref name="step"/> size (positive or negative).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source">The iteration starts with this value.</param>
    /// <param name="step">The value is increased this much each iteration.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>This version runs indefinitely.</para>
    /// <para>If the result reaches the limits of <typeparamref name="TNumber"/>, the iterator simply wraps the value, i.e. MinValue wraps to MaxValue and vice versa.</para>
    /// </remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopEndless<TNumber>(this TNumber source, TNumber step)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IMinMaxValue<TNumber>
    {
      if (TNumber.IsZero(step)) throw new System.ArgumentOutOfRangeException(nameof(step));

      for (var index = System.Numerics.BigInteger.Zero; ; index++)
        yield return source + TNumber.CreateChecked(index) * step;
    }
  }
}
