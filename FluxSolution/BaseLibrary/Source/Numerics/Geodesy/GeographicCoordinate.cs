namespace Flux.Numerics
{
  /// <summary>Represents a geographic position, using latitude, longitude and altitude.</summary>
  /// <seealso cref="http://www.edwilliams.org/avform.htm"/>
  /// <seealso cref="http://www.movable-type.co.uk/scripts/latlong.html"/>
  /// <remarks>Abbreviated angles are in radians, and full names are in degrees.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct GeographicCoordinate
    : System.IFormattable, IGeographicCoordinate<double>
  {
    public const double MaxAltitudeInMeters = 1500000000;
    public const double MinAltitudeInMeters = -11000;

    /// <summary>This is a reference coordinate for Madrid, in Spain on Europe, which is antipodal to Takapau, in New Zeeland.</summary>
    public static GeographicCoordinate MadridSpain => new(40.416667, -3.716667, 820);

    /// <summary>This is a reference coordinate for Takapau, in New Zeeland, which is antipodal to Madrid, in Spain on Europe.</summary>
    public static GeographicCoordinate TakapauNewZealand => new(-40.033333, 176.35, 221);

    /// <summary>This is a reference point for Phoenix, Arizona, USA, from where the C# version of this library originated.</summary>
    public static GeographicCoordinate PhoenixAzUsa => new(33.448333, -112.073889, 331);
    /// <summary>This is a reference point for Tucson, Arizona, USA, from where the C# version of this library originated.</summary>
    public static GeographicCoordinate TucsonAzUsa => new(32.221667, -110.926389, 728);

    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    private readonly double m_altitude;
    /// <summary>The latitude component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    private readonly double m_lat;
    /// <summary>The longitude component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    private readonly double m_lon;

    /// <summary></summary>
    /// <param name="latitude">The latitude in degrees.</param>
    /// <param name="longitude">The longitude in degrees.</param>
    /// <param name="altitude">The altitude in meters.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public GeographicCoordinate(double latitude, double longitude, double altitude = 1.0)
    {
      m_altitude = altitude >= MinAltitudeInMeters && altitude <= MaxAltitudeInMeters ? altitude : throw new System.ArgumentOutOfRangeException(nameof(altitude));
      m_lat = new Quantities.Latitude(latitude).InRadians;
      m_lon = new Quantities.Longitude(longitude).InRadians;
    }

    /// <summary></summary>
    /// <param name="altitude">The altitude in meters.</param>
    /// <param name="latitude">The latitude in degrees.</param>
    /// <param name="longitude">The longitude in degrees.</param>
    public void Deconstruct(out double altitude, out double latitude, out double longitude)
    {
      altitude = m_altitude;
      latitude = Latitude;
      longitude = Longitude;
    }

    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    public double Altitude { get => m_altitude; init => m_altitude = value; }

    /// <summary>The latitude component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    public double Latitude { get => Quantities.Angle.ConvertRadianToDegree(m_lat); init => m_lat = new Quantities.Latitude(value).InRadians; }

    public double LatitudeInRadians => m_lat;

    /// <summary>The longitude component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    public double Longitude { get => Quantities.Angle.ConvertRadianToDegree(m_lon); init => m_lon = new Quantities.Longitude(value).InRadians; }

    public double LongitudeInRadians => m_lon;

    #region Static members

    ///// <summary>Return the <see cref="IGeographicCoordinate"/> from the specified components.</summary>
    //static GeographicCoordinate From(Quantities.Length altitude, Latitude latitude, Longitude longitude)
    //  => new GeographicCoordinate(
    //    altitude.Value,
    //    latitude.Value,
    //    longitude.Value
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

      return System.Math.Acos(System.Math.Cos(trackCentralAngle13) / System.Math.Cos(crossTrackCentralAngle));
    }

    /// <summary>Returns a bounding box for the specified lat/lon (both in radians) and box radius.</summary>
    public static bool GetBoundingBox(double lat, double lon, double metersBoxRadius, out double latMin, out double lonMin, out double latMax, out double lonMax, EllipsoidReference ellipsoidReference)
    {
      metersBoxRadius = System.Math.Max(metersBoxRadius, 1);

      var angularDistance = metersBoxRadius / ellipsoidReference.EquatorialRadius.Value;

      var longitudeDelta = System.Math.Asin(System.Math.Sin(angularDistance) / System.Math.Cos(lat));

      latMin = lat - angularDistance;
      latMax = lat + angularDistance;

      if (latMin <= -GenericMath.PiOver2 || latMax >= GenericMath.PiOver2) // A pole is within the given distance.
      {
        latMin = System.Math.Max(latMin, -GenericMath.PiOver2);
        lonMin = -System.Math.PI;
        latMax = System.Math.Min(latMax, GenericMath.PiOver2);
        lonMax = System.Math.PI;

        return false;
      }

      lonMin = lon - longitudeDelta;
      lonMax = lon + longitudeDelta;

      if (lonMin < -System.Math.PI)
        lonMin += double.Tau;
      if (lonMax > System.Math.PI)
        lonMax -= double.Tau;

      return true;
    }

    /// <summary>
    /// <para>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Haversine_formula"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Central_angle"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Great-circle_distance"/></para>
    /// </summary>
    /// <param name="lat1">The source latitude in radians.</param>
    /// <param name="lon1">The source longitude in radians.</param>
    /// <param name="lat2">The target latitude in radians.</param>
    /// <param name="lon2">The target longitude in radians.</param>
    /// <remarks>
    /// <para>The haversine formula is numerically better-conditioned for small distances. Although this formula is accurate for most distances on a sphere, it too suffers from rounding errors for the special (and somewhat unusual) case of antipodal points (on opposite ends of the sphere).</para>
    /// <para>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</para>
    /// </remarks>
    public static double GetCentralAngleHaversineFormula(double lat1, double lon1, double lat2, double lon2)
      => Quantities.Angle.Ahvsin(Quantities.Angle.Hvsin(lat2 - lat1) + System.Math.Cos(lat1) * System.Math.Cos(lat2) * Quantities.Angle.Hvsin(lon2 - lon1));

    /// <summary>
    /// <para>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Central_angle"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Great-circle_distance"/></para>
    /// </summary>
    /// <param name="lat1">The source latitude in radians.</param>
    /// <param name="lon1">The source longitude in radians.</param>
    /// <param name="lat2">The target latitude in radians.</param>
    /// <param name="lon2">The target longitude in radians.</param>
    /// <remarks>
    /// <para>A more complicated formula that is accurate for all distances is the following special case of the Vincenty formula for an ellipsoid with equal major and minor axes.</para>
    /// <para>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</para>
    /// </remarks>
    public static double GetCentralAngleVincentyFormula(double lat1, double lon1, double lat2, double lon2)
    {
      var cosLat1 = System.Math.Cos(lat1);
      var cosLat2 = System.Math.Cos(lat2);
      var sinLat1 = System.Math.Sin(lat1);
      var sinLat2 = System.Math.Sin(lat2);

      var lonD = lon2 - lon1;

      var cosLat2LonD = cosLat2 * System.Math.Cos(lonD);

      return System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(cosLat2 * System.Math.Sin(lonD), 2) + System.Math.Pow(cosLat1 * sinLat2 - sinLat1 * cosLat2LonD, 2)), sinLat1 * sinLat2 + cosLat1 * cosLat2LonD);
    }

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
      trackCentralAngle13 = GetCentralAngleVincentyFormula(lat1, lon1, lat3, lon3);

      var course13 = GetInitialCourse(lat1, lon1, lat3, lon3);
      var course12 = GetInitialCourse(lat1, lon1, lat2, lon2);

      return System.Math.Asin(System.Math.Sin(trackCentralAngle13) * System.Math.Sin(course13 - course12));
    }

    /// <summary>Given a start point, initial bearing, and angularDistance, this will calculate the destination point and final bearing travelling along a (shortest distance) great circle arc.</summary>
    /// <remarks>The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</remarks>
    /// <param name="lat">The latitude in radians.</param>
    /// <param name="lon">The longitude in radians.</param>
    /// <param name="brg">Bearing is the direction or course.</param>
    /// <param name="angularDistance">The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</param>
    /// <param name="latOut">The resulting latitude in radians.</param>
    /// <param name="lonOut">The resulting longitude in radians.</param>
    public static void GetDestination(double lat, double lon, double brg, double angularDistance, out double latOut, out double lonOut)
    {
      var cosLat = System.Math.Cos(lat);
      var sinLat = System.Math.Sin(lat);

      var cosAd = System.Math.Cos(angularDistance);
      var sinAd = System.Math.Sin(angularDistance);

      latOut = System.Math.Asin(sinLat * cosAd + cosLat * sinAd * System.Math.Cos(brg));
      lonOut = lon + System.Math.Atan2(System.Math.Sin(brg) * sinAd * cosLat, cosAd - sinLat * System.Math.Sin(lat));
    }

    /// <summary>Computes the distance between the two lat/lon coordinates in whatever the unit is specified for Earths radius.</summary>
    /// <param name="lat1">The source latitude in radians.</param>
    /// <param name="lon1">The source longitude in radians.</param>
    /// <param name="lat2">The target latitude in radians.</param>
    /// <param name="lon2">The target longitude in radians.</param>
    /// <param name="earthsRadius"></param>
    /// <returns></returns>
    public static double GetDistance(double lat1, double lon1, double lat2, double lon2, double earthsRadius)
      => earthsRadius * GetCentralAngleVincentyFormula(lat1, lon1, lat2, lon2);

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    /// <param name="lat1">The source latitude in radians.</param>
    /// <param name="lon1">The source longitude in radians.</param>
    /// <param name="lat2">The target latitude in radians.</param>
    /// <param name="lon2">The target longitude in radians.</param>
    /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
    public static double GetFinalCourse(double lat1, double lon1, double lat2, double lon2)
      => (GetInitialCourse(lat2, lon2, lat1, lon1) + System.Math.PI) % double.Tau;

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    /// <param name="lat1">The source latitude in radians.</param>
    /// <param name="lon1">The source longitude in radians.</param>
    /// <param name="lat2">The target latitude in radians.</param>
    /// <param name="lon2">The target longitude in radians.</param>
    /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
    public static double GetInitialCourse(double lat1, double lon1, double lat2, double lon2)
    {
      var cosLat2 = System.Math.Cos(lat2);
      var lonD = lon2 - lon1;

      var y = System.Math.Sin(lonD) * cosLat2;
      var x = System.Math.Cos(lat1) * System.Math.Sin(lat2) - System.Math.Sin(lat1) * cosLat2 * System.Math.Cos(lonD);

      return (System.Math.Atan2(y, x) + double.Tau) % double.Tau; // Atan2 returns values in the range [-π, +π] radians (i.e. -180 - +180 degrees), shift to [0, 2PI] radians (i.e. 0 - 360 degrees).
    }

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
      var centralAngle = GetCentralAngleVincentyFormula(lat1, lon1, lat2, lon2);

      var a = System.Math.Sin((1.0 - mu) * centralAngle) / System.Math.Sin(centralAngle);
      var b = System.Math.Sin(mu * centralAngle) / System.Math.Sin(centralAngle);

      var cosLat1 = System.Math.Cos(lat1);
      var cosLat2 = System.Math.Cos(lat2);

      var x = a * cosLat1 * System.Math.Cos(lon1) + b * cosLat2 * System.Math.Cos(lon2);
      var y = a * cosLat1 * System.Math.Sin(lon1) + b * cosLat2 * System.Math.Sin(lon2);
      var z = a * System.Math.Sin(lat1) + b * System.Math.Sin(lat2);

      latOut = System.Math.Atan2(z, System.Math.Sqrt(x * x + y * y));
      lonOut = System.Math.Atan2(y, x);
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
      var sinlonD = System.Math.Sin(lonD);

      var cosLat1 = System.Math.Cos(lat1);
      var cosLat2 = System.Math.Cos(lat2);
      var sinLat1 = System.Math.Sin(lat1);
      var sinLat2 = System.Math.Sin(lat2);

      var d12 = 2 * System.Math.Asin(System.Math.Sqrt(System.Math.Pow(System.Math.Sin(latD / 2), 2) + cosLat1 * cosLat2 * System.Math.Pow(System.Math.Sin(lonD / 2), 2)));
      var cosd12 = System.Math.Cos(d12);
      var sind12 = System.Math.Sin(d12);

      var φ1 = System.Math.Acos(sinLat2 - sinLat1 * cosd12 / sind12 * cosLat1);
      var φ2 = System.Math.Acos(sinLat1 - sinLat2 * cosd12 / sind12 * cosLat2);

      var bearing12 = sinlonD > 0 ? φ1 : double.Tau - φ1;
      var bearing21 = sinlonD > 0 ? double.Tau - φ2 : φ2;

      var α1 = (brg1 - bearing12 + System.Math.PI) % double.Tau - System.Math.PI;
      var α1cos = System.Math.Cos(α1);
      var α1sin = System.Math.Sin(α1);

      var α2 = (bearing21 - brg2 + System.Math.PI) % double.Tau - System.Math.PI;
      var α2cos = System.Math.Cos(α2);
      var α2sin = System.Math.Sin(α2);

      var α3 = System.Math.Acos(-α1cos * α2cos + α1sin * α2sin * cosd12);

      var d13 = System.Math.Atan2(sind12 * α1sin * α2sin, α2cos + α1cos * System.Math.Cos(α3));
      var d13cos = System.Math.Cos(d13);
      var d13sin = System.Math.Sin(d13);

      latOut = System.Math.Asin(sinLat1 * d13cos + cosLat1 * d13sin * System.Math.Cos(brg1));

      var dLon13 = System.Math.Atan2(System.Math.Sin(brg1) * d13sin * cosLat1, d13cos - sinLat1 * System.Math.Sin(latOut));

      lonOut = (lon1 + dLon13 + System.Math.PI) % double.Tau - System.Math.PI;
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    /// <param name="lat">The latitude in radians.</param>
    /// <param name="brg">The bearing in radians.</param>
    public static double GetMaximumLatitude(double lat, double brg)
      => System.Math.Acos(System.Math.Abs(System.Math.Sin(brg) * System.Math.Cos(lat)));

    /// <summary>This is the halfway point along a great circle path between the two points.</summary>
    /// <param name="lat1">The first path latitude in radians.</param>
    /// <param name="lon1">The first path longitude in radians.</param>
    /// <param name="lat2">The second path latitude in radians.</param>
    /// <param name="lon2">The second path longitude in radians.</param>
    /// <param name="latOut">The resulting latitude in radians.</param>
    /// <param name="lonOut">The resulting longitude in radians.</param>
    public static void GetMidpoint(double lat1, double lon1, double lat2, double lon2, out double latOut, out double lonOut)
    {
      var lonD = lon2 - lon1;

      var cosLat1 = System.Math.Cos(lat1);
      var cosLat2 = System.Math.Cos(lat2);

      var Bx = cosLat2 * System.Math.Cos(lonD);
      var By = cosLat2 * System.Math.Sin(lonD);

      latOut = System.Math.Atan2(System.Math.Sin(lat1) + System.Math.Sin(lat2), System.Math.Sqrt(System.Math.Pow(cosLat1 + Bx, 2) + By * By));
      lonOut = lon1 + System.Math.Atan2(By, cosLat1 + Bx);
    }

    /// <summary>Try parsing the specified latitude and longitude into a Geoposition.</summary>
    public static bool TryParse(string latitudeDMS, string longitudeDMS, out GeographicCoordinate result, double earthRadius)
    {
      if (Quantities.Angle.TryParseSexagesimalDegrees(latitudeDMS, out var latitude) && Quantities.Angle.TryParseSexagesimalDegrees(longitudeDMS, out var longitude))
      {
        result = new GeographicCoordinate(latitude.ToUnitValue(Quantities.AngleUnit.Degree), longitude.ToUnitValue(Quantities.AngleUnit.Degree), earthRadius);
        return true;
      }

      result = default;
      return false;
    }

    #endregion Static members

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
      => $"{new Quantities.Latitude(Latitude).ToQuantityString(format)} {new Quantities.Longitude(Longitude).ToQuantityString(format)} {new Quantities.Length(Altitude).ToUnitString(Quantities.Length.DefaultUnit, "N1").ToSpanBuilder().RemoveAll(char.IsWhiteSpace).ToString()}";

    #endregion Implemented interfaces


    public override string ToString() => ToString(null, null);
  }
}
