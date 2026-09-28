namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region GeodeticToGeocentricLatitude

      public static double GeodeticToGeocentricLatitude(double geodeticLatitude, double equatorRadius, double polarRadius)
      {
        var flatteningCorrection = (polarRadius * polarRadius) / (equatorRadius * equatorRadius); // b^2 / a^2

        return double.Atan(flatteningCorrection * double.Tan(geodeticLatitude)); // The forward direction "shrinks" the tangent.
      }

      #endregion

      #region GeocentricToGeodeticLatitude

      public static double GeocentricToGeodeticLatitude(double geocentricLatitude, double equatorRadius, double polarRadius)
      {
        var inverseFlatteningCorrection = (equatorRadius * equatorRadius) / (polarRadius * polarRadius); // a^2 / b^2

        return double.Atan(inverseFlatteningCorrection * double.Tan(geocentricLatitude)); // The reverse direction "unshrinks" the tangent.
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>
    {
      //#region GeographicToSpherical

      ///// <summary>Converts a geographic-coordinate into a spherical-coordinate.</summary>
      //public static (TFloat radius, TFloat inclination, TFloat azimuth) GeographicToSpherical(TFloat latitude, TFloat longitude, TFloat altitude)
      //// Translates the geographic coordinate to spherical coordinate transparently. I cannot recall the reason for the System.Math.PI involvement (see remarks).
      //{
      //  return new(
      //    altitude,
      //    (TFloat.Pi / TFloat.CreateChecked(2)) - latitude, // Add 90 degrees to convert from [-90..+90] (elevation, lat/lon) to [+0..+180] (inclination).
      //    longitude
      //  );
      //}

      //#endregion

      //#region SphericalToGeographic

      ///// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      ///// <remarks>All angles in radians.</remarks>
      //public static (TFloat latitude, TFloat longitude, TFloat altitude) SphericalToGeographic(TFloat radius, TFloat inclination, TFloat azimuth)
      //  => new(
      //    TFloat.Pi / TFloat.CreateChecked(2) - inclination,
      //    azimuth,
      //    radius
      //  );

      //#endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      //#region CylindricalToCartesian

      ///// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
      ///// <remarks>All angles in radians.</remarks>
      //public static (TFloat x, TFloat y, TFloat z) CylindricalToCartesian(TFloat radius, TFloat azimuth, TFloat height)
      //{
      //  var (sin, cos) = TFloat.SinCos(azimuth);

      //  return (
      //    radius * cos,
      //    radius * sin,
      //    height
      //  );
      //}

      //#endregion

      //#region PolarToCartesian

      ///// <summary>
      ///// <para>Creates cartesian-coordinates (x, y) from polar-coordinates (radius, azimuth).</para>
      ///// <list type="bullet">
      ///// <item>When <c><paramref name="originStandardPosition"/> = true</c> then 'right-center' is 0 (i.e. positive-x and zero-y) with a counter-clockwise positive rotation angle [0, PI*2]. Looking at the face of a clock it's counter-clockwise from 3 o'clock.</item>
      ///// <item>When <c><paramref name="originStandardPosition"/> = false</c> then 'center-up' is 0 (i.e. zero-x and positive-y) with a clockwise positive rotation angle [0, PI*2]. Looking at the face of a clock (or a compass) it's clockwise from 12 o'clock (noon).</item>
      ///// </list>
      ///// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      ///// </summary>
      //public static (TFloat x, TFloat y) PolarToCartesian(TFloat radius, TFloat azimuth, bool originStandardPosition)
      //{
      //  var (sin, cos) = TFloat.SinCos(azimuth);

      //  return originStandardPosition ? (radius * cos, radius * sin) : (radius * sin, radius * cos);
      //}

      //#endregion

      //#region SphericalToCartesian

      ///// <summary>
      ///// <para>Creates cartesian-coordinates from spherical-coordinates.</para>
      ///// <remarks>All angles in radians.</remarks>
      ///// </summary>
      ///// <param name="radius"></param>
      ///// <param name="inclination">If only elevation is known, then pass "<c>(TFloat.Pi / 2) - elevation</c>" (i.e. <c>90 - elevation</c>, in radians) as inclination.</param>
      ///// <param name="azimuth"></param>
      ///// <returns></returns>
      //public static (TFloat x, TFloat y, TFloat z) SphericalToCartesian(TFloat radius, TFloat inclination, TFloat azimuth)
      //{
      //  var (si, ci) = TFloat.SinCos(inclination);
      //  var (sa, ca) = TFloat.SinCos(azimuth);

      //  return (
      //    radius * si * ca,
      //    radius * si * sa,
      //    radius * ci
      //  );
      //}

      //#endregion

      //#region SphericalToCylindrical

      ///// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      //public static (TFloat radius, TFloat azimuth, TFloat height) SphericalToCylindrical(TFloat radius, TFloat inclination, TFloat azimuth)
      //{
      //  var (si, ci) = TFloat.SinCos(inclination);

      //  return new(
      //    radius * si,
      //    azimuth,
      //    radius * ci
      //  );
      //}

      //#endregion

      //#region SphericalTriaxialToCartesian

      ///// <summary>
      ///// <para>Creates cartesian-coordinates from spherical-coordinates, but as a triaxial ellipsoid with three radii for each of the X (A), Y (B) and Z (C) axis, instead of a single radius.</para>
      ///// <remarks>All angles in radians.</remarks>
      ///// </summary>
      ///// <param name="radiusA"></param>
      ///// <param name="radiusB"></param>
      ///// <param name="radiusC"></param>
      ///// <param name="polarAngle">The polar angle (inclination). <c>[0, Pi]</c></param>
      ///// <param name="azimuth">The azimuth angle. <c>[0, Tau)</c></param>
      ///// <returns></returns>
      //public static (double x, double y, double z) SphericalTriaxialToCartesian(double radiusA, double radiusB, double radiusC, double polarAngle, double azimuth)
      //{
      //  var (si, ci) = double.SinCos(polarAngle);
      //  var (sa, ca) = double.SinCos(azimuth);

      //  return (
      //    radiusA * si * ca,
      //    radiusB * si * sa,
      //    radiusC * ci
      //  );
      //}

      //#endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
    {
      //#region CartesianToCylindrical

      ///// <summary>Creates a new <see cref="CylindricalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      //public static (TFloat radius, TFloat azimuth, TFloat height) CartesianToCylindrical(TFloat x, TFloat y, TFloat z)
      //=> (
      //  TFloat.Sqrt(x * x + y * y),
      //  TFloat.Atan2(y, x) % TFloat.Pi,
      //  z
      //);

      //#endregion

      //#region CartesianToPolar

      ///// <summary>
      ///// <para>Creates polar-coordinates (radius, azimuth) from cartesian-coordinates (x, y).</para>
      ///// <list type="bullet">
      ///// <item>When <c><paramref name="originStandardPosition"/> = true</c> then 'right-center' is 0 (i.e. positive-x and zero-y) with a counter-clockwise positive rotation angle [0, Tau]. Looking at the face of a clock it's counter-clockwise from 3 o'clock.</item>
      ///// <item>When <c><paramref name="originStandardPosition"/> = false</c> then 'center-up' is 0 (i.e. zero-x and positive-y) with a clockwise positive rotation angle [0, Tau]. Looking at the face of a clock (or a compass) it's clockwise from 12 o'clock (noon).</item>
      ///// </list>
      ///// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      ///// </summary>
      //public static (TFloat radius, TFloat azimuth) CartesianToPolar(TFloat x, TFloat y, bool originStandardPosition)
      //{
      //  var azimuth = originStandardPosition ? TFloat.Atan2(y, x) : TFloat.Atan2(x, y);

      //  if (TFloat.IsNegative(azimuth))
      //    azimuth += TFloat.Tau;

      //  return (
      //    TFloat.Sqrt(x * x + y * y),
      //    azimuth
      //  );
      //}

      //#endregion

      //#region CartesianToSpherical

      ///// <summary>Creates a new <see cref="SphericalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      //public static (TFloat radius, TFloat inclination, TFloat azimuth) CartesianToSpherical(TFloat x, TFloat y, TFloat z)
      //{
      //  var x2y2 = x * x + y * y;

      //  return (
      //    TFloat.Sqrt(x2y2 + z * z),
      //    TFloat.Atan2(TFloat.Sqrt(x2y2), z),
      //    TFloat.Atan2(y, x)
      //  );
      //}

      //#endregion // Conversion methods

      //#region CylindricalToSpherical

      ///// <summary>
      ///// <para>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="CylindricalCoordinate"/>.</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system#Spherical_coordinates"/></para>
      ///// </summary>
      ///// <remarks>All angles in radians.</remarks>
      //public static (TFloat radius, TFloat inclination, TFloat azimuth) CylindricalToSpherical(TFloat radius, TFloat azimuth, TFloat height)
      //{
      //  var r = radius;
      //  var h = height;

      //  return new(
      //    TFloat.Sqrt(r * r + h * h),
      //    (TFloat.Pi / TFloat.CreateChecked(2)) - TFloat.Atan(h / r), // "double.Atan(m_radius / m_height);", does NOT work for Takapau, New Zealand. Have to use elevation math instead of inclination, and investigate.
      //    azimuth
      //  );
      //}

      //#endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region CartesianToCylindrical

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public static void CartesianToCylindrical<TFloat>(TNumber cartesianX, TNumber cartesianY, TNumber cartesianZ, out TFloat cylindricalRadius, out TFloat cylindricalAzimuth, out TFloat cylindricalHeight)
        where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
      {
        var x = TFloat.CreateChecked(cartesianX);
        var y = TFloat.CreateChecked(cartesianY);
        var z = TFloat.CreateChecked(cartesianZ);

        cylindricalRadius = TFloat.Sqrt(x * x + y * y);
        cylindricalAzimuth = TFloat.Atan2(y, x) % TFloat.Pi;
        cylindricalHeight = z;
      }

      #endregion

      #region CartesianToPolar

      /// <summary>
      /// <para>Creates polar-coordinates (radius, azimuth) from cartesian-coordinates (x, y).</para>
      /// <list type="bullet">
      /// <item>When <c><paramref name="originStandardPosition"/> = true</c> then 'right-center' is 0 (i.e. positive-x and zero-y) with a counter-clockwise positive rotation angle [0, Tau]. Looking at the face of a clock it's counter-clockwise from 3 o'clock.</item>
      /// <item>When <c><paramref name="originStandardPosition"/> = false</c> then 'center-up' is 0 (i.e. zero-x and positive-y) with a clockwise positive rotation angle [0, Tau]. Looking at the face of a clock (or a compass) it's clockwise from 12 o'clock (noon).</item>
      /// </list>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static void CartesianToPolar<TFloat>(TNumber cartesianX, TNumber cartesianY, bool originStandardPosition, out TFloat polarRadius, out TFloat polarAzimuth)
        where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
      {
        var x = TFloat.CreateChecked(cartesianX);
        var y = TFloat.CreateChecked(cartesianY);

        var azimuth = originStandardPosition ? TFloat.Atan2(y, x) : TFloat.Atan2(x, y);

        if (TFloat.IsNegative(azimuth))
          azimuth += TFloat.Tau;

        polarRadius = TFloat.Sqrt(x * x + y * y);
        polarAzimuth = azimuth;
      }

      #endregion

      #region CartesianToSpherical

      /// <summary>Creates a new <see cref="SphericalCoordinate"/> from a <see cref="CartesianCoordinate"/> and its (X, Y, Z) components.</summary>
      public static void CartesianToSpherical<TFloat>(TFloat cartesianX, TFloat cartesianY, TFloat cartesianZ, out TFloat sphericalRadius, out TFloat sphericalInclination, out TFloat sphericalAzimuth)
        where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
      {
        var x = TFloat.CreateChecked(cartesianX);
        var y = TFloat.CreateChecked(cartesianY);
        var z = TFloat.CreateChecked(cartesianZ);

        var x2y2 = x * x + y * y;

        sphericalRadius = TFloat.Sqrt(x2y2 + z * z);
        sphericalInclination = TFloat.Atan2(TFloat.Sqrt(x2y2), z);
        sphericalAzimuth = TFloat.Atan2(y, x);
      }

      #endregion

      #region CylindricalToCartesian

      /// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public static void CylindricalToCartesian<TFloat>(TNumber cylindricalRadius, TNumber cylindricalAzimuth, TNumber cylindricalHeight, out TFloat cartesianX, out TFloat cartesianY, out TFloat cartesianZ)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var r = TFloat.CreateChecked(cylindricalRadius);
        var a = TFloat.CreateChecked(cylindricalAzimuth);
        var h = TFloat.CreateChecked(cylindricalHeight);

        var (sin, cos) = TFloat.SinCos(a);

        cartesianX = r * cos;
        cartesianY = r * sin;
        cartesianZ = h;
      }

      #endregion

      #region CylindricalToSpherical

      /// <summary>
      /// <para>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="CylindricalCoordinate"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system#Spherical_coordinates"/></para>
      /// </summary>
      /// <remarks>All angles in radians.</remarks>
      public static void CylindricalToSpherical<TFloat>(TNumber cylindricalRadius, TNumber cylindricalAzimuth, TNumber cylindricalHeight, out TFloat sphericalRadius, out TFloat sphericalInclination, out TFloat sphericalAzimuth)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>, System.Numerics.IRootFunctions<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var r = TFloat.CreateChecked(cylindricalRadius);
        var a = TFloat.CreateChecked(cylindricalAzimuth);
        var h = TFloat.CreateChecked(cylindricalHeight);

        sphericalRadius = TFloat.Sqrt(r * r + h * h);
        sphericalInclination = (TFloat.Pi / TFloat.CreateChecked(2)) - TFloat.Atan(h / r); // "double.Atan(m_radius / m_height);", does NOT work for Takapau, New Zealand. Have to use elevation math instead of inclination, and investigate.
        sphericalAzimuth = a;
      }

      #endregion

      #region GeographicToSpherical

      /// <summary>Converts a geographic-coordinate into a spherical-coordinate.</summary>
      public static void GeographicToSpherical<TFloat>(TNumber geographicLatitude, TNumber geographicLongitude, TNumber geographicAltitude, out TFloat sphericalRadius, out TFloat sphericalInclination, out TFloat sphericalAzimuth)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        // Translates the geographic coordinate to spherical coordinate transparently. I cannot recall the reason for the System.Math.PI involvement (see remarks).

        var lat = TFloat.CreateChecked(geographicLatitude);
        var lon = TFloat.CreateChecked(geographicLongitude);
        var alt = TFloat.CreateChecked(geographicAltitude);

        sphericalRadius = alt;
        sphericalInclination = (TFloat.Pi / TFloat.CreateChecked(2)) - lat; // Add 90 degrees to convert from [-90..+90] (elevation, lat/lon) to [+0..+180] (inclination).
        sphericalAzimuth = lon;
      }

      #endregion

      #region PolarToCartesian

      /// <summary>
      /// <para>Creates cartesian-coordinates (x, y) from polar-coordinates (radius, azimuth).</para>
      /// <list type="bullet">
      /// <item>When <c><paramref name="originStandardPosition"/> = true</c> then 'right-center' is 0 (i.e. positive-x and zero-y) with a counter-clockwise positive rotation angle [0, PI*2]. Looking at the face of a clock it's counter-clockwise from 3 o'clock.</item>
      /// <item>When <c><paramref name="originStandardPosition"/> = false</c> then 'center-up' is 0 (i.e. zero-x and positive-y) with a clockwise positive rotation angle [0, PI*2]. Looking at the face of a clock (or a compass) it's clockwise from 12 o'clock (noon).</item>
      /// </list>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static void PolarToCartesian<TFloat>(TNumber polarRadius, TNumber polarAzimuth, bool originStandardPosition, out TFloat cartesianX, out TFloat cartesianY)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var r = TFloat.CreateChecked(polarRadius);
        var a = TFloat.CreateChecked(polarAzimuth);

        var (sin, cos) = TFloat.SinCos(a);

        var rc = r * cos;
        var rs = r * sin;

        (cartesianX, cartesianY) = originStandardPosition
          ? (rc, rs)
          : (rs, rc);
      }

      #endregion

      #region SphericalToCartesian

      /// <summary>
      /// <para>Creates cartesian-coordinates from spherical-coordinates.</para>
      /// <remarks>All angles in radians.</remarks>
      /// </summary>
      /// <param name="radius"></param>
      /// <param name="inclination">If only elevation is known, then pass "<c>(TFloat.Pi / 2) - elevation</c>" (i.e. <c>90 - elevation</c>, in radians) as inclination.</param>
      /// <param name="azimuth"></param>
      /// <returns></returns>
      public static void SphericalToCartesian<TFloat>(TNumber sphericalRadius, TNumber sphericalInclination, TNumber sphericalAzimuth, out TFloat cartesianX, out TFloat cartesianY, out TFloat cartesianZ)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var r = TFloat.CreateChecked(sphericalRadius);
        var i = TFloat.CreateChecked(sphericalInclination);
        var a = TFloat.CreateChecked(sphericalAzimuth);

        var (si, ci) = TFloat.SinCos(i);
        var (sa, ca) = TFloat.SinCos(a);

        cartesianX = r * si * ca;
        cartesianY = r * si * sa;
        cartesianZ = r * ci;
      }

      #endregion

      #region SphericalToCylindrical

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      public static void SphericalToCylindrical<TFloat>(TNumber sphericalRadius, TNumber sphericalInclination, TNumber sphericalAzimuth, out TFloat cylindricalRadius, out TFloat cylindricalAzimuth, out TFloat cylindricalHeight)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var r = TFloat.CreateChecked(sphericalRadius);
        var i = TFloat.CreateChecked(sphericalInclination);
        var a = TFloat.CreateChecked(sphericalAzimuth);

        var (si, ci) = TFloat.SinCos(i);

        cylindricalRadius = r * si;
        cylindricalAzimuth = a;
        cylindricalHeight = r * ci;
      }

      #endregion

      #region SphericalToGeographic

      /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public static void SphericalToGeographic<TFloat>(TNumber sphericalRadius, TNumber sphericalInclination, TNumber sphericalAzimuth, out TFloat geographicLatitude, out TFloat geographicLongitude, out TFloat geographicAltitude)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var r = TFloat.CreateChecked(sphericalRadius);
        var i = TFloat.CreateChecked(sphericalInclination);
        var a = TFloat.CreateChecked(sphericalAzimuth);

        geographicLatitude = TFloat.Pi / TFloat.CreateChecked(2) - i;
        geographicLongitude = a;
        geographicAltitude = r;
      }

      #endregion

      #region SphericalTriaxialToCartesian

      /// <summary>
      /// <para>Creates cartesian-coordinates from spherical-coordinates, but as a triaxial ellipsoid with three radii for each of the X (A), Y (B) and Z (C) axis, instead of a single radius.</para>
      /// <remarks>All angles in radians.</remarks>
      /// </summary>
      /// <param name="radiusA"></param>
      /// <param name="radiusB"></param>
      /// <param name="radiusC"></param>
      /// <param name="polarAngle">The polar angle (inclination). <c>[0, Pi]</c></param>
      /// <param name="azimuth">The azimuth angle. <c>[0, Tau)</c></param>
      /// <returns></returns>
      public static void SphericalTriaxialToCartesian<TFloat>(TNumber sphericalRadiusA, TNumber sphericalRadiusB, TNumber sphericalRadiusC, TNumber sphericalInclination, TNumber sphericalAzimuth, out TFloat cartesianX, out TFloat cartesianY, out TFloat cartesianZ)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var ra = TFloat.CreateChecked(sphericalRadiusA);
        var rb = TFloat.CreateChecked(sphericalRadiusB);
        var rc = TFloat.CreateChecked(sphericalRadiusC);
        var i = TFloat.CreateChecked(sphericalInclination);
        var a = TFloat.CreateChecked(sphericalAzimuth);

        var (si, ci) = TFloat.SinCos(i);
        var (sa, ca) = TFloat.SinCos(a);

        cartesianX = ra * si * ca;
        cartesianY = rb * si * sa;
        cartesianZ = rc * ci;
      }

      #endregion
    }
  }
}
