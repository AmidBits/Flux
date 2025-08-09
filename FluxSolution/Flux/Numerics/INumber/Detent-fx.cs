namespace Flux
{
  public readonly record struct CartesianCoordinate<TNumber>
    where TNumber : System.Numerics.INumber<TNumber>
  {
    public readonly System.Numerics.Vector<TNumber> m_vector;

    public CartesianCoordinate(TNumber x) => m_vector = new System.Numerics.Vector<TNumber>(x);
    public CartesianCoordinate(TNumber x, TNumber y) => m_vector = VectorT.Create(x, y);
    public CartesianCoordinate(TNumber x, TNumber y, TNumber z) => m_vector = VectorT.Create(x, y, z);
    public CartesianCoordinate(TNumber x, TNumber y, TNumber z, TNumber w) => m_vector = VectorT.Create(x, y, z, w);

    public readonly System.Numerics.Vector<TNumber> Vector => m_vector;

    public readonly TNumber X => m_vector[0];
    public readonly TNumber Y => m_vector[1];
    public readonly TNumber Z => m_vector[2];
    public readonly TNumber W => m_vector[3];

    public void Deconstruct(out TNumber x) { x = m_vector[0]; }
    public void Deconstruct(out TNumber x, out TNumber y) { x = m_vector[0]; y = m_vector[1]; }
    public void Deconstruct(out TNumber x, out TNumber y, out TNumber z) { x = m_vector[0]; y = m_vector[1]; z = m_vector[2]; }
    public void Deconstruct(out TNumber x, out TNumber y, out TNumber z, out TNumber w) { x = m_vector[0]; y = m_vector[1]; z = m_vector[2]; w = m_vector[3]; }

    public readonly CoordinateSystems.CartesianCoordinate ToCartesianCoordinate() => new(double.CreateChecked(m_vector[0]), double.CreateChecked(m_vector[1]), double.CreateChecked(m_vector[2]));
  }

  public static partial class Number
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
