namespace Flux
{
  public static partial class Iteration
  {
    /// <summary>
    /// <para>Creates a sequence of <paramref name="count"/> numbers starting at <paramref name="source"/> with an iterative change of <paramref name="step"/> size (positive or negative).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source">The iteration starts with this value.</param>
    /// <param name="count">The process iterates this many times.</param>
    /// <param name="step">The value is increased this much each iteration.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>This version iterates <paramref name="count"/> times.</para>
    /// <para>If the result reaches the limits of <typeparamref name="TNumber"/>, the iterator simply wraps the value, i.e. MinValue wraps to MaxValue and vice versa.</para>
    /// </remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopRange<TNumber, TCount>(this TNumber source, TNumber step, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.INumber<TCount>
    {
      if (TNumber.IsZero(step)) throw new System.ArgumentOutOfRangeException(nameof(step));
      if (TCount.IsNegative(count)) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (var i = TCount.Zero; i < count; i++)
        yield return source + TNumber.CreateChecked(i) * step;
    }

  }
}