namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
#if NET5_0
  public struct EquirectangularProjection
    : System.IEquatable<EquirectangularProjection>, IMapForwardProjectable, IMapReverseProjectable
#elif NET6_0_OR_GREATER
  public record struct EquirectangularProjection
    : IMapForwardProjectable, IMapReverseProjectable
#endif
  {
    public static readonly EquirectangularProjection Default;

    public GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
      => new(project.Altitude.Value * (project.Longitude.Radian - CenterOfMap.Longitude.Radian) * System.Math.Cos(StandardParallels), project.Altitude.Value * (project.Latitude.Radian - CenterOfMap.Latitude.Radian), project.Altitude.Value);
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3 project)
      => new(project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.Longitude.Value, project.Y / project.Z + CenterOfMap.Latitude.Value, project.Z);

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(EquirectangularProjection a, EquirectangularProjection b)
      => a.Equals(b);
    public static bool operator !=(EquirectangularProjection a, EquirectangularProjection b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] EquirectangularProjection other)
      => CenterOfMap == other.CenterOfMap && StandardParallels == other.StandardParallels;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is EquirectangularProjection o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(CenterOfMap, StandardParallels);
#endif
    public override string ToString()
      => $"{GetType().Name}";
    #endregion Object overrides
  }

}
