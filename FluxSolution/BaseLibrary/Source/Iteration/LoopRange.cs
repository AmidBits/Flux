namespace Flux
{
  public struct IterateRange<TSelf>
    : IIterable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private System.Numerics.BigInteger m_stepIndex;

    private TSelf m_startAt;
    private TSelf m_stepSize;
    
    public IterateRange(TSelf startAt, TSelf stepSize)
    {
      if (TSelf.IsZero(stepSize)) throw new System.ArgumentOutOfRangeException(nameof(stepSize));

      m_stepIndex = System.Numerics.BigInteger.Zero;

      m_startAt = startAt;
      m_stepSize = TSelf.Abs(stepSize);
    }

    public System.Numerics.BigInteger StepIndex { get => m_stepIndex; set => m_stepIndex = value; }

    public static System.Collections.Generic.IEnumerable<TSelf> LoopRange(TSelf startAt, TSelf stepSize, System.Numerics.BigInteger count)
    {
      if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var iterator = new IterateRange<TSelf>(startAt, stepSize);

      for (var i = count - 1; i >= 0; i--)
        yield return TSelf.IsNegative(stepSize) ? iterator.StepBackward() : iterator.StepForward();
    }

    public TSelf StepBackward() => m_startAt + TSelf.CreateChecked(--m_stepIndex) * m_stepSize;
    public TSelf StepForward() => m_startAt + TSelf.CreateChecked(++m_stepIndex) * m_stepSize;
  }

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
