namespace Flux
{
  /// <summary>Represents a geographic position, using latitude, longitude and altitude.</summary>
  /// <seealso cref="http://www.edwilliams.org/avform.htm"/>
  /// <seealso cref="http://www.movable-type.co.uk/scripts/latlong.html"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct GeographicCoordinate
    : System.IEquatable<GeographicCoordinate>
  {
    public const double MaxAltitudeInMeters = 1500000000;
    public const double MinAltitudeInMeters = -11000;

    public static readonly GeographicCoordinate Empty;

    /// <summary>This is a reference coordinate for Madrid, in Spain on Europe, which is antipodal to Takapau, in New Zeeland.</summary>
    public static GeographicCoordinate MadridSpain
      => new(40.416667, -3.716667, 820);
    /// <summary>This is a reference coordinate for Takapau, in New Zeeland, which is antipodal to Madrid, in Spain on Europe.</summary>
    public static GeographicCoordinate TakapauNewZealand
      => new(-40.033333, 176.35, 221);

    /// <summary>This is a reference point for Phoenix, Arizona, USA, from where the C# version of this library originated.</summary>
    public static GeographicCoordinate PhoenixAzUsa
      => new(33.448333, -112.073889, 331);
    /// <summary>This is a reference point for Tucson, Arizona, USA, from where the C# version of this library originated.</summary>
    public static GeographicCoordinate TucsonAzUsa
      => new(32.221667, -110.926389, 728);

    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    public readonly double m_altitude;
    /// <summary>The latitude component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    public readonly double m_radLatitude;
    /// <summary>The longitude component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    public readonly double m_radLongitude;

    public GeographicCoordinate(double degLatitude, double degLongitude, double meterAltitude = 1.0)
    {
      m_altitude = ValidAltitude(meterAltitude) ? meterAltitude : throw new System.ArgumentOutOfRangeException(nameof(meterAltitude));
      m_radLatitude = Angle.ConvertDegreeToRadian(degLatitude);
      m_radLongitude = Angle.ConvertDegreeToRadian(degLongitude);
    }

    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    [System.Diagnostics.Contracts.Pure] public Length Altitude { get => new(m_altitude); init => m_altitude = value.Value; }
    /// <summary>The latitude component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    [System.Diagnostics.Contracts.Pure] public Latitude Latitude { get => new(Angle.ConvertRadianToDegree(m_radLatitude)); init => m_radLatitude = value.InRadians; }
    /// <summary>The longitude component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    [System.Diagnostics.Contracts.Pure] public Longitude Longitude { get => new(Angle.ConvertRadianToDegree(m_radLongitude)); init => m_radLongitude = value.InRadians; }

    /// <summary>Creates a new <see cref="CartesianCoordinate3R"/> Equal Earth projected X, Y coordinate with the Z component containing the altitude.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R ToEqualEarthProjection()
    {
      const double A1 = 1.340264;
      const double A2 = -0.081106;
      const double A3 = 0.000893;
      const double A4 = 0.003796;
      const double A23 = A2 * 3;
      const double A37 = A3 * 7;
      const double A49 = A4 * 9;

      var lat = m_radLatitude;
      var lon = m_radLongitude;

      var M = System.Math.Sqrt(3) / 2;
      var p = System.Math.Asin(M * System.Math.Sin(lat)); // parametric latitude
      var p2 = System.Math.Pow(p, 2);
      var p6 = System.Math.Pow(p, 6);
      var x = lon * System.Math.Cos(p) / (M * (A1 + A23 * p2 + p6 * (A37 + A49 * p2)));
      var y = p * (A1 + A2 * p2 + p6 * (A3 + A4 * p2));

      return new CartesianCoordinate3R(x, y, m_altitude);
    }
    //=> (CartesianCoordinate3)ConvertToEqualEarthProjection(Latitude.Radian, Longitude.Radian, Altitude.Value);
    /// <summary>Creates a new <see cref="CartesianCoordinate3"/> Natural Earth projected X, Y coordinate with the Z component containing the altitude.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R ToNaturalEarthProjection()
    {
      var lat = m_radLatitude;
      var lon = m_radLongitude;

      var latP2 = System.Math.Pow(lat, 2);
      var latP4 = latP2 * latP2;
      var latP6 = System.Math.Pow(lat, 6);
      var latP8 = latP4 * latP4;
      var latP10 = System.Math.Pow(lat, 10);
      var latP12 = latP6 * latP6;

      var x = lon * (0.870700 - 0.131979 * latP2 - 0.013791 * latP4 + 0.003971 * latP10 - 0.001529 * latP12);
      var y = lat * (1.007226 + 0.015085 * latP2 - 0.044475 * latP6 + 0.028874 * latP8 - 0.005916 * latP10);

      return new CartesianCoordinate3R(x, y, m_altitude);
    }
    /// <summary>Converts the <see cref="GeographicCoordinate"/> to a <see cref="SphericalCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public SphericalCoordinate ToSphericalCoordinate()
      => new(m_altitude, System.Math.PI - (m_radLatitude + Maths.PiOver2), m_radLongitude + System.Math.PI);
    /// <summary>Creates a new <see cref="CartesianCoordinate3R"/> Winkel Tripel projected X, Y coordinate with the Z component containing the altitude.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R ToWinkelTripelProjection()
    {
      var lat = m_radLatitude;
      var lon = m_radLongitude;

      var cosLatitude = System.Math.Cos(lat);

      var sinc = Angle.Sincu(System.Math.Acos(cosLatitude * System.Math.Cos(lon / 2)));

      var x = 0.5 * (lon * System.Math.Cos(System.Math.Acos(Maths.PiInto2)) + ((2 * cosLatitude * System.Math.Sin(lon / 2)) / sinc));
      var y = 0.5 * (lat + (System.Math.Sin(lat) / sinc));

      return new CartesianCoordinate3R(x, y, m_altitude);
    }

    ///// <summary>The distance along the specified track (from its starting point) where this position is the closest to the track.</summary>
    ///// <param name="trackStart"></param>
    ///// <param name="trackEnd"></param>
    ///// <param name="earthRadius">This can be used to control the unit of measurement for the distance, e.g. Meters.</param>
    ///// <returns>The distance from trackStart along the course towards trackEnd to a point abeam the position.</returns>
    //public double AlongTrackDistance(Geopoint trackStart, Geopoint trackEnd, double earthRadius = EarthRadii.MeanInMeters)
    //  => earthRadius * AlongTrackCentralAngle(trackStart.Latitude.Angle.Radian, trackStart.Longitude.Angle.Radian, trackEnd.Latitude.Angle.Radian, trackEnd.Longitude.Angle.Radian, Latitude.Angle.Radian, Longitude.Angle.Radian, out var _);

    ///// <summary>The shortest distance of this position from the specified track.</summary>
    ///// <remarks>The cross track error, i.e. the distance off course.</remarks>
    //public double CrossTrackError(Geopoint trackStart, Geopoint trackEnd, double earthRadius = EarthRadii.MeanInMeters)
    //  => earthRadius * CrossTrackCentralAngle(trackStart.Latitude.Angle.Radian, trackStart.Longitude.Angle.Radian, trackEnd.Latitude.Angle.Radian, trackEnd.Longitude.Angle.Radian, Latitude.Angle.Radian, Longitude.Angle.Radian, out var _);

    //// <summary>Given a start point, initial bearing, and distance in meters, this will calculate the destination point and final bearing travelling along a (shortest distance) great circle arc.</summary>
    ///// <param name="bearingDegrees"></param>
    ///// <param name="angularDistance">The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</param>
    //public Geopoint DestinationPointAt(double bearingDegrees, double angularDistance)
    //{
    //  EndPoint(Latitude.Angle.Radian, Longitude.Angle.Radian, Quantity.Angle.ConvertDegreeToRadian(bearingDegrees), angularDistance, out var lat2, out var lon2);

    //  return new Geopoint(Quantity.Angle.ConvertRadianToDegree(lat2), Maths.Wrap(Quantity.Angle.ConvertRadianToDegree(lon2), -180, +180), Height.Value);
    //}

    ///// <summary>The distance from the point to the specified target.</summary>
    ///// <param name="targetPoint"></param>
    ///// <param name="earthRadius">This can be used to control the unit of measurement for the distance, e.g. Meters.</param>
    ///// <returns></returns>
    //public double DistanceTo(Geopoint targetPoint, double earthRadius = EarthRadii.MeanInMeters)
    //  => earthRadius * CentralAngleVincentyFormula(Latitude.Angle.Radian, Longitude.Angle.Radian, targetPoint.Latitude.Angle.Radian, targetPoint.Longitude.Angle.Radian);

    //public double InitialBearingTo(Geopoint targetPoint)
    //  => Quantity.Angle.ConvertRadianToDegree(InitialBearing(Latitude.Angle.Radian, Longitude.Angle.Radian, targetPoint.Latitude.Angle.Radian, targetPoint.Longitude.Angle.Radian));

    ///// <summary>A point that is between 0.0 (at start) to 1.0 (at end) along the track.</summary>
    //public Geopoint IntermediaryPointTo(Geopoint target, double unitInterval)
    //{
    //  IntermediaryPoint(Latitude.Angle.Radian, Longitude.Angle.Radian, target.Latitude.Angle.Radian, target.Longitude.Angle.Radian, unitInterval, out var lat, out var lon);

    //  return new Geopoint(Quantity.Angle.ConvertRadianToDegree(lat), Quantity.Angle.ConvertRadianToDegree(lon), Height.Value);
    //}

    ///// <summary>The midpoint between this point and the specified target.</summary>
    //public Geopoint MidpointTo(Geopoint target)
    //{
    //  Midpoint(Latitude.Angle.Radian, Longitude.Angle.Radian, target.Latitude.Angle.Radian, target.Longitude.Angle.Radian, out var lat, out var lon);

    //  return new Geopoint(Quantity.Angle.ConvertRadianToDegree(lat), Quantity.Angle.ConvertRadianToDegree(lon), Height.Value);
    //}

    #region Static members
    /// <summary>Compass point (to given precision) for specified bearing.</summary>
    /// <remarks>Precision = max length of compass point, 1 = the four cardinal directions, 2 = ; it could be extended to 4 for quarter-winds (eg NEbN), but I think they are little used.</remarks>
    /// <param name="absoluteBearing">The direction in radians.</param>
    /// <param name="precision">4 = the four cardinal directions, 8 = the four cardinals and four intercardinal together (a.k.a. the eight principal winds) form the 8-wind compass rose, 16 = the eight principal winds and the eight half-winds together form the 16-wind compass rose, 32 = the eight principal winds, eight half-winds and sixteen quarter-winds form the 32-wind compass rose.</param>
    /// <returns></returns>
    [System.Diagnostics.Contracts.Pure]
    public static ThirtytwoWindCompassRose CompassPoint(double absoluteBearing, PointsOfTheCompass precision, out double notch)
    {
      notch = System.Math.Round(Maths.Wrap(absoluteBearing, 0, Maths.PiX2) / (Maths.PiX2 / (int)precision) % (int)precision);

      return (ThirtytwoWindCompassRose)(int)(notch * (32 / (int)precision));
    }

    /// <summary>Converts the specified Equal Earth projected X, Y coordinate components with Z optionally containing the altitude to geographical coordinate components.</summary>
    /// <param name="z">Optional altitude (in meters).</param>
    /// <see cref="https://github.com/dneuman/EqualEarth/blob/master/EqualEarth.py"/>
    [System.Diagnostics.Contracts.Pure]
    public static GeographicCoordinate FromEqualEarthProjection(double x, double y, double? z)
    {
      const double A1 = 1.340264;
      const double A2 = -0.081106;
      const double A3 = 0.000893;
      const double A4 = 0.003796;
      const double A23 = A2 * 3;
      const double A37 = A3 * 7;
      const double A49 = A4 * 9;

      var iterations = 20;
      var limit = 1e-8;
      var M = System.Math.Sqrt(3) / 2;

      var p = y; // Initial estimate for parametric latitude.
      var dp = 0.0; // No change at start.
      var dy = 0.0;

      for (var i = iterations - 1; i >= 0 && System.Math.Abs(dp) > limit; i--)
      {
        p -= dp;
        var p2 = System.Math.Pow(p, 2);
        var p6 = System.Math.Pow(p, 6);
        var fy = p * (A1 + A2 * p2 + p6 * (A3 + A4 * p2)) - y; // fy is the function you need the root of.
        dy = A1 + A23 * p2 + p6 * (A37 + A49 * p2); // dy is the derivative of the function
        dp = fy / dy; // dp is fy/dy or the change in estimate.
      }

      var lon = M * x * dy / System.Math.Cos(p);
      var lat = System.Math.Asin(System.Math.Sin(p) / M);

      return new GeographicCoordinate(Angle.ConvertRadianToDegree(lat), Angle.ConvertRadianToDegree(lon), z ?? EarthWgs84.MeanRadius.Value);
    }

    /// <summary>The along-track distance, from the start point to the closest point on the path to the third point.</summary>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double GetAlongTrackCentralAngle(double latitude1, double longitude1, double latitude2, double longitude2, double latitude3, double longitude3, out double crossTrackCentralAngle)
    {
      crossTrackCentralAngle = GetCrossTrackCentralAngle(latitude1, longitude1, latitude2, longitude2, latitude3, longitude3, out var trackCentralAngle13);

      return System.Math.Acos(System.Math.Cos(trackCentralAngle13) / System.Math.Cos(crossTrackCentralAngle));
    }

    /// <summary>Returns a bounding box for the specified lat/lon (both in radians) and box radius.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool GetBoundingBox(double radLatitude, double radLongitude, double metersBoxRadius, out double latitudeMin, out double longitudeMin, out double latitudeMax, out double longitudeMax)
    {
      metersBoxRadius = System.Math.Max(metersBoxRadius, 1);

      var angularDistance = metersBoxRadius / EarthWgs84.EquatorialRadius.Value;

      var longitudeDelta = System.Math.Asin(System.Math.Sin(angularDistance) / System.Math.Cos(radLatitude));

      latitudeMin = radLatitude - angularDistance;
      latitudeMax = radLatitude + angularDistance;

      if (latitudeMin <= -Maths.PiOver2 || latitudeMax >= Maths.PiOver2) // A pole is within the given distance.
      {
        latitudeMin = System.Math.Max(latitudeMin, -Maths.PiOver2);
        longitudeMin = -System.Math.PI;
        latitudeMax = System.Math.Min(latitudeMax, Maths.PiOver2);
        longitudeMax = System.Math.PI;

        return false;
      }

      longitudeMin = radLongitude - longitudeDelta;
      longitudeMax = radLongitude + longitudeDelta;

      if (longitudeMin < -System.Math.PI)
        longitudeMin += Maths.PiX2;
      if (longitudeMax > System.Math.PI)
        longitudeMax -= Maths.PiX2;

      return true;
    }

    /// <summary>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Haversine_formula"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Central_angle"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Great-circle_distance"/>
    /// <remarks>The haversine formula is numerically better-conditioned for small distances. Although this formula is accurate for most distances on a sphere, it too suffers from rounding errors for the special (and somewhat unusual) case of antipodal points (on opposite ends of the sphere).</remarks>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double GetCentralAngleHaversineFormula(double latitude1, double longitude1, double latitude2, double longitude2)
      => Angle.Ahvsin(Angle.Hvsin(latitude2 - latitude1) + System.Math.Cos(latitude1) * System.Math.Cos(latitude2) * Angle.Hvsin(longitude2 - longitude1));
    /// <summary>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Central_angle"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Great-circle_distance"/>
    /// <remarks>A more complicated formula that is accurate for all distances is the following special case of the Vincenty formula for an ellipsoid with equal major and minor axes.</remarks>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double GetCentralAngleVincentyFormula(double latitude1, double longitude1, double latitude2, double longitude2)
    {
      var cosLat1 = System.Math.Cos(latitude1);
      var cosLat2 = System.Math.Cos(latitude2);
      var sinLat1 = System.Math.Sin(latitude1);
      var sinLat2 = System.Math.Sin(latitude2);

      var lonD = longitude2 - longitude1;

      var cosLat2LonD = cosLat2 * System.Math.Cos(lonD);

      return System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(cosLat2 * System.Math.Sin(lonD), 2) + System.Math.Pow(cosLat1 * sinLat2 - sinLat1 * cosLat2LonD, 2)), sinLat1 * sinLat2 + cosLat1 * cosLat2LonD);
    }

    /// <summary>The distance of a point from a great-circle path (sometimes called cross track error). The sign of the result tells which side of the path the third point is on.</summary>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double GetCrossTrackCentralAngle(double latitude1, double longitude1, double latitude2, double longitude2, double latitude3, double longitude3, out double trackCentralAngle13)
    {
      trackCentralAngle13 = GetCentralAngleVincentyFormula(latitude1, longitude1, latitude3, longitude3);

      var course13 = GetInitialCourse(latitude1, longitude1, latitude3, longitude3);
      var course12 = GetInitialCourse(latitude1, longitude1, latitude2, longitude2);

      return System.Math.Asin(System.Math.Sin(trackCentralAngle13) * System.Math.Sin(course13 - course12));
    }

    /// <summary>Given a start point, initial bearing, and angularDistance, this will calculate the destination point and final bearing travelling along a (shortest distance) great circle arc.</summary>
    /// <param name="radAzimuth">Bearing is the direction or course.</param>
    /// <param name="angularDistance">The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</param>
    /// <remarks>The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static void GetDestination(double radLatitude, double radLongitude, double radAzimuth, double angularDistance, out double latitudeOut, out double longitudeOut)
    {
      var cosLat = System.Math.Cos(radLatitude);
      var sinLat = System.Math.Sin(radLatitude);

      var cosAd = System.Math.Cos(angularDistance);
      var sinAd = System.Math.Sin(angularDistance);

      latitudeOut = System.Math.Asin(sinLat * cosAd + cosLat * sinAd * System.Math.Cos(radAzimuth));
      longitudeOut = radLongitude + System.Math.Atan2(System.Math.Sin(radAzimuth) * sinAd * cosLat, cosAd - sinLat * System.Math.Sin(radLatitude));
    }

    /// <summary>Computes the distance between the two lat/lon coordinates in whatever the unit is specified for Earths radius.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2, double earthsRadius)
      => earthsRadius * GetCentralAngleVincentyFormula(latitude1, longitude1, latitude2, longitude2);

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double GetFinalCourse(double latitude1, double longitude1, double latitude2, double longitude2)
      => (GetInitialCourse(latitude2, longitude2, latitude1, longitude1) + System.Math.PI) % Maths.PiX2;

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double GetInitialCourse(double latitude1, double longitude1, double latitude2, double longitude2)
    {
      var cosLat2 = System.Math.Cos(latitude2);
      var lonD = longitude2 - longitude1;

      var y = System.Math.Sin(lonD) * cosLat2;
      var x = System.Math.Cos(latitude1) * System.Math.Sin(latitude2) - System.Math.Sin(latitude1) * cosLat2 * System.Math.Cos(lonD);

      return (System.Math.Atan2(y, x) + Maths.PiX2) % Maths.PiX2; // Atan2 returns values in the range [-π, +π] radians (i.e. -180 - +180 degrees), shift to [0, 2PI] radians (i.e. 0 - 360 degrees).
    }

    /// <summary>An intermediate point at any fraction along the great circle path between two points can also be calculated.</summary>
    /// <param name="mu">Unit interval is a fraction along great circle route (0=Latitude1,Longitude1, 1=Latitude2,Longitude2)</param>
    [System.Diagnostics.Contracts.Pure]
    public static void GetIntermediaryPoint(double latitude1, double longitude1, double latitude2, double longitude2, double mu, out double latitudeOut, out double longitudeOut)
    {
      var centralAngle = GetCentralAngleVincentyFormula(latitude1, longitude1, latitude2, longitude2);

      var a = System.Math.Sin((1.0 - mu) * centralAngle) / System.Math.Sin(centralAngle);
      var b = System.Math.Sin(mu * centralAngle) / System.Math.Sin(centralAngle);

      var cosLat1 = System.Math.Cos(latitude1);
      var cosLat2 = System.Math.Cos(latitude2);

      var x = a * cosLat1 * System.Math.Cos(longitude1) + b * cosLat2 * System.Math.Cos(longitude2);
      var y = a * cosLat1 * System.Math.Sin(longitude1) + b * cosLat2 * System.Math.Sin(longitude2);
      var z = a * System.Math.Sin(latitude1) + b * System.Math.Sin(latitude2);

      latitudeOut = System.Math.Atan2(z, System.Math.Sqrt(x * x + y * y));
      longitudeOut = System.Math.Atan2(y, x);
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

    [System.Diagnostics.Contracts.Pure]
    public static void GetIntersectionOfPaths(double latitude1, double longitude1, double bearing1, double latitude2, double longitude2, double bearing2, out double latitudeOut, out double longitudeOut)
    {
      var latD = latitude2 - latitude1;
      var lonD = longitude2 - longitude1;
      var sinlonD = System.Math.Sin(lonD);

      var cosLat1 = System.Math.Cos(latitude1);
      var cosLat2 = System.Math.Cos(latitude2);
      var sinLat1 = System.Math.Sin(latitude1);
      var sinLat2 = System.Math.Sin(latitude2);

      var d12 = 2 * System.Math.Asin(System.Math.Sqrt(System.Math.Pow(System.Math.Sin(latD / 2), 2) + cosLat1 * cosLat2 * System.Math.Pow(System.Math.Sin(lonD / 2), 2)));
      var cosd12 = System.Math.Cos(d12);
      var sind12 = System.Math.Sin(d12);

      var φ1 = System.Math.Acos(sinLat2 - sinLat1 * cosd12 / sind12 * cosLat1);
      var φ2 = System.Math.Acos(sinLat1 - sinLat2 * cosd12 / sind12 * cosLat2);

      var bearing12 = sinlonD > 0 ? φ1 : Maths.PiX2 - φ1;
      var bearing21 = sinlonD > 0 ? Maths.PiX2 - φ2 : φ2;

      var α1 = (bearing1 - bearing12 + System.Math.PI) % Maths.PiX2 - System.Math.PI;
      var α1cos = System.Math.Cos(α1);
      var α1sin = System.Math.Sin(α1);

      var α2 = (bearing21 - bearing2 + System.Math.PI) % Maths.PiX2 - System.Math.PI;
      var α2cos = System.Math.Cos(α2);
      var α2sin = System.Math.Sin(α2);

      var α3 = System.Math.Acos(-α1cos * α2cos + α1sin * α2sin * cosd12);

      var d13 = System.Math.Atan2(sind12 * α1sin * α2sin, α2cos + α1cos * System.Math.Cos(α3));
      var d13cos = System.Math.Cos(d13);
      var d13sin = System.Math.Sin(d13);

      latitudeOut = System.Math.Asin(sinLat1 * d13cos + cosLat1 * d13sin * System.Math.Cos(bearing1));

      var dLon13 = System.Math.Atan2(System.Math.Sin(bearing1) * d13sin * cosLat1, d13cos - sinLat1 * System.Math.Sin(latitudeOut));

      longitudeOut = (longitude1 + dLon13 + System.Math.PI) % Maths.PiX2 - System.Math.PI;
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double GetMaximumLatitude(double radLatitude, double radAzimuth)
      => System.Math.Acos(System.Math.Abs(System.Math.Sin(radAzimuth) * System.Math.Cos(radLatitude)));

    /// <summary>This is the halfway point along a great circle path between the two points.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static void GetMidpoint(double latitude1, double longitude1, double latitude2, double longitude2, out double latitudeOut, out double longitudeOut)
    {
      var lonD = longitude2 - longitude1;

      var cosLat1 = System.Math.Cos(latitude1);
      var cosLat2 = System.Math.Cos(latitude2);

      var Bx = cosLat2 * System.Math.Cos(lonD);
      var By = cosLat2 * System.Math.Sin(lonD);

      latitudeOut = System.Math.Atan2(System.Math.Sin(latitude1) + System.Math.Sin(latitude2), System.Math.Sqrt(System.Math.Pow(cosLat1 + Bx, 2) + By * By));
      longitudeOut = longitude1 + System.Math.Atan2(By, cosLat1 + Bx);
    }

    /// <summary>Try parsing the specified latitude and longitude into a Geoposition.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool TryParse(string latitudeDMS, string longitudeDMS, out GeographicCoordinate result, double earthRadius)
    {
      if (Angle.TryParseSexagesimalDegrees(latitudeDMS, out var latitude) && Angle.TryParseSexagesimalDegrees(longitudeDMS, out var longitude))
      {
        result = new GeographicCoordinate(latitude.ToUnitValue(AngleUnit.Degree), longitude.ToUnitValue(AngleUnit.Degree), earthRadius);
        return true;
      }

      result = Empty;
      return false;
    }

    /// <summary>Returns whether the altitude is valid on Earth.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool ValidAltitude(double altitudeInMeters)
      => altitudeInMeters >= MinAltitudeInMeters && altitudeInMeters <= MaxAltitudeInMeters;
    #endregion Static members

    #region Overloaded operators
    public static bool operator ==(GeographicCoordinate a, GeographicCoordinate b)
      => a.Equals(b);
    public static bool operator !=(GeographicCoordinate a, GeographicCoordinate b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(GeographicCoordinate other)
      => m_altitude == other.m_altitude && m_radLatitude == other.m_radLatitude && m_radLongitude == other.m_radLongitude;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is GeographicCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_altitude, m_radLatitude, m_radLongitude);
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => $"{GetType().Name} {{ Latitude = {Latitude.ToSexagesimalDegreeString()} ({Latitude.Value}), Longitude = {Longitude.ToSexagesimalDegreeString()} ({Longitude.Value}), Altitude = {Altitude.ToUnitString(LengthUnit.Meter)} }}";
    #endregion Object overrides
  }
}
