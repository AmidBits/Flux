#if WINDOWS_UWP
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>The distance along the specified track (from its starting point) where this position is the closest to the track.</summary>
    /// <param name="trackStart"></param>
    /// <param name="trackEnd"></param>
    /// <param name="earthRadius">This can be used to control the unit of measurement for the distance, e.g. Meters.</param>
    /// <returns>The distance from trackStart along the course towards trackEnd to a point abeam the position.</returns>
    public static double AlongTrackDistance(this Windows.Devices.Geolocation.BasicGeoposition source, Windows.Devices.Geolocation.BasicGeoposition trackStart, Windows.Devices.Geolocation.BasicGeoposition trackEnd)
      => Flux.EarthRadii.MeanInMeters * Flux.Geoposition.AlongTrackCentralAngle(Convert.Angle.Degree.ToRadians(trackStart.Latitude), Convert.Angle.Degree.ToRadians(trackStart.Longitude), Convert.Angle.Degree.ToRadians(trackEnd.Latitude), Convert.Angle.Degree.ToRadians(trackEnd.Longitude), Convert.Angle.Degree.ToRadians(source.Latitude), Convert.Angle.Degree.ToRadians(source.Longitude), out var crossTrackErrorCentralAngle);

    /// <summary>The shortest distance of this position from the specified track.</summary>
    /// <remarks>The cross track error, i.e. the distance off course.</remarks>
    public static double CrossTrackError(this Windows.Devices.Geolocation.BasicGeoposition source, Windows.Devices.Geolocation.BasicGeoposition trackStart, Windows.Devices.Geolocation.BasicGeoposition trackEnd)
      => Flux.EarthRadii.MeanInMeters * Flux.Geoposition.CrossTrackCentralAngle(Convert.Angle.Degree.ToRadians(trackStart.Latitude), Convert.Angle.Degree.ToRadians(trackStart.Longitude), Convert.Angle.Degree.ToRadians(trackEnd.Latitude), Convert.Angle.Degree.ToRadians(trackEnd.Longitude), Convert.Angle.Degree.ToRadians(source.Latitude), Convert.Angle.Degree.ToRadians(source.Longitude), out var trackCentralAngle13);

    /// <summary>Given a start point, initial bearing, and distance in meters, this will calculate the destina­tion point and final bearing travelling along a (shortest distance) great circle arc.</summary>
    public static Windows.Devices.Geolocation.BasicGeoposition DestinationPointAt(this Windows.Devices.Geolocation.BasicGeoposition source, double bearingDegrees, double distanceMeters)
    {
      Flux.Geoposition.EndPoint(Flux.Convert.Angle.Degree.ToRadians(source.Latitude), Flux.Convert.Angle.Degree.ToRadians(source.Longitude), Flux.Convert.Angle.Degree.ToRadians(bearingDegrees), distanceMeters / Flux.EarthRadii.MeanInMeters, out var lat, out var lon);

      return new Windows.Devices.Geolocation.BasicGeoposition() { Latitude = Flux.Convert.Angle.Radian.ToDegrees(lat), Longitude = Flux.Convert.Angle.Radian.ToDegrees(lon) };
    }

    /// <summary>By using the ‘haversine’ formula to calculate the great-circle distance between two points – that is, the shortest distance over the earth’s surface – giving an ‘as-the-crow-flies’ distance between the points.</summary>
    /// <return>Returns the distance (using the Haversine formula) from the geographic position to the location specified in meters.</returns>
    public static double DistanceTo(this Windows.Devices.Geolocation.BasicGeoposition source, Windows.Devices.Geolocation.BasicGeoposition target)
      => Flux.EarthRadii.MeanInMeters * Flux.Math.Ahvsin(Flux.Geoposition.CentralAngleHaversineFormula(Flux.Convert.Angle.Degree.ToRadians(source.Latitude), Flux.Convert.Angle.Degree.ToRadians(source.Longitude), Flux.Convert.Angle.Degree.ToRadians(target.Latitude), Flux.Convert.Angle.Degree.ToRadians(target.Longitude)));

    /// <summary>Calculates the approximate radius at the latitude of the specified geoposition.</summary>
    public static double GetApproximateRadius(this Windows.Devices.Geolocation.BasicGeoposition source)
      => Flux.Geoposition.ApproximateRadiusAtLatitude(Flux.Convert.Angle.Degree.ToRadians(source.Latitude));

    /// <summary>Calculates the approximate radius at the latitude of the specified geoposition.</summary>
    public static double GetLatitudinalHeight(this Windows.Devices.Geolocation.BasicGeoposition source)
      => Flux.Geoposition.ApproximateLatitudinalHeight(Flux.Convert.Angle.Degree.ToRadians(source.Latitude));
    public static double GetLongitudinalWidth(this Windows.Devices.Geolocation.BasicGeoposition source)
      => Flux.Geoposition.ApproximateLongitudinalWidth(Flux.Convert.Angle.Degree.ToRadians(source.Latitude));

    /// <summary>Returns the initial bearing (sometimes referred to as forward azimuth) which if followed in a straight line along a great-circle arc will take you from the start point to the end point.</summary>
    public static double InitialBearingTo(this Windows.Devices.Geolocation.BasicGeoposition source, Windows.Devices.Geolocation.BasicGeoposition target)
      => Flux.Convert.Angle.Radian.ToDegrees(Flux.Geoposition.InitialBearing(Flux.Convert.Angle.Degree.ToRadians(source.Latitude), Flux.Convert.Angle.Degree.ToRadians(source.Longitude), Flux.Convert.Angle.Degree.ToRadians(target.Latitude), Flux.Convert.Angle.Degree.ToRadians(target.Longitude)));

    /// <summary>This is the halfway point along a great circle path between the two points.</summary>
    public static Windows.Devices.Geolocation.BasicGeoposition MidpointTo(this Windows.Devices.Geolocation.BasicGeoposition source, Windows.Devices.Geolocation.BasicGeoposition target)
    {
      Flux.Geoposition.Midpoint(Flux.Convert.Angle.Degree.ToRadians(source.Latitude), Flux.Convert.Angle.Degree.ToRadians(source.Longitude), Flux.Convert.Angle.Degree.ToRadians(target.Latitude), Flux.Convert.Angle.Degree.ToRadians(target.Longitude), out var lat, out var lon);

      return new Windows.Devices.Geolocation.BasicGeoposition() { Latitude = Flux.Convert.Angle.Radian.ToDegrees(lat), Longitude = Flux.Convert.Angle.Radian.ToDegrees(lon) };
    }

    /// <summary>An intermediate point at any fraction along the great circle path between two points can also be calculated.</summary>
    public static Windows.Devices.Geolocation.BasicGeoposition IntermediaryPointTo(this Windows.Devices.Geolocation.BasicGeoposition source, Windows.Devices.Geolocation.BasicGeoposition target, double unitInterval)
    {
      Flux.Geoposition.IntermediaryPoint(Flux.Convert.Angle.Degree.ToRadians(source.Latitude), Flux.Convert.Angle.Degree.ToRadians(source.Longitude), Flux.Convert.Angle.Degree.ToRadians(target.Latitude), Flux.Convert.Angle.Degree.ToRadians(target.Longitude), unitInterval, out var lat, out var lon);

      return new Windows.Devices.Geolocation.BasicGeoposition() { Latitude = Flux.Convert.Angle.Radian.ToDegrees(lat), Longitude = Flux.Convert.Angle.Radian.ToDegrees(lon) };
    }

    /// <summary>Returns the latitude in radians.</summary>
    public static double LatitudeInRadians(this Windows.Devices.Geolocation.BasicGeoposition source)
      => Flux.Convert.Angle.Degree.ToRadians(source.Latitude);
    /// <summary>Returns the longitude in radians.</summary>
    public static double LongitudeInRadians(this Windows.Devices.Geolocation.BasicGeoposition source)
      => Flux.Convert.Angle.Degree.ToRadians(source.Longitude);

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    public static double MaximumLatitude(this Windows.Devices.Geolocation.BasicGeoposition source, double bearing)
      => Flux.Geoposition.MaximumLatitude(Convert.Angle.Degree.ToRadians(source.Latitude), Convert.Angle.Degree.ToRadians(bearing));

    public static string ToStringEx(this Windows.Devices.Geolocation.BasicGeoposition source)
    {
      return string.Format("[{0:F5}r, {1:F5}° lat, {2:F5}° long]", source.Altitude, source.Latitude, source.Longitude);
    }
  }
}
#endif
