namespace Flux.Units
{
  /// <summary>
  /// <para>Area, unit of square meter. This is an SI derived quantity.</para>
  /// <see href="https://en.wikipedia.org/wiki/Area"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="Length"/>, <see cref="Area"/> and <see cref="Volume"/>.</remarks>
  public readonly record struct Area
    : System.IComparable, System.IComparable<Area>, System.IFormattable, ISiUnitValueQuantifiable<double, AreaUnit>
  {
    private readonly double m_value;

    public Area(double value, AreaUnit unit = AreaUnit.SquareMeter) => m_value = ConvertFromUnit(unit, value);

    public Area(MetricPrefix prefix, double squareMeter) => m_value = prefix.ChangePrefix(squareMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #region Conversions

    public static double ConvertHectareToSquareMeter(double hectare) => hectare * 10000;

    public static double ConvertSquareMeterToHectare(double squareMeter) => squareMeter / 10000;

    #endregion // Conversions

    #region Area of geometric shapes

    /// <summary>
    /// <para>Computes the surface area of a circle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    public static double OfCircle(double radius)
      => double.Pi * radius * radius;

    /// <summary>
    /// <para>Computes the surface area of a closed cylinder with the specified <paramref name="radius"/> and <paramref name="height"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <returns>The surface area of a closed cylinder.</returns>
    public static double OfClosedCylinder(double radius, double height) => 2 * double.Pi * radius * (radius + height);

    /// <summary>
    /// <para>Computes the surface area of a cube with the specified <paramref name="sideLength"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double OfCube(double sideLength)
      => 6 * sideLength * sideLength;

    /// <summary>
    /// <para>Computes the surface area of a cuboid with the specified <paramref name="length"/>, <paramref name="width"/> and <paramref name="height"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="length">The length of a rectangle.</param>
    /// <param name="width">The width of a rectangle.</param>
    /// <param name="height">The width of a rectangle.</param>
    public static double OfCuboid(double length, double width, double height)
      => (2 * length * width) + (2 * length * height) + (2 * width * height);

    public static double OfCylindricalAnnulus(double externalRadius, double internalRadius, double height) => 2 * double.Pi * (externalRadius + internalRadius) * (externalRadius - internalRadius + height);

    /// <summary>
    /// <para>Returns the area of an ellipse with the two specified semi-axes or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter).</para>
    /// </summary>
    public static double OfEllipse(double a, double b)
      => double.Pi * a * b;

    /// <summary>
    /// <para>Computes the surface area of a hemisphere with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="radius">The radius of the hemisphere.</param>
    public static double OfHemisphere(double radius) => 3 * double.Pi * radius * radius;

    /// <summary>
    /// <para>Computes the surface area of a hemispherical shell with the specified <paramref name="externalRadius"/> and <paramref name="internalRadius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="radius">The radius of the hemispherical shell.</param>
    public static double OfHemisphericalShell(double externalRadius, double internalRadius) => double.Pi * (3 * externalRadius * externalRadius + internalRadius * internalRadius);

    /// <summary>Calculates the surface area for a hexagon with the specified length (which is the length of a side or the outer radius).</summary>
    /// <param name="length">Length of the side (or outer radius, i.e. half outer diameter).</param>
    public static double OfHexagon(double sideLength)
      => 3 * double.Sqrt(3) / 2 * (sideLength * sideLength);

    /// <summary>
    /// <para>Computes the surface area of a rectangle with the specified <paramref name="length"/> and <paramref name="width"/>.</para>
    /// </summary>
    /// <param name="length">The length of a rectangle.</param>
    /// <param name="width">The width of a rectangle.</param>
    public static double OfRectangle(double length, double width)
      => length * width;

    /// <summary>
    /// <para>Computes the surface area of a regular polygon with the specified <paramref name="circumradius"/> and <paramref name="numberOfSides"/>.</para>
    /// </summary>
    /// <param name="circumradius"></param>
    /// <param name="numberOfSides"></param>
    /// <returns></returns>
    public static double OfRegularPolygon(double circumradius, int numberOfSides)
      => numberOfSides * circumradius * circumradius * double.Sin(2 * double.Pi / numberOfSides) / 2;

    /// <summary>
    /// <para>Computes the surface area of a semicircle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static double OfSemicircle(double radius)
      => OfCircle(radius) / 2;

    /// <summary>
    /// <para>Computes the surface area of a sphere with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="radius">The radius of the sphere.</param>
    public static double OfSphere(double radius) => 4 * double.Pi * radius * radius;

    /// <summary>
    /// <para>Computes the surface area of a spherical lune with the specified <paramref name="radius"/> and <paramref name="dihedralAngle"/>.</para>
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="dihedralAngle"></param>
    /// <returns>The surface area of a spherical lune.</returns>
    public static double OfSphericalLune(double radius, double dihedralAngle) => 2 * radius * radius * dihedralAngle;

    /// <summary>
    /// <para>Computes the surface area of a square with the specified <paramref name="sideLength"/>.</para>
    /// </summary>
    /// <param name="sideLength">The sidelength of a rectangle.</param>
    public static double OfSquare(double sideLength)
      => sideLength * sideLength;

    /// <summary>
    /// <para>Computes the surface area of a square pyramid with the specified <paramref name="baseLength"/> and <paramref name="verticalHeight"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="baseLength"></param>
    /// <param name="verticalHeight"></param>
    /// <returns></returns>
    public static double OfSquarePyramid(double baseLength, double verticalHeight)
      => (baseLength * baseLength) + (2 * baseLength) * double.Sqrt(double.Pow(baseLength / 2, 2) + (verticalHeight * verticalHeight));

    /// <summary>
    /// <para>Computes the surface area of a triangle with the specified <paramref name="baseLength"/> and <paramref name="height"/>.</para>
    /// </summary>
    /// <param name="baseLength"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static double OfTriangle(double baseLength, double height)
      => baseLength * height / 2;

    /// <summary>
    /// <para>Computes the surface area of a tetrahedron (triangular pyramid) with the specified <paramref name="sideLength"/>.</para>
    /// </summary>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double OfTetrahedron(double sideLength)
      => double.Sqrt(3) * sideLength * sideLength;

    #endregion // Area of geometric shapes

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Area a, Area b) => a.CompareTo(b) < 0;
    public static bool operator >(Area a, Area b) => a.CompareTo(b) > 0;
    public static bool operator <=(Area a, Area b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Area a, Area b) => a.CompareTo(b) >= 0;

    public static Area operator -(Area v) => new(-v.m_value);
    public static Area operator *(Area a, Area b) => new(a.m_value * b.m_value);
    public static Area operator /(Area a, Area b) => new(a.m_value / b.m_value);
    public static Area operator %(Area a, Area b) => new(a.m_value % b.m_value);
    public static Area operator +(Area a, Area b) => new(a.m_value + b.m_value);
    public static Area operator -(Area a, Area b) => new(a.m_value - b.m_value);
    public static Area operator *(Area a, double b) => new(a.m_value * b);
    public static Area operator /(Area a, double b) => new(a.m_value / b);
    public static Area operator %(Area a, double b) => new(a.m_value % b);
    public static Area operator +(Area a, double b) => new(a.m_value + b);
    public static Area operator -(Area a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Area o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Area other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AreaUnit.SquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix, 2);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AreaUnit.SquareMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AreaUnit unit, double value)
      => unit switch
      {
        AreaUnit.SquareMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AreaUnit unit, double value)
      => unit switch
      {
        AreaUnit.SquareMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AreaUnit from, AreaUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AreaUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AreaUnit unit = AreaUnit.SquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Area.Value"/> property is in <see cref="AreaUnit.SquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
