namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Snaps the <paramref name="value"/> to the nearest <paramref name="interval"/> if it's within the specified <paramref name="proximity"/> of an <paramref name="interval"/> position, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has notches which latches the knob at certain positions.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="interval">The number will snap to any multiple of the specified <paramref name="interval"/>.</param>
    /// <param name="proximity">This is the absolute tolerance of proximity, on either side of an <paramref name="interval"/>.</param>
    /// <returns></returns>
    public static TNumber DetentInterval<TNumber>(this TNumber value, TNumber interval, TNumber proximity)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzInterval && TNumber.Abs(tzInterval - value) <= proximity
      ? tzInterval
      : tzInterval + interval is var afzInterval && TNumber.Abs(afzInterval - value) <= proximity
      ? afzInterval
      : value;

    /// <summary>
    /// <para>Snaps a <paramref name="value"/> to a <paramref name="position"/> if it's within the specified <paramref name="proximity"/> of the <paramref name="position"/>, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has a notch which latches the knob at a certain position.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="position">E.g. a 0 snaps the <paramref name="value"/> to zero within the <paramref name="proximity"/>.</param>
    /// <param name="proximity">This is the absolute tolerance of proximity, on either side of the <paramref name="position"/>.</param>
    /// <returns></returns>
    public static TNumber DetentPosition<TNumber>(this TNumber value, TNumber position, TNumber proximity)
      where TNumber : System.Numerics.INumber<TNumber>
      => position.EqualsWithinAbsoluteTolerance(value, proximity)
      ? position // Detent to the position.
      : value;
  }
}
