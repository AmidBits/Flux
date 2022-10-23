namespace Flux
{
  public interface ISphericalCoordinate
  {
    /// <summary>Radius in meters.</summary>
    double Radius { get; }
    /// <summary>Inclination in radians.</summary>
    double Inclination { get; }
    /// <summary>Azimuth in radians.</summary>
    double Azimuth { get; }

    /// <summary>Elevation in radians.</summary>
    double Elevatiuon => System.Math.PI / 2 - Inclination;
  }
}
