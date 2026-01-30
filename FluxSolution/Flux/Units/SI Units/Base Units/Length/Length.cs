namespace Flux.Units
{
  /// <summary>
  /// <para>Length. SI unit of meter. This is a base quantity.</para>
  /// <see href="https://en.wikipedia.org/wiki/Length"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="Length"/>, <see cref="Area"/> and <see cref="Volume"/>.</remarks>
  public readonly record struct Length
    : System.IComparable, System.IComparable<Length>, System.IEquatable<Length>, System.IFormattable, ISiUnitValueQuantifiable<double, LengthUnit>
  {
    public const double OneParsecInMeters = 30856775814913672;

    private readonly double m_value;

    public Length(double value, LengthUnit unit = LengthUnit.Meter) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (i.e. metric multiple) of <see cref="TimeUnit.Meter"/>, e.g. <see cref="MetricPrefix.Milli"/> and <paramref name="meter"/> for millimeter.</para>
    /// </summary>
    /// <param name="meter"></param>
    /// <param name="prefix"></param>
    public Length(MetricPrefix prefix, double meter) => m_value = prefix.ConvertPrefix(meter, MetricPrefix.Unprefixed);

    /// <summary>
    /// <para>Computes the wavelength from the specified phase velocity and frequency. A wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per second) for the wavelength distance.</para>
    /// </summary>
    /// <param name="phaseVelocity">The constant speed of the traveling wave. If these are sound waves then typically this is the speed of sound. If electromagnetic radiation (e.g. light) in free space then speed of light.</param>
    /// <param name="frequency"></param>
    /// <returns>The wavelength of the frequency cycle at the phase velocity.</returns>
    /// <see href="https://en.wikipedia.org/wiki/Wavelength"/>
    public Length(Speed phaseVelocity, Frequency frequency) : this(phaseVelocity.Value / frequency.Value) { }

    /// <summary>Creates a new <see cref="Length"/> instance from <see cref="Speed"/> and <see cref="AngularFrequency"/></summary>
    /// <param name="speed"></param>
    /// <param name="angularVelocity"></param>
    public Length(Speed speed, AngularFrequency angularVelocity) : this(speed.Value / angularVelocity.Value) { }

    #region Static methods

    //#region Conversion methods

    //public static double ConvertAstronomicalUnitToMeter(double astronomicalUnit) => astronomicalUnit * 149597870700;
    //public static double ConvertFathomToMeter(double fathom) => fathom * 1.8288;
    //public static double ConvertFootToMeter(double foot) => foot * 0.3048;
    //public static double ConvertInchToMeter(double inch) => inch * 0.0254;
    //public static double ConvertMeterToAstronomicalUnit(double meter) => meter / 149597870700;
    //public static double ConvertMeterToFathom(double fathom) => fathom / 1.8288;
    //public static double ConvertMeterToFoot(double meter) => meter / 0.3048;
    //public static double ConvertMeterToInch(double meter) => meter / 0.0254;
    //public static double ConvertMeterToMile(double meter) => meter / 1609.344;
    //public static double ConvertMeterToNauticalMile(double meter) => meter / 1852;
    //public static double ConvertMeterToParsec(double meter) => meter / 30856775814913672;
    //public static double ConvertMeterToYard(double meter) => meter / 0.9144;
    //public static double ConvertMeterToÅngström(double meter) => meter * 10000000000;
    //public static double ConvertMileToMeter(double mile) => mile * 1609.344;
    //public static double ConvertNauticalMileToMeter(double nauticalMile) => nauticalMile * 1852;
    //public static double ConvertParsecToMeter(double parsec) => parsec * 30856775814913672;
    //public static double ConvertYardToMeter(double yard) => yard * 0.9144;
    //public static double ConvertÅngströmToMeter(double ångström) => ångström * 10000000000;

    //#endregion // Conversion methods

    #region Perimeter of geometric shapes

    /// <summary>
    /// <para>Computes the perimeter (circumference) of a circle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    public static double OfCirclePerimeter(double radius)
      => 2 * double.Pi * radius;

    /// <summary>
    /// <para>Computes the approximate perimeter (circumference) of an ellipse with the two semi-axis or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter). Uses Ramanujans second approximation.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static double OfEllipsePerimeter(double a, double b)
    {
      var circle = double.Pi * (a + b); // (2 * PI * radius)

      if (a == b) // Short circuit for circles (PI * diameter);
        return circle;

      var h3 = 3 * (double.Pow(a - b, 2) / double.Pow(a + b, 2)); // H function (* 3 for H3 is an optimizaion for below).

      return circle * (1 + h3 / (10 + double.Sqrt(4 - h3)));
    }

    /// <summary>
    /// <para>Computes the perimeter of a hexagon with the specified <paramref name="sideLength"/> (which is the length of a side and also the circumradius).</para>
    /// </summary>
    /// <param name="length">Length of the side (and also the circumradius, i.e. half outer diameter).</param>
    public static double OfHexagonPerimeter(double sideLength)
      => sideLength * 6;

    /// <summary>
    /// <para>Computes the perimeter of a rectangle with the specified <paramref name="length"/> and <paramref name="width"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    /// <param name="length">The length of a rectangle.</param>
    /// <param name="width">The width of a rectangle.</param>
    public static double OfRectanglePerimeter(double length, double width)
      => 2 * length + 2 * width;

    /// <summary>
    /// <para>Computes the perimeter of a regular polygon with the specified <paramref name="circumradius"/> and <paramref name="numberOfSides"/>.</para>
    /// </summary>
    /// <param name="circumradius"></param>
    /// <param name="numberOfSides"></param>
    /// <returns></returns>
    public static double OfRegularPolygonPerimeter(double circumradius, int numberOfSides)
      => 2 * circumradius * numberOfSides * double.Sin(double.Pi / numberOfSides);

    /// <summary>
    /// <para>Computes the perimeter of a semicircle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    public static double OfSemicirclePerimeter(double radius)
      => (double.Pi + 2) * radius;

    /// <summary>
    /// <para>Computes the perimeter of a square with the specified <paramref name="sideLength"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    /// <param name="sideLength">The sidelength of a rectangle.</param>
    public static double OfSquarePerimeter(double sideLength)
      => 4 * sideLength;

    /// <summary>
    /// <para>Computes the perimeter of a triangle with the specified <paramref name="a"/>, <paramref name="b"/> and <paramref name="c"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static double OfTrianglePerimeter(double a, double b, double c)
      => a + b + c;

    #endregion // Perimeter of geometric shapes

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Length a, Length b) => a.CompareTo(b) < 0;
    public static bool operator >(Length a, Length b) => a.CompareTo(b) > 0;
    public static bool operator <=(Length a, Length b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Length a, Length b) => a.CompareTo(b) >= 0;

    public static Length operator -(Length v) => new(-v.m_value);
    public static Length operator *(Length a, Length b) => new(a.m_value * b.m_value);
    public static Length operator /(Length a, Length b) => new(a.m_value / b.m_value);
    public static Length operator %(Length a, Length b) => new(a.m_value % b.m_value);
    public static Length operator +(Length a, Length b) => new(a.m_value + b.m_value);
    public static Length operator -(Length a, Length b) => new(a.m_value - b.m_value);
    public static Length operator *(Length a, double b) => new(a.m_value * b);
    public static Length operator /(Length a, double b) => new(a.m_value / b);
    public static Length operator %(Length a, double b) => new(a.m_value % b);
    public static Length operator +(Length a, double b) => new(a.m_value + b);
    public static Length operator -(Length a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Length o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Length other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + LengthUnit.Meter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(LengthUnit unit, double value)
      => unit switch
      {
        LengthUnit.Meter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(LengthUnit unit, double value)
      => unit switch
      {
        LengthUnit.Meter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, LengthUnit from, LengthUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(LengthUnit unit)
      => ConvertToUnit(unit, m_value);

    public string ToUnitString(LengthUnit unit = LengthUnit.Meter, string? format = null, System.IFormatProvider? provider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, provider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Length.Value"/> property is in <see cref="LengthUnit.Meter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
