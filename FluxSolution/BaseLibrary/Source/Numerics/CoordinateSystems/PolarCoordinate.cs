namespace Flux
{
  #region ExtensionMethods
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Coordinates.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
    {
      var x = source.X;
      var y = source.Y;

      return new(
        System.Math.Sqrt(x * x + y * y), Quantities.LengthUnit.Meter,
        System.Math.Atan2(y, x), Quantities.AngleUnit.Radian
      );
    }
  }
  #endregion

  namespace Coordinates
  {
    /// <summary>
    /// <para>Polar coordinate. Please note that polar coordinates are two dimensional.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Polar_coordinate_system"/></para>
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct PolarCoordinate
      : System.IFormattable
    {
      public static PolarCoordinate Zero { get; }

      private readonly Quantities.Length m_radius;
      private readonly Quantities.Angle m_azimuth;

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(Quantities.Length radius, Quantities.Angle azimuth)
      {
        m_radius = radius;
        m_azimuth = azimuth;
      }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(double radiusValue, Quantities.LengthUnit radiusUnit, double azimuthValue, Quantities.AngleUnit azimuthUnit)
        : this(new Quantities.Length(radiusValue, radiusUnit), new Quantities.Angle(azimuthValue, azimuthUnit)) { }

      public PolarCoordinate(double radiusMeter, double azimuthRadian)
        : this(radiusMeter, Quantities.LengthUnit.Meter, azimuthRadian, Quantities.AngleUnit.Radian) { }

      public void Deconstruct(out double radiusMeter, out double azimuthRadian)
      {
        radiusMeter = m_radius.Value;
        azimuthRadian = m_azimuth.Value;
      }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial coordinate, or radial distance.</para>
      /// </summary>
      /// <remarks>If the radius is zero, azimuth is arbitrary.</remarks>
      public Quantities.Length Radius { get => m_radius; init => m_radius = value; }

      /// <summary>
      /// <para>Azimuth angle, unit of radian. A.k.a. angular coordinate, or polar angle.</para>
      /// </summary>
      /// <remarks>The angle is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
      public Quantities.Angle Azimuth { get => m_azimuth; init => m_azimuth = value; }

      public CartesianCoordinate ToCartesianCoordinate()
      {
        var (x, y) = ConvertPolarToCartesian2(m_radius.Value, m_azimuth.Value);

        return new(x, y);
      }

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="height"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate(Quantities.Length height)
        => new(
          m_radius,
          m_azimuth,
          height
        );

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="height"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate(double height, Quantities.LengthUnit heightUnit)
        => new(
          m_radius,
          m_azimuth,
          new Quantities.Length(height, heightUnit)
        );

      /// <summary>Creates a <see cref="System.Numerics.Complex"/> from the <see cref="PolarCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Complex ToComplex()
        => System.Numerics.Complex.FromPolarCoordinates(
          m_radius.Value,
          m_azimuth.Value
        );

      /// <summary>Creates a <see cref="System.Numerics.Vector2"/> from the <see cref="PolarCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Vector2 ToVector2()
      {
        var (x, y) = ConvertPolarToCartesian2(m_radius.Value, m_azimuth.Value);

        return new((float)x, (float)y);
      }

      #region Static methods

      #region Conversion methods

      /// <summary>
      /// <para>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (double radius, double azimuth) ConvertCartesian2ToPolar(double x, double y)
        => new(
          double.Sqrt(x * x + y * y),
          double.Atan2(y, x)
        );

      /// <summary>
      /// <para>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (double radius, double azimuth) ConvertCartesian2ToPolarEx(double x, double y)
        => (
          double.Sqrt(x * x + y * y),
          double.Atan2(x, y) is var atan2 && atan2 < 0 ? double.Tau + atan2 : atan2
        );

      /// <summary>
      /// <para>Convert the polar coordinate [0, Tau(2*Pi)] (i.e. radians) where 'zero' azimuth is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (double x, double y) ConvertPolarToCartesian2(double radius, double azimuth)
      {
        var (sin, cos) = double.SinCos(azimuth);

        return (radius * cos, radius * sin);
      }

      /// <summary>
      /// <para>Convert the polar coordinate [0, Tau(2*Pi)] (i.e. radians) where 'zero' azimuth is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (double x, double y) ConvertPolarToCartesian2Ex(double radius, double azimuth)
        => ConvertPolarToCartesian2(radius, (double.Tau * 1.25) - (azimuth % double.Tau) is var h && h > double.Tau ? h - double.Tau : h);

      #endregion // Conversion methods

      /// <summary>
      /// <para>Creates <paramref name="count"/> vertices along the circumference of the circle [<paramref name="radius"/>] with the <paramref name="arcOffset"/>, <paramref name="transposeX"/>, <paramref name="transposeY"/> and randomized by <paramref name="maxRandomness"/> using the <paramref name="rng"/>.</para>
      /// </summary>
      /// <param name="count"></param>
      /// <param name="radius">When scaleRadius equals 1, the result follows the circumradius of the unit circle.</param>
      /// <param name="arcOffset"></param>
      /// <param name="transposeX"></param>
      /// <param name="transposeY"></param>
      /// <param name="maxRandomness"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> CreateCircle(double count, double radius = 1, double arcOffset = 0, double transposeX = 0, double transposeY = 0, double maxRandomness = 0, System.Random? rng = null)
        => CreateEllipse(count, radius, radius, arcOffset, transposeX, transposeY, maxRandomness, rng);

      /// <summary>
      /// <para>Creates <paramref name="count"/> vertices along the perimeter of the ellipse [<paramref name="a"/>, <paramref name="b"/>] with the <paramref name="arcOffset"/>, <paramref name="transposeX"/>, <paramref name="transposeY"/> and randomized by <paramref name="maxRandomness"/> using the <paramref name="rng"/>.</para>
      /// </summary>
      /// <param name="count"></param>
      /// <param name="a">Correspond to the X-axis. If X and Y are both equal to 1, the result follows the circumradius of the unit circle.</param>
      /// <param name="b">Correspond to the Y-axis.</param>
      /// <param name="arcOffset"></param>
      /// <param name="transposeX"></param>
      /// <param name="transposeY"></param>
      /// <param name="maxRandomness"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector256<double>> CreateEllipse(double count, double a = 1, double b = 1, double arcOffset = 0, double transposeX = 0, double transposeY = 0, double maxRandomness = 0, System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        var arc = System.Math.Tau / count;

        for (var index = 0; index < count; index++)
        {
          var angle = arcOffset + index * arc;

          if (maxRandomness > 0)
            angle += rng.NextDouble(0, arc * maxRandomness);

          var (x, y) = Coordinates.PolarCoordinate.ConvertPolarToCartesian2Ex(1, angle);

          yield return System.Runtime.Intrinsics.Vector256.Create(x * a + transposeX, y * b + transposeY, 0, 0);
        }
      }

      /// <summary>
      /// <para>Computes the perimeter (circumference) of a circle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
      /// </summary>
      public static double PerimeterOfCircle(double radius)
        => 2 * double.Pi * radius;

      /// <summary>
      /// <para>Returns the approximate perimeter (circumference) of an ellipse with the two semi-axis or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter). Uses Ramanujans second approximation.</para>
      /// </summary>
      public static double PerimeterOfEllipse(double a, double b)
      {
        var circle = double.Pi * (a + b); // (2 * PI * radius)

        if (a == b) // For a circle, use (PI * diameter);
          return circle;

        var h3 = 3 * (double.Pow(a - b, 2) / double.Pow(a + b, 2)); // H function.

        return circle * (1 + h3 / (10 + double.Sqrt(4 - h3)));
      }

      /// <summary>
      /// <para>Computes the perimeter of a semicircle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
      /// </summary>
      public static double PerimeterOfSemicircle(double radius)
        => (double.Pi + 2) * radius;

      /// <summary>
      /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside of a circle with the specified <paramref name="radius"/>.</para>
      /// </summary>
      public static bool PointInCircle(double radius, double x, double y)
        => double.Pow(x, 2) + double.Pow(y, 2) <= double.Pow(radius, 2);

      /// <summary>
      /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse with the the two specified semi-axes or radii (<paramref name="a"/>, <paramref name="b"/>). The ellipse <paramref name="a"/> and <paramref name="b"/> correspond to same axes as <paramref name="x"/> and <paramref name="y"/> of the point, respectively.</para>
      /// </summary>
      public static bool PointInEllipse(double a, double b, double x, double y, double rotationAngle = 0)
        => double.Cos(rotationAngle) is var cos && double.Sin(rotationAngle) is var sin && double.Pow(cos * x + sin * y, 2) / (a * a) + double.Pow(sin * x - cos * y, 2) / (b * b) <= 1;

      /// <summary>
      /// <para>Computes the surface area of a circle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      public static double SurfaceAreaOfCircle(double radius)
        => double.Pi * radius * radius;

      /// <summary>
      /// <para>Returns the area of an ellipse with the two specified semi-axes or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter).</para>
      /// </summary>
      public static double SurfaceAreaOfEllipse(double a, double b)
        => double.Pi * a * b;

      /// <summary>
      /// <para>Computes the surface area of a semicircle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      /// <param name="radius"></param>
      /// <returns></returns>
      public static double SurfaceAreaOfSemicircle(double radius)
        => SurfaceAreaOfCircle(radius) / 2;

      #endregion // Static methods

      #region Implemented interfaces

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{m_radius.ToString(format ?? Format.UpTo3Decimals, provider)}, {m_azimuth.ToUnitString(Quantities.AngleUnit.Degree, format ?? Format.UpTo6Decimals, provider)}>";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
