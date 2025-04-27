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

    public Volume(MetricPrefix prefix, double cubicMeter) => m_value = prefix.ConvertTo(cubicMeter, MetricPrefix.Unprefixed);

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

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => GetUnitName(VolumeUnit.CubicMeter, preferPlural).Insert(5, prefix.GetMetricPrefixName());

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(VolumeUnit.CubicMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix, 3);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(VolumeUnit unit, double value)
      => unit switch
      {
        VolumeUnit.CubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(VolumeUnit unit, double value)
      => unit switch
      {
        VolumeUnit.CubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, VolumeUnit from, VolumeUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(VolumeUnit unit)
      => unit switch
      {
        VolumeUnit.CubicMeter => 1,

        VolumeUnit.Microliter => 0.000000001,
        VolumeUnit.Milliliter => 0.000001,
        VolumeUnit.Centiliter => 0.00001,
        VolumeUnit.Deciliter => 0.0001,
        VolumeUnit.Liter => 0.001,
        VolumeUnit.UKGallon => 1 / 219.96924829909,
        VolumeUnit.UKQuart => 1 / 879.87699319635,
        VolumeUnit.USDryGallon => 1 / 227.02074456538,
        VolumeUnit.USLiquidGallon => 1 / 264.17205124156,
        VolumeUnit.USDryQuart => 0.00110122095,
        VolumeUnit.USLiquidQuart => 0.00094635295,
        VolumeUnit.CubicFoot => 1 / (1953125000.0 / 55306341.0),
        VolumeUnit.CubicYard => 1 / (1953125000.0 / 1493271207.0),
        VolumeUnit.CubicMile => (8140980127813632.0 / 1953125.0),
        VolumeUnit.CubicKilometer => 1e-09,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(VolumeUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(VolumeUnit unit, bool preferUnicode)
      => unit switch
      {
        VolumeUnit.CubicMeter => preferUnicode ? "\u33A5" : "m�",

        VolumeUnit.Microliter => preferUnicode ? "\u3395" : "탅",
        VolumeUnit.Milliliter => preferUnicode ? "\u3396" : "ml",
        VolumeUnit.Centiliter => "cl",
        VolumeUnit.Deciliter => preferUnicode ? "\u3397" : "dl",
        VolumeUnit.Liter => "l",
        VolumeUnit.UKGallon => preferUnicode ? "\u33FF" : "gal (UK)",
        VolumeUnit.UKQuart => "qt (UK)",
        VolumeUnit.USDryGallon => preferUnicode ? "\u33FF" : "gal (US-dry)",
        VolumeUnit.USLiquidGallon => preferUnicode ? "\u33FF" : "gal (US-liquid)",
        VolumeUnit.USDryQuart => "qt (US-dry)",
        VolumeUnit.USLiquidQuart => "qt (US-liquid)",
        VolumeUnit.CubicFoot => "ft�",
        VolumeUnit.CubicYard => "yd�",
        VolumeUnit.CubicMile => "mi�",
        VolumeUnit.CubicKilometer => preferUnicode ? "\u33A6" : "km�",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(VolumeUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(VolumeUnit unit = VolumeUnit.CubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
