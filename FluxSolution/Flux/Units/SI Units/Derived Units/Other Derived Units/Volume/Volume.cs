namespace Flux.Units
{
  /// <summary>
  /// <para>Volume, unit of cubic meter. This is an SI derived quantity.</para>
  /// <see href="https://en.wikipedia.org/wiki/Volume"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="Length"/>, <see cref="Area"/> and <see cref="Volume"/>.</remarks>
  public readonly record struct Volume
    : System.IComparable, System.IComparable<Volume>, System.IFormattable, ISiUnitValueQuantifiable<double, VolumeUnit>
  {
    private readonly double m_value;

    public Volume(double value, VolumeUnit unit = VolumeUnit.CubicMeter) => m_value = ConvertFromUnit(unit, value);

    public Volume(MetricPrefix prefix, double cubicMeter) => m_value = prefix.ConvertPrefix(cubicMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #region Volume of geometric shapes

    /// <summary>
    /// <para>Computes the volume of the specified cone using <paramref name="radius"/> and <paramref name="height"/>.</para>
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static double OfCone(double radius, double height)
      => 1d / 3d * double.Pi * radius * radius * height;

    /// <summary>
    /// <para>Computes the volume of the specified cube using <paramref name="sideLength"/>.</para>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double OfCube(double sideLength)
      => sideLength * sideLength * sideLength;

    /// <summary>
    /// <para>Computes the volume of the specified cuboid using <paramref name="a"/>, <paramref name="b"/> and <paramref name="c"/>.</para>
    /// </summary>
    /// <param name="length">The length of a cuboid.</param>
    /// <param name="width">The width of a cuboid.</param>
    /// <param name="height">The height of a cuboid.</param>
    public static double OfCuboid(double a, double b, double c)
      => a * b * c;

    /// <summary>
    /// <para>Computes the volume of the specified cylinder using <paramref name="radius"/> and <paramref name="height"/>.</para>
    /// </summary>
    /// <param name="radius">The radius of a cylinder.</param>
    /// <param name="height">The height of a cylinder.</param>
    public static double OfCylinder(double radius, double height)
      => double.Pi * radius * radius * height;

    /// <summary>
    /// <para>Computes the volume of the specified ellipsoid using <paramref name="a"/>, <paramref name="b"/> and <paramref name="c"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double OfEllipsoid(double a, double b, double c)
      => 4d / 3d * double.Pi * a * b * c;

    /// <summary>
    /// <para>Computes the volume of a square pyramid using <paramref name="sideLength"/> and <paramref name="height"/>.</para>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static double OfSquarePyramid(double sideLength, double height)
      => 1d / 3d * sideLength * sideLength * height;

    /// <summary>
    /// <para>Computes the volume of the specified sphere using <paramref name="radius"/>.</para>
    /// </summary>
    /// <param name="radius">The radius of a sphere.</param>
    public static double OfSphere(double radius)
      => 4d / 3d * double.Pi * radius * radius * radius;

    /// <summary>
    /// <para>Computes the volume of the specified tetrahedron (triangular pyramid) using <paramref name="sideLength"/>.</para>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double OfTetrahedron(double sideLength)
      => double.Sqrt(2) / 12 * sideLength * sideLength * sideLength;

    #endregion // Volume of geometric shapes

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Volume a, Volume b) => a.CompareTo(b) < 0;
    public static bool operator >(Volume a, Volume b) => a.CompareTo(b) > 0;
    public static bool operator <=(Volume a, Volume b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Volume a, Volume b) => a.CompareTo(b) >= 0;

    public static Volume operator -(Volume v) => new(-v.m_value);
    public static Volume operator *(Volume a, Volume b) => new(a.m_value * b.m_value);
    public static Volume operator /(Volume a, Volume b) => new(a.m_value / b.m_value);
    public static Volume operator %(Volume a, Volume b) => new(a.m_value % b.m_value);
    public static Volume operator +(Volume a, Volume b) => new(a.m_value + b.m_value);
    public static Volume operator -(Volume a, Volume b) => new(a.m_value - b.m_value);
    public static Volume operator *(Volume a, double b) => new(a.m_value * b);
    public static Volume operator /(Volume a, double b) => new(a.m_value / b);
    public static Volume operator %(Volume a, double b) => new(a.m_value % b);
    public static Volume operator +(Volume a, double b) => new(a.m_value + b);
    public static Volume operator -(Volume a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Volume o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Volume other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(VolumeUnit.CubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix, 3);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + VolumeUnit.CubicMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(VolumeUnit unit, double value)
      => unit switch
      {
        VolumeUnit.CubicMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(VolumeUnit unit, double value)
      => unit switch
      {
        VolumeUnit.CubicMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, VolumeUnit from, VolumeUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(VolumeUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(VolumeUnit unit = VolumeUnit.CubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Volume.Value"/> property is in <see cref="VolumeUnit.CubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
