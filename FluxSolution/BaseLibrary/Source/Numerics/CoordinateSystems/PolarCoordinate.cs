namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Coordinates.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
    {
      var x = source.X;
      var y = source.Y;

      return new(
        System.Math.Sqrt(x * x + y * y), Quantities.LengthUnit.Metre,
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
      public static readonly PolarCoordinate Zero;

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
        : this(radiusMeter, Quantities.LengthUnit.Metre, azimuthRadian, Quantities.AngleUnit.Radian) { }

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

      public double CirclePerimeter => PolarCoordinate.PerimeterOfCircle(m_radius.Value);

      public double CircleSurfaceArea => PolarCoordinate.SurfaceAreaOfCircle(m_radius.Value);

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

      /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside of a circle with the specified <paramref name="radius"/>.</summary>
      public static bool CircleContainsPoint(double radius, double x, double y) => System.Math.Pow(x, 2) + System.Math.Pow(y, 2) <= System.Math.Pow(radius, 2);

      /// <summary>
      /// <para>Creates a circle consisting of <paramref name="count"/> vertices transformed with <paramref name="resultSelector"/> starting at <paramref name="arcOffset"/> and optional <paramref name="maxRandomness"/> (using <paramref name="rng"/>) unit interval (toward 0 = no random, toward 1 = total random).</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(3, 100, 100, 0); // triangle, top pointy</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(3, 100, 100, double.Tau / 6); // triangle, bottom pointy</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(4, 100, 100, 0); // rectangle, horizontally and vertically pointy</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(4, 100, 100, double.Tau / 8); // rectangle, vertically and horizontally flat</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(5, 100, 100, 0); // pentagon, horizontally pointy</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(5, 100, 100, double.Tau / 10); // pentagon, vertically pointy</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(6, 100, 100, 0); // hexagon, vertically flat (or horizontally pointy)</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(6, 100, 100, double.Tau / 12); // hexagon, horizontally flat (or vertically pointy)</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(8, 100, 100, 0); // octagon, horizontally and vertically pointy</para>
      /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(8, 100, 100, double.Tau / 16); // octagon, vertically and horizontally flat</para>
      /// </summary>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="count"></param>
      /// <param name="resultSelector">The selector that determines the result (<typeparamref name="TResult"/>) for each vector.</param>
      /// <param name="arcOffset">The offset in radians to apply to each vector.</param>
      /// <param name="maxRandomness">The maximum randomness to allow for each vector. Must be in the range [0, 0.5].</param>
      /// <param name="rng">The random number generator to use, or default if null.</param>
      /// <returns>A new sequence of <typeparamref name="TResult"/>.</returns>
      public static System.Collections.Generic.List<TResult> CreateCircleVectors<TResult>(double count, System.Func<double, double, TResult> resultSelector, double radius = 1, double arcOffset = 0, double maxRandomness = 0, System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        var list = new System.Collections.Generic.List<TResult>(System.Convert.ToInt32(System.Math.Ceiling(count)));

        var arcLength = System.Math.Tau / count;

        for (var segment = 0; segment < count; segment++)
        {
          var angle = arcOffset + segment * arcLength;

          if (maxRandomness > 0)
            angle += rng.NextDouble(0, arcLength * maxRandomness);

          var (x, y) = ConvertPolarToCartesian2Ex(radius, angle);

          list.Add(resultSelector(x, y));
        }

        return list;
      }

      /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse with the the two specified semi-axes or radii (<paramref name="a"/>, <paramref name="b"/>). The ellipse <paramref name="a"/> and <paramref name="b"/> correspond to same axes as <paramref name="x"/> and <paramref name="y"/> of the point, respectively.</summary>
      public static bool EllipseContainsPoint(double a, double b, double x, double y, double rotationAngle = 0)
        => System.Math.Cos(rotationAngle) is var cos && System.Math.Sin(rotationAngle) is var sin && System.Math.Pow(cos * x + sin * y, 2) / (a * a) + System.Math.Pow(sin * x - cos * y, 2) / (b * b) <= 1;

      #region Conversions

      /// <summary>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static (double radius, double azimuth) ConvertCartesian2ToPolar(double x, double y)
        => new(
          System.Math.Sqrt(x * x + y * y),
          System.Math.Atan2(y, x)
        );

      /// <summary>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static (double radius, double azimuth) ConvertCartesian2ToPolarEx(double x, double y)
        => (
          System.Math.Sqrt(x * x + y * y),
          //System.Math.Tau - System.Math.Atan2(-x, y) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2
          System.Math.Atan2(x, y) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2
        );

      /// <summary>Convert the polar coordinate [0, PI*2] (i.e. radians) where 'zero' azimuth is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      public static (double x, double y) ConvertPolarToCartesian2(double radius, double azimuth)
      {
        var (sin, cos) = System.Math.SinCos(azimuth);

        return (radius * cos, radius * sin);
      }

      /// <summary>Convert the polar coordinate [0, PI*2] (i.e. radians) where 'zero' azimuth is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      public static (double x, double y) ConvertPolarToCartesian2Ex(double radius, double azimuth)
        => ConvertPolarToCartesian2(radius, (System.Math.Tau * 1.25) - (azimuth % System.Math.Tau) is var h && h > System.Math.Tau ? h - System.Math.Tau : h);
      // Adjust azimuth.
      //{
      //  var adjustedAzimuth = (System.Math.Tau * 1.25) - (azimuth % System.Math.Tau) is var h && h > System.Math.Tau ? h - System.Math.Tau : h;
      //  //var adjustedAzimuth = System.Math.Tau - (azimuth % System.Math.Tau is var rad && rad < 0 ? rad + System.Math.Tau : rad) + System.Math.PI / 2;

      //  var (sin, cos) = System.Math.SinCos(adjustedAzimuth);

      //  return (radius * cos, radius * sin);
      //}

      #endregion // Conversions

      /// <summary>
      /// <para>Computes the perimeter (circumference) of a circle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
      /// </summary>
      public static double PerimeterOfCircle(double radius) => 2 * System.Math.PI * radius;

      /// <summary>Returns the approximate perimeter (circumference) of an ellipse with the two semi-axis or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
      public static double PerimeterOfEllipse(double a, double b)
      {
        var circle = System.Math.PI * (a + b); // (2 * PI * radius)

        if (a == b) // For a circle, use (PI * diameter);
          return circle;

        var h3 = 3 * (System.Math.Pow(a - b, 2) / System.Math.Pow(a + b, 2)); // H function.

        return circle * (1 + h3 / (10 + System.Math.Sqrt(4 - h3)));
      }

      /// <summary>
      /// <para>Computes the perimeter of a semicircle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
      /// </summary>
      public static double PerimeterOfSemicircle(double radius) => (System.Math.PI + 2) * radius;

      /// <summary>
      /// <para>Computes the surface area of a circle with the specified <paramref name="radius"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      public static double SurfaceAreaOfCircle(double radius) => System.Math.PI * radius * radius;

      /// <summary>Returns the area of an ellipse with the two specified semi-axes or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter).</summary>
      public static double SurfaceAreaOfEllipse(double a, double b) => System.Math.PI * a * b;

      #endregion // Static methods

      #region Implemented interfaces

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{m_radius.Value.ToString(format ?? "N3", provider)}, {m_azimuth.ToUnitValueString(Quantities.AngleUnit.Degree, format ?? "N3", provider, UnicodeSpacing.Space, true)}>";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
