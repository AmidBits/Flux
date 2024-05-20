namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Snaps the <paramref name="value"/> to the nearest <paramref name="interval"/> if it's within the specified <paramref name="proximity"/> of an <paramref name="interval"/> position, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has notches which latches the knob at certain positions.</remarks>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf proximity)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzInterval && TSelf.Abs(tzInterval - value) <= proximity
      ? tzInterval
      : tzInterval + interval is var afzInterval && TSelf.Abs(afzInterval - value) <= proximity
      ? afzInterval
      : value;

    /// <summary>
    /// <para>Snaps a <paramref name="value"/> to a <paramref name="position"/> if it's within the specified <paramref name="proximity"/> of the <paramref name="position"/>, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has a notch which latches the knob at a certain position.</remarks>
    public static TSelf DetentPosition<TSelf>(this TSelf value, TSelf position, TSelf proximity)
      where TSelf : System.Numerics.INumber<TSelf>
      => position.EqualsWithinAbsoluteTolerance(value, proximity)
      ? position // Detent to the position.
      : value;

    /// <summary>
    /// <para>Snaps the <paramref name="value"/> to zero if it's within the specified <paramref name="proximity"/> of zero, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has a notch which latches the knob at the zero position.</remarks>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf proximity)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Zero.EqualsWithinAbsoluteTolerance(value, proximity)
      ? TSelf.Zero // Detent to zero.
      : value;
  }
}
