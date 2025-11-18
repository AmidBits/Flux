namespace Flux
{
  public readonly record struct CartesianCoordinate<T>
    where T : System.Numerics.INumber<T>, Units.IValueQuantifiable<System.Numerics.Vector<T>>
  {
    private readonly System.Numerics.Vector<T> m_vector;

    public CartesianCoordinate(T x) => m_vector = new System.Numerics.Vector<T>(x);
    public CartesianCoordinate(T x, T y) => m_vector = new System.Numerics.Vector<T>([x, y]);
    public CartesianCoordinate(T x, T y, T z) => m_vector = new System.Numerics.Vector<T>([x, y, z]);
    public CartesianCoordinate(T x, T y, T z, T w) => m_vector = new System.Numerics.Vector<T>([x, y, z, w]);
    public CartesianCoordinate(System.Numerics.Vector<T> vector) => m_vector = vector;

    public readonly System.Numerics.Vector<T> Vector => m_vector;

    public readonly T X => m_vector[0];
    public readonly T Y => m_vector[1];
    public readonly T Z => m_vector[2];
    public readonly T W => m_vector[3];

    public void Deconstruct(out T x) { x = m_vector[0]; }
    public void Deconstruct(out T x, out T y) { x = m_vector[0]; y = m_vector[1]; }
    public void Deconstruct(out T x, out T y, out T z) { x = m_vector[0]; y = m_vector[1]; z = m_vector[2]; }
    public void Deconstruct(out T x, out T y, out T z, out T w) { x = m_vector[0]; y = m_vector[1]; z = m_vector[2]; w = m_vector[3]; }

    public readonly CoordinateSystems.CartesianCoordinate ToCartesianCoordinate() => new(double.CreateChecked(m_vector[0]), double.CreateChecked(m_vector[1]), double.CreateChecked(m_vector[2]));

    #region Static members

    /// <summary>
    /// <para>Converts cartesian 2D (<paramref name="x"/>, <paramref name="y"/>) coordinates to a linear index of a grid with the <paramref name="width"/> (the length of the x-axis).</para>
    /// </summary>
    public static TSelf ConvertCartesian2ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>
    /// <para>Converts cartesian 3D (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinates to a linear index of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</para>
    /// </summary>
    public static TSelf ConvertCartesian3ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>
    /// <para>Converts a <paramref name="linearIndex"/> of a grid with the <paramref name="width"/> (the length of the x-axis) to cartesian 2D (x, y) coordinates.</para>
    /// </summary>
    public static (TSelf x, TSelf y) ConvertLinearIndexToCartesian2<TSelf>(TSelf linearIndex, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
       => (
        linearIndex % width,
        linearIndex / width
      );

    /// <summary>
    /// <para>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</para>
    /// </summary>
    public static (TSelf x, TSelf y, TSelf z) ConvertLinearIndexToCartesian3<TSelf>(TSelf linearIndex, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var xy = width * height;
      var irxy = linearIndex % xy;

      return (
        irxy % width,
        irxy / width,
        linearIndex / xy
      );
    }

    #endregion // Static members
  }

  public partial class CartesianCoordinate
  {
    /// <summary>
    /// <para>Converts cartesian 2D (<paramref name="x"/>, <paramref name="y"/>) coordinates to a linear index of a grid with the <paramref name="width"/> (the length of the x-axis).</para>
    /// </summary>
    public static TSelf ConvertCartesian2ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width);

    /// <summary>
    /// <para>Converts cartesian 3D (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) coordinates to a linear index of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</para>
    /// </summary>
    public static TSelf ConvertCartesian3ToLinearIndex<TSelf>(TSelf x, TSelf y, TSelf z, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x + (y * width) + (z * width * height);

    /// <summary>
    /// <para>Converts a <paramref name="linearIndex"/> of a grid with the <paramref name="width"/> (the length of the x-axis) to cartesian 2D (x, y) coordinates.</para>
    /// </summary>
    public static (TSelf x, TSelf y) ConvertLinearIndexToCartesian2<TSelf>(TSelf linearIndex, TSelf width)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
       => (
        linearIndex % width,
        linearIndex / width
      );

    /// <summary>
    /// <para>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</para>
    /// </summary>
    public static (TSelf x, TSelf y, TSelf z) ConvertLinearIndexToCartesian3<TSelf>(TSelf linearIndex, TSelf width, TSelf height)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var xy = width * height;
      var irxy = linearIndex % xy;

      return (
        irxy % width,
        irxy / width,
        linearIndex / xy
      );
    }
  }

  //public static partial class Number
  //{
  //  /// <summary>
  //  /// <para>Snaps the <paramref name="value"/> to the nearest <paramref name="interval"/> if it's within the specified <paramref name="proximity"/> of an <paramref name="interval"/> position, otherwise unaltered.</para>
  //  /// </summary>
  //  /// <remarks>This is similar to a knob that has notches which latches the knob at certain positions.</remarks>
  //  /// <typeparam name="TNumber"></typeparam>
  //  /// <param name="value"></param>
  //  /// <param name="interval">The number will snap to any multiple of the specified <paramref name="interval"/>.</param>
  //  /// <param name="proximity">This is the absolute tolerance of proximity, on either side of an <paramref name="interval"/>.</param>
  //  /// <returns></returns>
  //  public static TNumber DetentInterval<TNumber>(this TNumber value, TNumber interval, TNumber proximity)
  //    where TNumber : System.Numerics.INumber<TNumber>
  //    => TNumber.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzInterval && TNumber.Abs(tzInterval - value) <= proximity
  //    ? tzInterval
  //    : tzInterval + interval is var afzInterval && TNumber.Abs(afzInterval - value) <= proximity
  //    ? afzInterval
  //    : value;

  //  /// <summary>
  //  /// <para>Snaps a <paramref name="value"/> to a <paramref name="position"/> if it's within the specified <paramref name="proximity"/> of the <paramref name="position"/>, otherwise unaltered.</para>
  //  /// </summary>
  //  /// <remarks>This is similar to a knob that has a notch which latches the knob at a certain position.</remarks>
  //  /// <typeparam name="TNumber"></typeparam>
  //  /// <param name="value"></param>
  //  /// <param name="position">E.g. a 0 snaps the <paramref name="value"/> to zero within the <paramref name="proximity"/>.</param>
  //  /// <param name="proximity">This is the absolute tolerance of proximity, on either side of the <paramref name="position"/>.</param>
  //  /// <returns></returns>
  //  public static TNumber DetentPosition<TNumber>(this TNumber value, TNumber position, TNumber proximity)
  //    where TNumber : System.Numerics.INumber<TNumber>
  //    => position.EqualsWithinAbsoluteTolerance(value, proximity)
  //    ? position // Detent to the position.
  //    : value;
  //}
}
