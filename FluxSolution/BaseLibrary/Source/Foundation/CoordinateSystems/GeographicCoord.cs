namespace Flux
{
  /// <summary>Represents a geographic position, using latitude, longitude and altitude.</summary>
  /// <seealso cref="http://www.edwilliams.org/avform.htm"/>
  /// <seealso cref="http://www.movable-type.co.uk/scripts/latlong.html"/>
  public struct GeographicCoord
    : System.IEquatable<GeographicCoord>, System.IFormattable
  {
    public static readonly GeographicCoord Empty;

    public static GeographicCoord Tucson
      => new GeographicCoord(32.221667, -110.926389, 728);

    /// <summary>The height (a.k.a. altitude) of the geographic position in meters.</summary>
    public Quantity.Length Altitude { get; }
    /// <summary>The latitude component of the geographic position. Range from -90.0 (southern hemisphere) to 90.0 degrees (northern hemisphere).</summary>
    public Latitude Latitude { get; }
    /// <summary>The longitude component of the geographic position. Range from -180.0 (western half) to 180.0 degrees (eastern half).</summary>
    public Longitude Longitude { get; }

    public GeographicCoord(Latitude latitude, Longitude longitude, Quantity.Length altitude)
    {
      Altitude = altitude;
      Latitude = latitude;
      Longitude = longitude;
    }
    public GeographicCoord(double latitude, double longitude, double altitude = 1.0)
      : this(new Latitude(latitude), new Longitude(longitude), new Quantity.Length(altitude))
    { }

    public SphericalCoord ToSphericalCoord()
    {
      var (radius, inclinationRad, azimuthRad) = ConvertToSphericalCoordinate(Latitude.Radian, Longitude.Radian, Altitude.Value);

      return new SphericalCoord(radius, new Quantity.Angle(inclinationRad), new Quantity.Angle(azimuthRad));
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
    /// <summary>right-handed vector: x -> 0°E,0°N; y -> 90°E,0°N, z -> 90°N</summary>
    //public static System.Numerics.Vector3 ToVector3RH(double latitudeR, double longitudeR) => new System.Numerics.Vector3((float)System.Math.Cos(latitudeR) * (float)System.Math.Cos(longitudeR), (float)System.Math.Cos(latitudeR) * (float)System.Math.Sin(longitudeR), (float)System.Math.Sin(latitudeR));
    //public static Geoposition FromVector3RH(double x, double y, double z) => new Geoposition() { Latitude = System.Math.Atan2(z, System.Math.Sqrt(x * x + y * y)), Longitude = System.Math.Atan2(y, x) };

    /// <summary>The along-track distance, from the start point to the closest point on the path to the third point.</summary>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    public static double AlongTrackCentralAngle(double latitude1, double longitude1, double latitude2, double longitude2, double latitude3, double longitude3, out double crossTrackCentralAngle)
    {
      crossTrackCentralAngle = CrossTrackCentralAngle(latitude1, longitude1, latitude2, longitude2, latitude3, longitude3, out var trackCentralAngle13);

      return System.Math.Acos(System.Math.Cos(trackCentralAngle13) / System.Math.Cos(crossTrackCentralAngle));
    }

    /// <summary>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Haversine_formula"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Central_angle"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Great-circle_distance"/>
    /// <remarks>The haversine formula is numerically better-conditioned for small distances. Although this formula is accurate for most distances on a sphere, it too suffers from rounding errors for the special (and somewhat unusual) case of antipodal points (on opposite ends of the sphere).</remarks>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    public static double CentralAngleHaversineFormula(double latitude1, double longitude1, double latitude2, double longitude2)
      => Maths.Ahvsin(Maths.Hvsin(latitude2 - latitude1) + System.Math.Cos(latitude1) * System.Math.Cos(latitude2) * Maths.Hvsin(longitude2 - longitude1));

    /// <summary>The shortest distance between two points on the surface of a sphere, measured along the surface of the sphere (as opposed to a straight line through the sphere's interior). Multiply by unit radius, e.g. 6371 km or 3959 mi.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Central_angle"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Great-circle_distance"/>
    /// <remarks>A more complicated formula that is accurate for all distances is the following special case of the Vincenty formula for an ellipsoid with equal major and minor axes.</remarks>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    public static double CentralAngleVincentyFormula(double latitude1, double longitude1, double latitude2, double longitude2)
    {
      var cosLat1 = System.Math.Cos(latitude1);
      var cosLat2 = System.Math.Cos(latitude2);
      var sinLat1 = System.Math.Sin(latitude1);
      var sinLat2 = System.Math.Sin(latitude2);

      var lonD = longitude2 - longitude1;

      var cosLat2LonD = cosLat2 * System.Math.Cos(lonD);

      return System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(cosLat2 * System.Math.Sin(lonD), 2) + System.Math.Pow(cosLat1 * sinLat2 - sinLat1 * cosLat2LonD, 2)), (sinLat1 * sinLat2 + cosLat1 * cosLat2LonD));
    }

    /// <summary>Compass point (to given precision) for specified bearing.</summary>
    /// <remarks>Precision = max length of compass point, 1 = the four cardinal directions, 2 = ; it could be extended to 4 for quarter-winds (eg NEbN), but I think they are little used.</remarks>
    /// <param name="absoluteBearingInRadians">The direction in radians.</param>
    /// <param name="precision">4 = the four cardinal directions, 8 = the four cardinals and four intercardinal together (a.k.a. the eight principal winds) form the 8-wind compass rose, 16 = the eight principal winds and the eight half-winds together form the 16-wind compass rose, 32 = the eight principal winds, eight half-winds and sixteen quarter-winds form the 32-wind compass rose.</param>
    /// <returns></returns>
    public static ThirtytwoWindCompassRose CompassPoint(double absoluteBearingInRadians, PointsOfTheCompass precision, out double notch)
    {
      notch = System.Math.Round(Maths.Wrap(absoluteBearingInRadians, 0, Maths.PiX2) / (Maths.PiX2 / (int)precision) % (int)precision);

      return (ThirtytwoWindCompassRose)(int)(notch * (32 / (int)precision));
    }

    /// <summary>Convert geographical coordinate to sperical coordinate.</summary>
    /// <param name="latitudeRad"></param>
    /// <param name="longitudeRad"></param>
    /// <param name="altitude"></param>
    public static (double radius, double inclinationRad, double azimuthRad) ConvertToSphericalCoordinate(double latitudeRad, double longitudeRad, double altitude)
      => (altitude, System.Math.PI - (latitudeRad + Maths.PiOver2), longitudeRad + System.Math.PI);

    /// <summary>The distance of a point from a great-circle path (sometimes called cross track error). The sign of the result tells which side of the path the third point is on.</summary>
    /// <remarks>Central angles are subtended by an arc between those two points, and the arc length is the central angle of a circle of radius one (measured in radians). The central angle is also known as the arc's angular distance.</remarks>
    public static double CrossTrackCentralAngle(double latitude1, double longitude1, double latitude2, double longitude2, double latitude3, double longitude3, out double trackCentralAngle13)
    {
      trackCentralAngle13 = CentralAngleVincentyFormula(latitude1, longitude1, latitude3, longitude3);

      var bearing13 = InitialBearing(latitude1, longitude1, latitude3, longitude3);
      var bearing12 = InitialBearing(latitude1, longitude1, latitude2, longitude2);

      return System.Math.Asin(System.Math.Sin(trackCentralAngle13) * System.Math.Sin(bearing13 - bearing12));
    }

    /// <summary>Given a start point, initial bearing, and angularDistance, this will calculate the destination point and final bearing travelling along a (shortest distance) great circle arc.</summary>
    /// <param name="bearing">Bearing is the direction or course.</param>
    /// <param name="angularDistance">The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</param>
    /// <remarks>The angular distance is a distance divided by a radius of the same unit, e.g. meters. (1000 m / EarthMeanRadiusInMeters)</remarks>
    public static void EndPoint(double latitude, double longitude, double bearing, double angularDistance, out double latitudeOut, out double longitudeOut)
    {
      var cosLat = System.Math.Cos(latitude);
      var sinLat = System.Math.Sin(latitude);

      var cosAd = System.Math.Cos(angularDistance);
      var sinAd = System.Math.Sin(angularDistance);

      latitudeOut = System.Math.Asin(sinLat * cosAd + cosLat * sinAd * System.Math.Cos(bearing));
      longitudeOut = longitude + System.Math.Atan2(System.Math.Sin(bearing) * sinAd * cosLat, cosAd - sinLat * System.Math.Sin(latitude));
    }

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
    public static double FinalBearing(double latitude1, double longitude1, double latitude2, double longitude2)
      => (InitialBearing(latitude2, longitude2, latitude1, longitude1) + System.Math.PI) % Maths.PiX2;

    ///// <summary>Computes the square of half the chord length between the points, often labeled "a" in C implementations.</summary>
    ///// <returns>The square of half the chord length between the points.</returns>
    //public static double HaversineFormulaA(double latitude1, double longitude1, double latitude2, double longitude2) => Math.Haversin(latitude2 - latitude1) + System.Math.Cos(latitude1) * System.Math.Cos(latitude2) * Math.Haversin(longitude2 - longitude1);
    ///// <summary>Computes the angular distance in radians, often labeled "c" in C implementations.</summary>
    ///// <returns>The angular distance in radians.</returns>
    ///// <remarks>An alternative to this method is to pass the specified parmater (haversineFormulaA) to the inverse half versine function: Flux.Math.HaversinInverse(haversineFormulaA).</remarks>
    //public static double HaversineFormulaC(double haversineFormulaA) => 2 * System.Math.Atan2(System.Math.Sqrt(haversineFormulaA), System.Math.Sqrt(1 - haversineFormulaA));

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    /// <remarks>In general, your current heading will vary as you follow a great circle path (orthodrome); the final heading will differ from the initial heading by varying degrees according to distance and latitude.</remarks>
    public static double InitialBearing(double radLat1, double radLon1, double radLat2, double radLon2)
    {
      var cosLat2 = System.Math.Cos(radLat2);
      var lonD = radLon2 - radLon1;

      var y = System.Math.Sin(lonD) * cosLat2;
      var x = System.Math.Cos(radLat1) * System.Math.Sin(radLat2) - System.Math.Sin(radLat1) * cosLat2 * System.Math.Cos(lonD);

      return (System.Math.Atan2(y, x) + Maths.PiX2) % Maths.PiX2; // atan2 returns values in the range -π - +π radians (i.e. -180 - +180 degrees), so we normalize to 0 - (PI * 2) radians (i.e. 0 - 360 degrees)
    }

    /// <summary>An intermediate point at any fraction along the great circle path between two points can also be calculated.</summary>
    /// <param name="unitInterval">Unit interval is a fraction along great circle route (0=Latitude1,Longitude1, 1=Latitude2,Longitude2)</param>
    public static void IntermediaryPoint(double latitude1, double longitude1, double latitude2, double longitude2, double unitInterval, out double latitudeOut, out double longitudeOut)
    {
      var centralAngle = CentralAngleVincentyFormula(latitude1, longitude1, latitude2, longitude2);

      var a = System.Math.Sin((1.0 - unitInterval) * centralAngle) / System.Math.Sin(centralAngle);
      var b = System.Math.Sin(unitInterval * centralAngle) / System.Math.Sin(centralAngle);

      var cosLat1 = System.Math.Cos(latitude1);
      var cosLat2 = System.Math.Cos(latitude2);

      var x = a * cosLat1 * System.Math.Cos(longitude1) + b * cosLat2 * System.Math.Cos(longitude2);
      var y = a * cosLat1 * System.Math.Sin(longitude1) + b * cosLat2 * System.Math.Sin(longitude2);
      var z = a * System.Math.Sin(latitude1) + b * System.Math.Sin(latitude2);

      latitudeOut = System.Math.Atan2(z, System.Math.Sqrt(x * x + y * y));
      longitudeOut = System.Math.Atan2(y, x);
    }

    public static (double, double) IntersectionOfPaths(double lat1, double lon1, double bearing1, double lat2, double lon2, double bearing2)
    {
      var dLat = lat2 - lat1;
      var dLon = lon2 - lon1;

      var cosLat1 = System.Math.Cos(lat1);
      var cosLat2 = System.Math.Cos(lat2);
      var sinLat1 = System.Math.Sin(lat1);
      var sinLat2 = System.Math.Sin(lat2);

      var d12 = 2 * System.Math.Asin(System.Math.Sqrt(System.Math.Pow(System.Math.Sin(dLat / 2), 2) + cosLat1 * cosLat2 * System.Math.Pow(System.Math.Sin(dLon / 2), 2)));

      var φ1 = System.Math.Acos(sinLat2 - sinLat1 * System.Math.Cos(d12) / System.Math.Sin(d12) * cosLat1);
      var φ2 = System.Math.Acos(sinLat1 - sinLat2 * System.Math.Cos(d12) / System.Math.Sin(d12) * cosLat2);

      var bearing12 = System.Math.Sin(dLon) > 0 ? φ1 : Maths.PiX2 - φ1;
      var bearing21 = System.Math.Sin(dLon) > 0 ? Maths.PiX2 - φ2 : φ2;

      var α1 = (bearing1 - bearing12 + System.Math.PI) % Maths.PiX2 - System.Math.PI;
      var α1cos = System.Math.Cos(α1);
      var α1sin = System.Math.Sin(α1);

      var α2 = (bearing21 - bearing2 + System.Math.PI) % Maths.PiX2 - System.Math.PI;
      var α2cos = System.Math.Cos(α2);
      var α2sin = System.Math.Sin(α2);

      var α3 = System.Math.Acos(-α1cos * α2cos + α1sin * α2sin * System.Math.Cos(d12));

      var d13 = System.Math.Atan2(System.Math.Sin(d12) * α1sin * α2sin, α2cos + α1cos * System.Math.Cos(α3));
      var d13cos = System.Math.Cos(d13);
      var d13sin = System.Math.Sin(d13);

      var lat3 = System.Math.Asin(sinLat1 * d13cos + cosLat1 * d13sin * System.Math.Cos(bearing1));

      var dLon13 = System.Math.Atan2(System.Math.Sin(bearing1) * d13sin * cosLat1, d13cos - sinLat1 * System.Math.Sin(lat3));

      var lon3 = (lon1 + dLon13 + System.Math.PI) % Maths.PiX2 - System.Math.PI;

      return (lat3, lon3);
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    public static double MaximumLatitude(double latitude, double bearing)
      => System.Math.Acos(System.Math.Abs(System.Math.Sin(bearing) * System.Math.Cos(latitude)));

    /// <summary></summary>
    /// <example>
    /// var pixel = Flux.Mercator.MercatorProjectPixel(5, 9.95, 10000, 10000);
    /// var ll = Flux.Mercator.MercatorUnprojectPixel(pixel.X, pixel.Y, 10000, 10000);
    /// </example>
    //public static (double X, double Y) MercatorProjectPixel(double latitude, double longitude, int pixelCanvasWidth, int pixelCanvasHeight)
    //{
    //  var x = (longitude + 180.0) * pixelCanvasWidth / 360.0;

    //  var mpForward = System.Math.PI - System.Math.Log(System.Math.Tan((Quantity.Angle.ConvertDegreeToRadian(latitude) / 2.0) + Maths.PiOver4));

    //  var y = (pixelCanvasHeight / 2.0) - (mpForward * (pixelCanvasWidth / Maths.PiX2));

    //  return (x, y);
    //}
    //public static (double Latitude, double Longitude) MercatorUnprojectPixel(double x, double y, int pixelCanvasWidth, int pixelCanvasHeight)
    //{
    //  var longitude = x * 360.0 / pixelCanvasWidth - 180.0;

    //  var mpInverse = ((pixelCanvasHeight / 2.0) - y) / (pixelCanvasWidth / Maths.PiX2);

    //  var latitude = Quantity.Angle.ConvertRadianToDegree((System.Math.Atan(System.Math.Pow(System.Math.E, mpInverse)) - Maths.PiOver4) * 2.0);

    //  return (latitude, longitude);
    //}

    /// <summary>This is the halfway point along a great circle path between the two points.</summary>
    public static void Midpoint(double latitude1, double longitude1, double latitude2, double longitude2, out double latitudeOut, out double longitudeOut)
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
    public static bool TryParse(string latitudeDMS, string longitudeDMS, out GeographicCoord result, double earthRadius)
    {
      if (Formatting.DmsFormatter.TryParse(latitudeDMS, out var latitude) && Formatting.DmsFormatter.TryParse(longitudeDMS, out var longitude))
      {
        result = new GeographicCoord(latitude, longitude, earthRadius);
        return true;
      }

      result = Empty;
      return false;
    }
    #endregion Static members

    #region Overloaded operators
    public static bool operator ==(GeographicCoord a, GeographicCoord b)
      => a.Equals(b);
    public static bool operator !=(GeographicCoord a, GeographicCoord b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(GeographicCoord other)
      => Altitude == other.Altitude && Latitude == other.Latitude && Longitude == other.Longitude;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new Formatting.GeopointFormatter(), format ?? $"<{nameof(GeographicCoord)}: {{0:DMS}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is GeographicCoord o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Altitude, Latitude, Longitude);
    public override string ToString()
      => ToString($"<{nameof(GeographicCoord)}: {{0:DMS}}>", new Formatting.GeopointFormatter());
    #endregion Object overrides
  }
}

/*
      System.Console.OutputEncoding = System.Text.Encoding.Unicode;

      var lax = new Flux.Geoposition() { Latitude = Flux.Convert.Angle.RadiansToDegrees(0.592539), Longitude = Flux.Convert.Angle.RadiansToDegrees(2.066470) };
      var jfk = new Flux.Geoposition() { Latitude = Flux.Convert.Angle.RadiansToDegrees(0.709186), Longitude = Flux.Convert.Angle.RadiansToDegrees(1.287762) };

      if ("32°13′18″N 110°55′35″W" is string dmsTucson && Flux.Geoposition.TryParseDMS(dmsTucson, out var ddTucson))
      {
        System.Console.WriteLine("Tucson: {0}, {1}, {2}", dmsTucson, ddTucson, string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:D6NS} {1:D6EW}", ddTucson.Latitude, ddTucson.Longitude));
      }

      if ("33°27′N 112°04′W" is string dmsPhoenix && Flux.Geoposition.TryParseDMS(dmsPhoenix, out var ddPhoenix))
      {
        System.Console.WriteLine("Phoenix: {0}, {1}, {2}", dmsPhoenix, ddPhoenix, ddPhoenix.ToString());
      }

      if ("32°53′9″N 111°44′38″W" is string dmsCasaGrande && Flux.Geoposition.TryParseDMS(dmsCasaGrande, out var ddCasaGrande))
      {
        System.Console.WriteLine("CasaGrande: {0}, {1}, {2}", dmsCasaGrande, ddCasaGrande, ddCasaGrande.ToString());
      }

      System.Console.WriteLine();

      System.Console.WriteLine(lax.DistanceTo(jfk, Flux.Geoposition.EarthRadii.MeanInMiles));

      System.Console.ReadKey();
*/
