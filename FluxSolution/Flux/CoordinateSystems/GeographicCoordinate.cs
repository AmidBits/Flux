namespace Flux
{
  /// <summary>
  /// These are extension methods for geographic coordinates, i.e. latitude and longitude on the surface of a sphere or ellipsoid. The methods include calculations for distances, bearings, and other geodesic properties.
  /// </summary>
  public static class GeoCoordinateExtensions
  {
    extension(CoordinateSystems.GeographicCoordinate)
    {
      /// <summary>The along-track distance, from the start point to the closest point on the path (lat/lon 1 to lat/lon 3) to the second point.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The query latitude in radians.</param>
      /// <param name="lon2">The query longitude in radians.</param>
      /// <param name="lat3">The target latitude in radians.</param>
      /// <param name="lon3">The target longitude in radians.</param>
      /// <param name="crossTrackCentralAngle">The query longitude in radians.</param>
      /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
      public static double GetAlongTrackCentralAngle(double lat1, double lon1, double lat2, double lon2, double lat3, double lon3, out double crossTrackCentralAngle)
      {
        crossTrackCentralAngle = GetCrossTrackCentralAngle(lat1, lon1, lat2, lon2, lat3, lon3, out var trackCentralAngle13);

        return double.Acos(double.Cos(trackCentralAngle13) / double.Cos(crossTrackCentralAngle));
      }

      /// <summary>
      /// <para>Computes initial and final bearings between two points.</para>
      /// </summary>
      /// <param name="lat1">The source latitude in degrees.</param>
      /// <param name="lon1">The source longitude in degrees.</param>
      /// <param name="lat2">The target latitude in degrees.</param>
      /// <param name="lon2">The target longitude in degrees.</param>
      /// <returns>A tuple containing the initial and final bearings in degrees.</returns>
      /// <returns></returns>
      public static (double InitialBearing, double FinalBearing) GetBearings(double lat1, double lon1, double lat2, double lon2)
      {
        lat1 = double.DegreesToRadians(lat1);
        lon1 = double.DegreesToRadians(lon1);
        lat2 = double.DegreesToRadians(lat2);
        lon2 = double.DegreesToRadians(lon2);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var (sinLonD, cosLonD) = double.SinCos(lon2 - lon1);

        var y = sinLonD * cosLat2;
        var x = cosLat1 * sinLat2 - sinLat1 * cosLat2 * cosLonD;

        var brgi = (double.Atan2(y, x) + double.Tau) % double.Tau; // Atan2 returns values in the range [-π, +π] radians (i.e. -180 - +180 degrees), shift to [0, 2PI] radians (i.e. 0 - 360 degrees).
        var brgf = (brgi + double.Pi) % double.Tau;

        return (double.RadiansToDegrees(brgi), double.RadiansToDegrees(brgf));
      }

      /// <summary>
      /// <para>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</para>
      /// <para>Haversine computes great‑circle distance on a sphere. This introduces errors up to 0.5% depending on latitude and distance.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Haversine_formula"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Central_angle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Great-circle_distance"/></para>
      /// </summary>
      /// <remarks>
      /// <para>The haversine formula is numerically better-conditioned for small distances. Although this formula is accurate for most distances on a sphere, it too suffers from rounding errors for the special (and somewhat unusual) case of antipodal points (on opposite ends of the sphere).</para>
      /// <para>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</para>
      /// </remarks>
      /// <param name="lat1">The source latitude in degrees.</param>
      /// <param name="lon1">The source longitude in degrees.</param>
      /// <param name="lat2">The target latitude in degrees.</param>
      /// <param name="lon2">The target longitude in degrees.</param>
      /// <returns>The central angle in radians.</returns>
      public static double GetCentralAngleHaversine(double lat1, double lon1, double lat2, double lon2)
      {
        lat1 = double.DegreesToRadians(lat1);
        lon1 = double.DegreesToRadians(lon1);
        lat2 = double.DegreesToRadians(lat2);
        lon2 = double.DegreesToRadians(lon2);

        var dLat = lat2 - lat1;
        var dLon = lon2 - lon1;

        var a = double.Hvsin(dLat) + double.Cos(lat1) * double.Cos(lat2) * double.Hvsin(dLon);

        var centralAngle = 2 * double.Asin(double.Sqrt(a));

        return centralAngle; // central angle in radians
      }

      /// <summary>The distance of a point from a great-circle path (sometimes called cross track error). The sign of the result tells which side of the path (lat/lon 1 to lat/lon 3) the second point is on.</summary>
      /// <param name="lat1">The source latitude in degrees.</param>
      /// <param name="lon1">The source longitude in degrees.</param>
      /// <param name="lat2">The query latitude in degrees.</param>
      /// <param name="lon2">The query longitude in degrees.</param>
      /// <param name="lat3">The target latitude in degrees.</param>
      /// <param name="lon3">The target longitude in degrees.</param>
      /// <param name="trackCentralAngle13">The query longitude in radians.</param>
      /// <remarks>Returns the cross track central angle, in radians. Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
      public static double GetCrossTrackCentralAngle(double lat1, double lon1, double lat2, double lon2, double lat3, double lon3, out double trackCentralAngle13)
      {
        trackCentralAngle13 = GetCentralAngleHaversine(lat1, lon1, lat3, lon3);

        var course13 = GetBearings(lat1, lon1, lat3, lon3).InitialBearing;
        var course12 = GetBearings(lat1, lon1, lat2, lon2).InitialBearing;

        return double.Asin(double.Sin(trackCentralAngle13) * double.Sin(double.DegreesToRadians(course13 - course12)));
      }

      /// <summary>Given a start point, initial bearing, and angularDistance, this will calculate the destination point and final bearing travelling along a (shortest distance) great circle arc.</summary>
      /// <remarks>The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</remarks>
      /// <param name="lat">The latitude in degrees.</param>
      /// <param name="lon">The longitude in degrees.</param>
      /// <param name="brg">Bearing is the direction or course in degrees.</param>
      /// <param name="angularDistance">The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</param>
      /// <returns>A tuple containing the destination latitude and longitude in degrees.</returns>
      public static (double DestinationLatitude, double DestinationLongitude) GetDestinationPoint(double lat, double lon, double brg, double angularDistance)
      {
        lat = double.DegreesToRadians(lat);
        lon = double.DegreesToRadians(lon);
        brg = double.DegreesToRadians(brg);

        var (sinLat, cosLat) = double.SinCos(lat);

        var (sinAd, cosAd) = double.SinCos(angularDistance);

        var (sinBrg, cosBrg) = double.SinCos(brg);

        var latd = double.Asin(sinLat * cosAd + cosLat * sinAd * cosBrg);
        var lond = lon + double.Atan2(sinBrg * sinAd * cosLat, cosAd - sinLat * sinLat);

        return (double.RadiansToDegrees(latd), double.RadiansToDegrees(lond));
      }

      /// <summary>
      /// <para>Computes the distance between the two lat/lon coordinates in whatever the unit is specified for Earths radius. I.e. if the distance in meters is desired then specify: <c><paramref name="earthsRadius"/> = 6371008.8</c></para>
      /// <para>Haversine computes great‑circle distance on a sphere. This introduces errors up to 0.5% depending on latitude and distance.</para>
      /// </summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <param name="earthsRadius">Specify Earth's radius in the unit desired in order to return the distance in that unit, e.g. meters = 6371008.8, kilometers = 6371.0088, for (US) miles: 3958.76133, etc.</param>
      /// <returns>The distance between the two points, in the same units as the specified Earth's radius.</returns>
      public static double GetDistanceHaversine(double lat1, double lon1, double lat2, double lon2, double earthsRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusMean)
        => earthsRadius * GetCentralAngleHaversine(lat1, lon1, lat2, lon2);

      /// <summary>
      /// <para>Vincenty distance implementation (inverse formula) in C#, using the WGS‑84 ellipsoid and proper λ‑iteration. This is the real Vincenty algorithm — not the spherical approximation. It returns the geodesic distance in meters and handles nearly all edge cases.</para>
      /// <para>Vincenty computes geodesics on an ellipsoid and is accurate to ≈0.1 mm under normal conditions</para>
      /// </summary>
      /// <param name="lat1"></param>
      /// <param name="lon1"></param>
      /// <param name="lat2"></param>
      /// <param name="lon2"></param>
      /// <returns>The distance between the two points, in meters.</returns>
      public static double GetDistanceVincenty(double lat1, double lon1, double lat2, double lon2)
      {
        // WGS‑84 ellipsoid parameters
        const double a = 6378137.0;              // major axis
        const double f = 1 / 298.257223563;      // flattening
        const double b = (1 - f) * a;            // minor axis

        // Convert degrees → radians
        var φ1 = double.DegreesToRadians(lat1);
        var φ2 = double.DegreesToRadians(lat2);
        var L = double.DegreesToRadians(lon2 - lon1);

        var U1 = double.Atan((1 - f) * double.Tan(φ1));
        var U2 = double.Atan((1 - f) * double.Tan(φ2));

        var (sinU1, cosU1) = double.SinCos(U1);
        var (sinU2, cosU2) = double.SinCos(U2);

        double λ = L;
        double λPrev;

        const int maxIter = 200;

        var iter = 0;
        double sinλ, cosλ, sinσ, cosσ, σ, sinα, cosSqα, cos2σm, C;

        do
        {
          (sinλ, cosλ) = double.SinCos(λ);

          sinσ = double.Sqrt(
              double.Pow(cosU2 * sinλ, 2) +
              double.Pow(cosU1 * sinU2 - sinU1 * cosU2 * cosλ, 2)
          );

          if (sinσ == 0)
            return 0; // coincident points

          cosσ = sinU1 * sinU2 + cosU1 * cosU2 * cosλ;
          σ = double.Atan2(sinσ, cosσ);

          sinα = cosU1 * cosU2 * sinλ / sinσ;
          cosSqα = 1 - sinα * sinα;

          cos2σm = (cosSqα == 0) ? 0 : cosσ - 2 * sinU1 * sinU2 / cosSqα;

          C = f / 16 * cosSqα * (4 + f * (4 - 3 * cosSqα));

          λPrev = λ;
          λ = L + (1 - C) * f * sinα *
              (σ + C * sinσ * (cos2σm + C * cosσ * (-1 + 2 * cos2σm * cos2σm)));

        }
        while (double.Abs(λ - λPrev) > 1e-12 && ++iter < maxIter);

        if (iter >= maxIter)
          return double.NaN; // formula failed to converge

        var uSq = cosSqα * (a * a - b * b) / (b * b);

        var A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));

        var B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));

        var Δσ = B * sinσ *
            (cos2σm + B / 4 *
            (cosσ * (-1 + 2 * cos2σm * cos2σm) -
             B / 6 * cos2σm * (-3 + 4 * sinσ * sinσ) *
             (-3 + 4 * cos2σm * cos2σm)));

        var s = b * A * (σ + Δσ); // distance in meters

        return s;
      }

      /// <summary>This is the halfway point along a great circle path between the two points.</summary>
      /// <param name="lat1">The first path latitude in radians.</param>
      /// <param name="lon1">The first path longitude in radians.</param>
      /// <param name="lat2">The second path latitude in radians.</param>
      /// <param name="lon2">The second path longitude in radians.</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static (double HalfwayLatitude, double HalfwayLongitude) GetHalfwayPoint(double lat1, double lon1, double lat2, double lon2)
      {
        lat1 = double.DegreesToRadians(lat1);
        lon1 = double.DegreesToRadians(lon1);
        lat2 = double.DegreesToRadians(lat2);
        lon2 = double.DegreesToRadians(lon2);

        var (sinLonD, cosLonD) = double.SinCos(lon2 - lon1);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var Bx = cosLat2 * cosLonD;
        var By = cosLat2 * sinLonD;

        var lath = double.Atan2(sinLat1 + sinLat2, double.Sqrt(double.Pow(cosLat1 + Bx, 2) + By * By));
        var lonh = lon1 + double.Atan2(By, cosLat1 + Bx);

        return (double.RadiansToDegrees(lath), double.RadiansToDegrees(lonh));
      }

      /// <summary>An intermediate point at any fraction along the great circle path between two points can also be calculated.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <param name="mu">Unit interval is a fraction along great circle route (0=Latitude1,Longitude1, 1=Latitude2,Longitude2)</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static (double IntermediaryLatitude, double IntermediaryLongitude) GetIntermediaryPoint(double lat1, double lon1, double lat2, double lon2, double mu)
      {
        var centralAngle = GetCentralAngleHaversine(lat1, lon1, lat2, lon2);

        lat1 = double.DegreesToRadians(lat1);
        lon1 = double.DegreesToRadians(lon1);
        lat2 = double.DegreesToRadians(lat2);
        lon2 = double.DegreesToRadians(lon2);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var sinCentralAngle = double.Sin(centralAngle);

        var a = double.Sin((1.0 - mu) * centralAngle) / sinCentralAngle;
        var b = double.Sin(mu * centralAngle) / sinCentralAngle;

        var (sinLon1, cosLon1) = double.SinCos(lon1);
        var (sinLon2, cosLon2) = double.SinCos(lon2);

        var x = a * cosLat1 * cosLon1 + b * cosLat2 * cosLon2;
        var y = a * cosLat1 * sinLon1 + b * cosLat2 * sinLon2;
        var z = a * sinLat1 + b * sinLat2;

        var lati = double.Atan2(z, double.Sqrt(x * x + y * y));
        var loni = double.Atan2(y, x);

        return (double.RadiansToDegrees(lati), double.RadiansToDegrees(loni));
      }

      /// <summary></summary>
      /// <param name="lat1">The first path latitude in radians.</param>
      /// <param name="lon1">The first path longitude in radians.</param>
      /// <param name="brg1">The first path bearing in radians.</param>
      /// <param name="lat2">The second path latitude in radians.</param>
      /// <param name="lon2">The second path longitude in radians.</param>
      /// <param name="brg2">The second path bearing in radians.</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static void GetIntersectionOfPaths(double lat1, double lon1, double brg1, double lat2, double lon2, double brg2, out double latOut, out double lonOut)
      {
        var latD = lat2 - lat1;
        var lonD = lon2 - lon1;
        var sinLonD = double.Sin(lonD);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var d12 = 2 * double.Asin(double.Sqrt(double.Pow(double.Sin(latD / 2), 2) + cosLat1 * cosLat2 * double.Pow(double.Sin(lonD / 2), 2)));
        var (sind12, cosd12) = double.SinCos(d12);

        var φ1 = double.Acos(sinLat2 - sinLat1 * cosd12 / sind12 * cosLat1);
        var φ2 = double.Acos(sinLat1 - sinLat2 * cosd12 / sind12 * cosLat2);

        var bearing12 = sinLonD > 0 ? φ1 : double.Tau - φ1;
        var bearing21 = sinLonD > 0 ? double.Tau - φ2 : φ2;

        var α1 = (brg1 - bearing12 + double.Pi) % double.Tau - double.Pi;
        var (α1sin, α1cos) = double.SinCos(α1);

        var α2 = (bearing21 - brg2 + double.Pi) % double.Tau - double.Pi;
        var (α2sin, α2cos) = double.SinCos(α2);

        var α3 = double.Acos(-α1cos * α2cos + α1sin * α2sin * cosd12);

        var d13 = double.Atan2(sind12 * α1sin * α2sin, α2cos + α1cos * double.Cos(α3));
        var (d13sin, d13cos) = double.SinCos(d13);

        latOut = double.Asin(sinLat1 * d13cos + cosLat1 * d13sin * double.Cos(brg1));

        var dLon13 = double.Atan2(double.Sin(brg1) * d13sin * cosLat1, d13cos - sinLat1 * double.Sin(latOut));

        lonOut = (lon1 + dLon13 + double.Pi) % double.Tau - double.Pi;
      }

      /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
      /// <param name="lat">The latitude in degrees.</param>
      /// <param name="brg">The bearing in degrees.</param>
      /// <returns>The maximum latitude in degrees.</returns>
      public static double GetMaximumLatitude(double lat, double brg)
      {
        lat = double.DegreesToRadians(lat);
        brg = double.DegreesToRadians(brg);

        var latm = double.Acos(double.Abs(double.Sin(brg) * double.Cos(lat)));

        return double.RadiansToDegrees(latm);
      }

      /// <summary>
      /// <para>Computes the radius of curvature at a given latitude.</para>
      /// </summary>
      /// <param name="lat">The latitude in degrees.</param>
      /// <param name="equatorialRadius">The equatorial radius of the ellipsoid.</param>
      /// <param name="polarRadius">The polar radius of the ellipsoid.</param>
      /// <returns>The radius of curvature in meters.</returns>
      public static double GetRadiusOfCurvature(double lat, double equatorialRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusEquatorial, double polarRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusPolar)
      {
        lat = double.DegreesToRadians(lat);

        var oneMinusEccentricitySquared = 1 - double.Pow(double.Sqrt(1 - (polarRadius * polarRadius) / (equatorialRadius * equatorialRadius)), 2);

        var sinLat = double.Sin(lat);

        return equatorialRadius * oneMinusEccentricitySquared / double.Pow(oneMinusEccentricitySquared * sinLat * sinLat, 1.5); // 1.5 = (3 / 2)
      }

    }
  }

  namespace CoordinateSystems
  {
    /// <summary>Represents a geographic position, using latitude, longitude and altitude.</summary>
    /// <seealso href="http://www.edwilliams.org/avform.htm"/>
    /// <seealso href="https://www.edwilliams.org/avform147.htm"/>
    /// <seealso href="http://www.movable-type.co.uk/scripts/latlong.html"/>
    /// <remarks>Abbreviated angles (e.g. lat and lon) are in radians, and full names (e.g. latitude and longitude) are in degrees.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct GeographicCoordinate
    : System.IFormattable
    {
      public const double MaxAltitudeInMeters = 1500000000;
      public const double MinAltitudeInMeters = -11000;

      public static GeographicCoordinate Empty { get; }

      public static GeographicCoordinate GreenwichMeridian { get; } = new(51.477811, Units.AngleUnit.Degree, -0.001475, Units.AngleUnit.Degree, 46, Units.LengthUnit.Meter);

      private readonly double m_latitude;

      private readonly double m_longitude;

      private readonly double m_altitude; // Geocentric altitude, i.e. the distance from the center of Earth, not the height above sea level.
      public GeographicCoordinate(double latitudeRadian, double longitudeRadian, double altitudeMeter)
      {
        m_latitude = new Units.Latitude(latitudeRadian, Units.AngleUnit.Radian).Radians;
        m_longitude = new Units.Longitude(longitudeRadian, Units.AngleUnit.Radian).Radians;
        m_altitude = altitudeMeter;// >= MinAltitudeInMeters && altitudeMeter <= MaxAltitudeInMeters ? altitudeMeter : throw new System.ArgumentOutOfRangeException(nameof(altitudeMeter));
      }

      /// <summary>
      /// <para>Create a new <see cref="GeographicCoordinate"/> from the specified <paramref name="latitude"/>, <paramref name="longitude"/> and <paramref name="altitude"/>.</para>
      /// </summary>
      /// <param name="latitude">The <see cref="Units.Angle"/> of the latitude.</param>
      /// <param name="longitude">The <see cref="Units.Angle"/> of the longitude.</param>
      /// <param name="altitude">The <see cref="Units.Length"/> of the altitude.</param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public GeographicCoordinate(Units.Angle latitude, Units.Angle longitude, Units.Length altitude)
        : this(latitude.Value, longitude.Value, altitude.Value)
      { }

      /// <summary>
      /// <para>Create a new <see cref="GeographicCoordinate"/> from the specified <paramref name="latitudeValue"/>, <paramref name="latitudeUnit"/>, <paramref name="longitudeValue"/>, <paramref name="longitudeUnit"/>, <paramref name="altitudeValue"/> and <paramref name="altitudeUnit"/>.</para>
      /// </summary>
      /// <param name="latitudeValue">The angle of the latitude.</param>
      /// <param name="latitudeUnit">The <see cref="Units.AngleUnit"/> of the latitude.</param>
      /// <param name="longitudeValue">The angle of the longitude.</param>
      /// <param name="latitudeUnit">The <see cref="Units.AngleUnit"/> of the longitude.</param>
      /// <param name="altitudeValue">The length of the altitude.</param>
      /// <param name="altitudeUnit">The <see cref="Units.Length"/> of the altitude.</param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public GeographicCoordinate(double latitudeValue, Units.AngleUnit latitudeUnit, double longitudeValue, Units.AngleUnit longitudeUnit, double altitudeValue = 1, Units.LengthUnit altitudeUnit = Units.LengthUnit.Meter)
        : this(Units.Angle.ConvertFromUnit(latitudeUnit, latitudeValue), Units.Angle.ConvertFromUnit(longitudeUnit, longitudeValue), Units.Length.ConvertFromUnit(altitudeUnit, altitudeValue))
      { }

      /// <summary>
      /// <para>Deconstructs the <see cref="GeographicCoordinate"/> to <paramref name="latitudeRadian"/>itude (radians), <paramref name="longitudeRadian"/>gitude (radians) and <paramref name="altitudeMeter"/>itude (meters).</para>
      /// </summary>
      /// <param name="latitudeRadian">The latitude in radians.</param>
      /// <param name="longitudeRadian">The longitude in radians.</param>
      /// <param name="altitudeMeter">The altitude in meters.</param>
      public void Deconstruct(out double latitudeRadian, out double longitudeRadian, out double altitudeMeter)
      {
        latitudeRadian = m_latitude;
        longitudeRadian = m_longitude;
        altitudeMeter = m_altitude;
      }

      /// <summary>The geocentric altitude of the geographic position in meters.</summary>
      public Units.Length Altitude { get => new(m_altitude); init => m_altitude = value.Value; }

      /// <summary>The latitude component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
      public Units.Latitude Latitude { get => new(m_latitude, Units.AngleUnit.Radian); init => m_latitude = value.Value; }

      /// <summary>The longitude component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
      public Units.Longitude Longitude { get => new(m_longitude, Units.AngleUnit.Radian); init => m_longitude = value.Value; }

      /// <summary>The diametrical opposite of the <see cref="GeographicCoordinate"/>, i.e. the opposite side of Earth's surface. This is a plain mathematical antipode.</summary>
      public GeographicCoordinate Antipode
        => new(
          -m_latitude,
          m_longitude - double.Pi,
          m_altitude
        );

      /// <summary>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="GeographicCoordinate"/>.</summary>
      public CoordinateSystems.SphericalCoordinate ToSphericalCoordinate()
      // Translates the geographic coordinate to spherical coordinate transparently. I cannot recall the reason for the System.Math.PI involvement (see remarks).
      {
        var (lat, lon, alt) = this;

        return new(
          alt,
          lat + (double.Pi / 2), // Add 90 degrees to convert from [-90..+90] (elevation, lat/lon) to [+0..+180] (inclination).
          lon
        );
      }

      #region Static members

      public static GeographicCoordinate CreateRandom(System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        return new(
          rng.NextDouble(double.Pi),
          rng.NextDouble(double.Tau),
          rng.NextDouble(MinAltitudeInMeters, MaxAltitudeInMeters)
        );
      }

      ///// <summary>Return the <see cref="IGeographicCoordinate"/> from the specified components.</summary>
      //static GeographicCoordinate FromUnits(Angle latitude, Angle longitude, Units.Length altitude)
      //  => new GeographicCoordinate(
      //    new Units.Latitude() { Angle = latitude },
      //    new Units.Longitude() { Angle = longitude },
      //    altitude
      //  );

      /// <summary>The along-track distance, from the start point to the closest point on the path (lat/lon 1 to lat/lon 3) to the second point.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The query latitude in radians.</param>
      /// <param name="lon2">The query longitude in radians.</param>
      /// <param name="lat3">The target latitude in radians.</param>
      /// <param name="lon3">The target longitude in radians.</param>
      /// <param name="crossTrackCentralAngle">The query longitude in radians.</param>
      /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
      public static double GetAlongTrackCentralAngle(double lat1, double lon1, double lat2, double lon2, double lat3, double lon3, out double crossTrackCentralAngle)
      {
        crossTrackCentralAngle = GetCrossTrackCentralAngle(lat1, lon1, lat2, lon2, lat3, lon3, out var trackCentralAngle13);

        return double.Acos(double.Cos(trackCentralAngle13) / double.Cos(crossTrackCentralAngle));
      }

      /// <summary>Returns a bounding box for the specified lat/lon (both in radians) and box radius.</summary>
      public static bool GetBoundingBox(double lat, double lon, double metersBoxRadius, out double latMin, out double lonMin, out double latMax, out double lonMax, double equatorialRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusEquatorial)
      {
        metersBoxRadius = double.Max(metersBoxRadius, 1);

        var angularDistance = metersBoxRadius / equatorialRadius;

        var longitudeDelta = double.Asin(double.Sin(angularDistance) / double.Cos(lat));

        latMin = lat - angularDistance;
        latMax = lat + angularDistance;

        if (latMin <= -(double.Pi / 2) || latMax >= (double.Pi / 2)) // A pole is within the given distance.
        {
          latMin = double.Max(latMin, -(double.Pi / 2));
          lonMin = -double.Pi;
          latMax = double.Min(latMax, (double.Pi / 2));
          lonMax = double.Pi;

          return false;
        }

        lonMin = lon - longitudeDelta;
        lonMax = lon + longitudeDelta;

        if (lonMin < -double.Pi)
          lonMin += double.Tau;
        if (lonMax > double.Pi)
          lonMax -= double.Tau;

        return true;
      }

      /// <summary>
      /// <para>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</para>
      /// <para>Haversine computes great‑circle distance on a sphere. This introduces errors up to 0.5% depending on latitude and distance.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Haversine_formula"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Central_angle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Great-circle_distance"/></para>
      /// </summary>
      /// <remarks>
      /// <para>The haversine formula is numerically better-conditioned for small distances. Although this formula is accurate for most distances on a sphere, it too suffers from rounding errors for the special (and somewhat unusual) case of antipodal points (on opposite ends of the sphere).</para>
      /// <para>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</para>
      /// </remarks>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <returns>The central angle in radians.</returns>
      public static double GetCentralAngleHaversineFormula(double lat1, double lon1, double lat2, double lon2)
      {
        var dLat = lat2 - lat1;
        var dLon = lon2 - lon1;

        var a = double.Hvsin(dLat) + double.Cos(lat1) * double.Cos(lat2) * double.Hvsin(dLon);

        var c = 2 * double.Asin(double.Sqrt(a));

        return c; // central angle in radians
      }

      ///// <summary>
      ///// <para>This is the real Vincenty inverse algorithm, not a spherical approximation.</para>
      ///// <para>Vincenty computes geodesics on an ellipsoid and is accurate to ≈0.1 mm under normal conditions</para>
      ///// </summary>
      ///// <param name="lat1">The source latitude in radians.</param>
      ///// <param name="lon1">The source longitude in radians.</param>
      ///// <param name="lat2">The target latitude in radians.</param>
      ///// <param name="lon2">The target longitude in radians.</param>
      ///// <returns>The central angle in radians.</returns>
      //public static double GetCentralAngleVincentyAlgorithm(double lat1, double lon1, double lat2, double lon2)
      //{
      //  // WGS‑84 ellipsoid parameters
      //  const double a = 6378137.0;
      //  const double f = 1 / 298.257223563;
      //  const double b = (1 - f) * a;

      //  // Inputs are already in radians
      //  var φ1 = lat1;
      //  var φ2 = lat2;
      //  var L = lon2 - lon1;

      //  var U1 = double.Atan((1 - f) * double.Tan(φ1));
      //  var U2 = double.Atan((1 - f) * double.Tan(φ2));

      //  var (sinU1, cosU1) = double.SinCos(U1);
      //  var (sinU2, cosU2) = double.SinCos(U2);

      //  var λ = L;
      //  double λPrev;
      //  const int maxIter = 200;

      //  int iter = 0;
      //  double σ;

      //  do
      //  {
      //    var (sinλ, cosλ) = double.SinCos(λ);

      //    var t1 = cosU2 * sinλ;
      //    var t2 = cosU1 * sinU2 - sinU1 * cosU2 * cosλ;

      //    var sinσ = double.Sqrt(t1 * t1 + t2 * t2);
      //    if (sinσ < 1e-15)
      //      return 0; // coincident points

      //    var cosσ = sinU1 * sinU2 + cosU1 * cosU2 * cosλ;
      //    σ = double.Atan2(sinσ, cosσ);

      //    var sinα = cosU1 * cosU2 * sinλ / sinσ;

      //    var cosSqα = 1 - sinα * sinα;
      //    var cos2σm = (cosSqα == 0) ? 0 : cosσ - 2 * sinU1 * sinU2 / cosSqα;

      //    var C = f / 16 * cosSqα * (4 + f * (4 - 3 * cosSqα));

      //    λPrev = λ;
      //    λ = L + (1 - C) * f * sinα * (σ + C * sinσ * (cos2σm + C * cosσ * (-1 + 2 * cos2σm * cos2σm)));

      //  } while (double.Abs(λ - λPrev) > 1e-12 && ++iter < maxIter);

      //  if (iter >= maxIter)
      //    return double.NaN; // failed to converge

      //  return σ; // central angle in radians
      //}

      ///// <summary>
      ///// <para>Vincenty on a sphere (more accurate than Haversine). This is a non‑iterative spherical version of Vincenty’s atan2 formulation.</para>
      ///// </summary>
      ///// <param name="lat1"></param>
      ///// <param name="lon1"></param>
      ///// <param name="lat2"></param>
      ///// <param name="lon2"></param>
      ///// <returns></returns>
      //public static double GetCentralAngleVincentySphere(double lat1, double lon1, double lat2, double lon2)
      //{
      //  double dLon = lon2 - lon1;

      //  var (sinLonD, cosLonD) = double.SinCos(dLon);

      //  var (sinLat1, cosLat1) = double.SinCos(lat1);
      //  var (sinLat2, cosLat2) = double.SinCos(lat2);

      //  double x = cosLat2 * sinLonD;
      //  double y = cosLat1 * sinLat2 - sinLat1 * cosLat2 * cosLonD;
      //  double z = sinLat1 * sinLat2 + cosLat1 * cosLat2 * cosLonD;

      //  return double.Atan2(double.Sqrt(x * x + y * y), z);
      //}

      /// <summary>The distance of a point from a great-circle path (sometimes called cross track error). The sign of the result tells which side of the path (lat/lon 1 to lat/lon 3) the second point is on.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The query latitude in radians.</param>
      /// <param name="lon2">The query longitude in radians.</param>
      /// <param name="lat3">The target latitude in radians.</param>
      /// <param name="lon3">The target longitude in radians.</param>
      /// <param name="trackCentralAngle13">The query longitude in radians.</param>
      /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
      public static double GetCrossTrackCentralAngle(double lat1, double lon1, double lat2, double lon2, double lat3, double lon3, out double trackCentralAngle13)
      {
        trackCentralAngle13 = GetCentralAngleHaversineFormula(lat1, lon1, lat3, lon3);

        var course13 = GetInitialBearing(lat1, lon1, lat3, lon3);
        var course12 = GetInitialBearing(lat1, lon1, lat2, lon2);

        return double.Asin(double.Sin(trackCentralAngle13) * double.Sin(course13 - course12));
      }

      /// <summary>Given a start point, initial bearing, and angularDistance, this will calculate the destination point and final bearing travelling along a (shortest distance) great circle arc.</summary>
      /// <remarks>The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</remarks>
      /// <param name="lat">The latitude in radians.</param>
      /// <param name="lon">The longitude in radians.</param>
      /// <param name="brg">Bearing is the direction or course in radians.</param>
      /// <param name="angularDistance">The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static void GetDestinationPoint(double lat, double lon, double brg, double angularDistance, out double latOut, out double lonOut)
      {
        var (sinLat, cosLat) = double.SinCos(lat);

        var (sinAd, cosAd) = double.SinCos(angularDistance);

        var (sinBrg, cosBrg) = double.SinCos(brg);

        latOut = double.Asin(sinLat * cosAd + cosLat * sinAd * cosBrg);
        lonOut = lon + double.Atan2(sinBrg * sinAd * cosLat, cosAd - sinLat * sinLat);
      }

      /// <summary>
      /// <para>Computes the distance between the two lat/lon coordinates in whatever the unit is specified for Earths radius. I.e. if the distance in meters is desired then specify: <c><paramref name="earthsRadius"/> = 6371008.8</c></para>
      /// <para>Haversine computes great‑circle distance on a sphere. This introduces errors up to 0.5% depending on latitude and distance.</para>
      /// </summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <param name="earthsRadius">Specify Earth's radius in the unit desired in order to return the distance in that unit, e.g. meters = 6371008.8, kilometers = 6371.0088, for (US) miles: 3958.76133, etc.</param>
      /// <returns>The distance between the two points, in the same units as the specified Earth's radius.</returns>
      public static double GetDistanceHaversineFormula(double lat1, double lon1, double lat2, double lon2, double earthsRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusMean)
        => earthsRadius * GetCentralAngleHaversineFormula(lat1, lon1, lat2, lon2);

      /// <summary>
      /// <para>Vincenty distance implementation (inverse formula) in C#, using the WGS‑84 ellipsoid and proper λ‑iteration. This is the real Vincenty algorithm — not the spherical approximation. It returns the geodesic distance in meters and handles nearly all edge cases.</para>
      /// <para>Vincenty computes geodesics on an ellipsoid and is accurate to ≈0.1 mm under normal conditions</para>
      /// </summary>
      /// <param name="lat1"></param>
      /// <param name="lon1"></param>
      /// <param name="lat2"></param>
      /// <param name="lon2"></param>
      /// <returns>The distance between the two points, in meters.</returns>
      public static double GetDistanceVincentyAlgorithm(double lat1, double lon1, double lat2, double lon2)
      {
        // WGS‑84 ellipsoid parameters
        const double a = 6378137.0;              // major axis
        const double f = 1 / 298.257223563;      // flattening
        const double b = (1 - f) * a;            // minor axis

        // Convert degrees → radians
        var φ1 = lat1;
        var φ2 = lat2;
        var L = lon2 - lon1;

        var U1 = double.Atan((1 - f) * double.Tan(φ1));
        var U2 = double.Atan((1 - f) * double.Tan(φ2));

        var (sinU1, cosU1) = double.SinCos(U1);
        var (sinU2, cosU2) = double.SinCos(U2);

        double λ = L;
        double λPrev;

        const int maxIter = 200;

        var iter = 0;
        double sinλ, cosλ, sinσ, cosσ, σ, sinα, cosSqα, cos2σm, C;

        do
        {
          (sinλ, cosλ) = double.SinCos(λ);

          sinσ = double.Sqrt(
              double.Pow(cosU2 * sinλ, 2) +
              double.Pow(cosU1 * sinU2 - sinU1 * cosU2 * cosλ, 2)
          );

          if (sinσ == 0)
            return 0; // coincident points

          cosσ = sinU1 * sinU2 + cosU1 * cosU2 * cosλ;
          σ = double.Atan2(sinσ, cosσ);

          sinα = cosU1 * cosU2 * sinλ / sinσ;
          cosSqα = 1 - sinα * sinα;

          cos2σm = (cosSqα == 0) ? 0 : cosσ - 2 * sinU1 * sinU2 / cosSqα;

          C = f / 16 * cosSqα * (4 + f * (4 - 3 * cosSqα));

          λPrev = λ;
          λ = L + (1 - C) * f * sinα *
              (σ + C * sinσ * (cos2σm + C * cosσ * (-1 + 2 * cos2σm * cos2σm)));

        }
        while (double.Abs(λ - λPrev) > 1e-12 && ++iter < maxIter);

        if (iter >= maxIter)
          return double.NaN; // formula failed to converge

        var uSq = cosSqα * (a * a - b * b) / (b * b);

        var A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));

        var B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));

        var Δσ = B * sinσ *
            (cos2σm + B / 4 *
            (cosσ * (-1 + 2 * cos2σm * cos2σm) -
             B / 6 * cos2σm * (-3 + 4 * sinσ * sinσ) *
             (-3 + 4 * cos2σm * cos2σm)));

        var s = b * A * (σ + Δσ); // distance in meters

        return s;
      }

      /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
      public static double GetFinalBearing(double lat1, double lon1, double lat2, double lon2)
        => (GetInitialBearing(lat2, lon2, lat1, lon1) + double.Pi) % double.Tau;

      /// <summary>This is the halfway point along a great circle path between the two points.</summary>
      /// <param name="lat1">The first path latitude in radians.</param>
      /// <param name="lon1">The first path longitude in radians.</param>
      /// <param name="lat2">The second path latitude in radians.</param>
      /// <param name="lon2">The second path longitude in radians.</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static void GetHalfwayPoint(double lat1, double lon1, double lat2, double lon2, out double latOut, out double lonOut)
      {
        var (sinLonD, cosLonD) = double.SinCos(lon2 - lon1);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var Bx = cosLat2 * cosLonD;
        var By = cosLat2 * sinLonD;

        latOut = double.Atan2(sinLat1 + sinLat2, double.Sqrt(double.Pow(cosLat1 + Bx, 2) + By * By));
        lonOut = lon1 + double.Atan2(By, cosLat1 + Bx);
      }

      /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
      public static double GetInitialBearing(double lat1, double lon1, double lat2, double lon2)
      {
        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var (sinLonD, cosLonD) = double.SinCos(lon2 - lon1);

        var y = sinLonD * cosLat2;
        var x = cosLat1 * sinLat2 - sinLat1 * cosLat2 * cosLonD;

        var initialBearing = (double.Atan2(y, x) + double.Tau) % double.Tau; // Atan2 returns values in the range [-π, +π] radians (i.e. -180 - +180 degrees), shift to [0, 2PI] radians (i.e. 0 - 360 degrees).

        return initialBearing;
      }

      //public static (double InitialBearing, double FinalBearing) GetBearings(double lat1, double lon1, double lat2, double lon2)
      //{
      //  var (sinLat1, cosLat1) = double.SinCos(lat1);
      //  var (sinLat2, cosLat2) = double.SinCos(lat2);

      //  var (sinLonD, cosLonD) = double.SinCos(lon2 - lon1);

      //  var y = sinLonD * cosLat2;
      //  var x = cosLat1 * sinLat2 - sinLat1 * cosLat2 * cosLonD;

      //  var initialBearing = (double.Atan2(y, x) + double.Tau) % double.Tau; // Atan2 returns values in the range [-π, +π] radians (i.e. -180 - +180 degrees), shift to [0, 2PI] radians (i.e. 0 - 360 degrees).

      //  var finalBearing = (initialBearing + double.Pi) % double.Tau;

      //  return (initialBearing, finalBearing);
      //}

      /// <summary>An intermediate point at any fraction along the great circle path between two points can also be calculated.</summary>
      /// <param name="lat1">The source latitude in radians.</param>
      /// <param name="lon1">The source longitude in radians.</param>
      /// <param name="lat2">The target latitude in radians.</param>
      /// <param name="lon2">The target longitude in radians.</param>
      /// <param name="mu">Unit interval is a fraction along great circle route (0=Latitude1,Longitude1, 1=Latitude2,Longitude2)</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static void GetIntermediaryPoint(double lat1, double lon1, double lat2, double lon2, double mu, out double latOut, out double lonOut)
      {
        var centralAngle = GetCentralAngleHaversineFormula(lat1, lon1, lat2, lon2);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var sinCentralAngle = double.Sin(centralAngle);

        var a = double.Sin((1.0 - mu) * centralAngle) / sinCentralAngle;
        var b = double.Sin(mu * centralAngle) / sinCentralAngle;

        var (sinLon1, cosLon1) = double.SinCos(lon1);
        var (sinLon2, cosLon2) = double.SinCos(lon2);

        var x = a * cosLat1 * cosLon1 + b * cosLat2 * cosLon2;
        var y = a * cosLat1 * sinLon1 + b * cosLat2 * sinLon2;
        var z = a * sinLat1 + b * sinLat2;

        latOut = double.Atan2(z, double.Sqrt(x * x + y * y));
        lonOut = double.Atan2(y, x);
      }

      //// https://en.wikipedia.org/wiki/Great-circle_navigation
      //public static void WikiCourse(double latitude1, double longitude1, double latitude2, double longitude2)
      //{
      //  var cosLat1 = System.Math.Cos(latitude1);
      //  var cosLat2 = System.Math.Cos(latitude2);
      //  var sinLat1 = System.Math.Sin(latitude1);
      //  var sinLat2 = System.Math.Sin(latitude2);

      //  var lonD = longitude2 - longitude1;

      //  var cosLonD = System.Math.Cos(lonD);
      //  var sinLonD = System.Math.Sin(lonD);

      //  // initialCourse from a to b
      //  var a1 = (Quantity.Angle)System.Math.Atan2(cosLat2 * sinLonD, cosLat1 * sinLat2 - sinLat1 * cosLat2 * cosLonD);
      //  var a1b = (Quantity.Angle)InitialCourse(latitude1, longitude1, latitude2, longitude2);
      //  // finalCourse from a to b
      //  var a2 = (Quantity.Angle)System.Math.Atan2(cosLat1 * sinLonD, -cosLat2 * sinLat1 + sinLat2 * cosLat1 * cosLonD);
      //  var a2b = (Quantity.Angle)FinalCourse(latitude1, longitude1, latitude2, longitude2);

      //  // centralAngle between a and b
      //  var q12 = (Quantity.Angle)System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(cosLat1 * sinLat2 - sinLat1 * cosLat2 * cosLonD, 2) + System.Math.Pow(cosLat2 * sinLonD, 2)), sinLat1 * sinLat2 + cosLat1 * cosLat2 * cosLonD);
      //  var q12b = (Quantity.Angle)CentralAngleVincentyFormula(latitude1, longitude1, latitude2, longitude2);

      //  //      var s12 = Earth.MeanRadiusRadius * q12.Value;

      //  var sinA1 = System.Math.Sin(a1.Value);

      //  var a0 = (Quantity.Angle)System.Math.Atan((sinA1 * cosLat1) / System.Math.Sqrt(System.Math.Pow(System.Math.Cos(a1.Value), 2) + System.Math.Pow(sinA1, 2) * System.Math.Pow(sinLat1, 2)));
      //  var a0b = (Quantity.Angle)ComputeAzimuthAtExtrapolatedEquatorialCrossing(latitude1, longitude1, latitude2, longitude2);

      //  var q01 = (Quantity.Angle)System.Math.Atan2(System.Math.Tan(latitude1), System.Math.Cos(a1.Value));

      //  var q02 = (Quantity.Angle)(q01.Value + q12.Value);

      //  var y01 = (Quantity.Angle)System.Math.Atan2((System.Math.Sin(a0b.Value) * System.Math.Sin(q01.Value)), System.Math.Cos(q01.Value));

      //  var y0 = (Quantity.Angle)(longitude1 - y01.Value);

      //  var y12 = (Quantity.Angle)System.Math.Atan2(System.Math.Sin(q12.Value) * System.Math.Sin(a1.Value), cosLat1 * System.Math.Cos(q12.Value) - sinLat1 * System.Math.Sin(q12.Value) * System.Math.Cos(a1.Value));
      //  // var a = System.Math.Atan2(a0, System.Math.Cos(q))

      //  var q = (q01.Value + q02.Value) / 2;
      //  var qv = (Quantity.Angle)q;

      //  var lat = (Quantity.Angle)System.Math.Atan2(System.Math.Cos(a0b.Value) * System.Math.Sin(q), System.Math.Sqrt(System.Math.Pow(System.Math.Cos(q), 2) + System.Math.Pow(System.Math.Sin(a0b.Value), 2) * System.Math.Pow(System.Math.Sin(q), 2)));

      //  var lon = (Quantity.Angle)System.Math.Atan2(System.Math.Sin(a0b.Value) * System.Math.Sin(q), System.Math.Cos(q)) + y0;

      //  var az = (Quantity.Angle)System.Math.Atan2(System.Math.Tan(a0b.Value), System.Math.Cos(q));

      //}

      /// <summary></summary>
      /// <param name="lat1">The first path latitude in radians.</param>
      /// <param name="lon1">The first path longitude in radians.</param>
      /// <param name="brg1">The first path bearing in radians.</param>
      /// <param name="lat2">The second path latitude in radians.</param>
      /// <param name="lon2">The second path longitude in radians.</param>
      /// <param name="brg2">The second path bearing in radians.</param>
      /// <param name="latOut">The resulting latitude in radians.</param>
      /// <param name="lonOut">The resulting longitude in radians.</param>
      public static void GetIntersectionOfPaths(double lat1, double lon1, double brg1, double lat2, double lon2, double brg2, out double latOut, out double lonOut)
      {
        var latD = lat2 - lat1;
        var lonD = lon2 - lon1;
        var sinLonD = double.Sin(lonD);

        var (sinLat1, cosLat1) = double.SinCos(lat1);
        var (sinLat2, cosLat2) = double.SinCos(lat2);

        var d12 = 2 * double.Asin(double.Sqrt(double.Pow(double.Sin(latD / 2), 2) + cosLat1 * cosLat2 * double.Pow(double.Sin(lonD / 2), 2)));
        var (sind12, cosd12) = double.SinCos(d12);

        var φ1 = double.Acos(sinLat2 - sinLat1 * cosd12 / sind12 * cosLat1);
        var φ2 = double.Acos(sinLat1 - sinLat2 * cosd12 / sind12 * cosLat2);

        var bearing12 = sinLonD > 0 ? φ1 : double.Tau - φ1;
        var bearing21 = sinLonD > 0 ? double.Tau - φ2 : φ2;

        var α1 = (brg1 - bearing12 + double.Pi) % double.Tau - double.Pi;
        var (α1sin, α1cos) = double.SinCos(α1);

        var α2 = (bearing21 - brg2 + double.Pi) % double.Tau - double.Pi;
        var (α2sin, α2cos) = double.SinCos(α2);

        var α3 = double.Acos(-α1cos * α2cos + α1sin * α2sin * cosd12);

        var d13 = double.Atan2(sind12 * α1sin * α2sin, α2cos + α1cos * double.Cos(α3));
        var (d13sin, d13cos) = double.SinCos(d13);

        latOut = double.Asin(sinLat1 * d13cos + cosLat1 * d13sin * double.Cos(brg1));

        var dLon13 = double.Atan2(double.Sin(brg1) * d13sin * cosLat1, d13cos - sinLat1 * double.Sin(latOut));

        lonOut = (lon1 + dLon13 + double.Pi) % double.Tau - double.Pi;
      }

      /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
      /// <param name="lat">The latitude in radians.</param>
      /// <param name="brg">The bearing in radians.</param>
      public static double GetMaximumLatitude(double lat, double brg)
        => double.Acos(double.Abs(double.Sin(brg) * double.Cos(lat)));

      public static double GetRadiusOfCurvature(double lat, double equatorialRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusEquatorial, double polarRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusPolar)
      {
        var oneMinusEccentricitySquared = 1 - double.Pow(double.Sqrt(1 - (polarRadius * polarRadius) / (equatorialRadius * equatorialRadius)), 2);

        var sinLat = double.Sin(lat);

        return equatorialRadius * oneMinusEccentricitySquared / double.Pow(oneMinusEccentricitySquared * sinLat * sinLat, 1.5); // 1.5 = (3 / 2)
      }

      /// <summary>Try parsing the specified latitude and longitude into a Geoposition.</summary>
      public static bool TryParse(string latitudeDms, string longitudeDms, out GeographicCoordinate result, double earthRadius = PlanetaryScience.ReferenceEllipsoid.EarthRadiusMean)
      {
        try
        {
          if (Units.Angle.TryParseDmsNotations(latitudeDms, out var latitudes) && latitudes.Single(e => e is Units.Latitude) is var latitudeAngle && Units.Angle.TryParseDmsNotations(longitudeDms, out var longitudes) && longitudes.Single(e => e is Units.Longitude) is var longitudeAngle)
          {
            result = new GeographicCoordinate(latitudeAngle.Value, Units.AngleUnit.Radian, longitudeAngle.Value, Units.AngleUnit.Radian, earthRadius);
            return true;
          }
        }
        catch { }

        result = default;
        return false;
      }

      #endregion Static members

      #region Implemented interfaces
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => $"{Latitude.ToDmsNotationString()} {Longitude.ToDmsNotationString()} {Altitude.ToUnitString(Units.LengthUnit.Meter, format ?? "0.#", formatProvider)}";

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
