﻿namespace Flux.MapProjections
{
  // https://en.wikipedia.org/wiki/Equirectangular_projection
  public struct EquirectangularProjection
    : System.IEquatable<EquirectangularProjection>, IMapForwardProjectable, IMapReverseProjectable
  {
    public static readonly EquirectangularProjection Default;

    public GeographicCoordinate CenterOfMap { get; init; }
    public double StandardParallels { get; init; }

    public CartesianCoordinate3 ProjectForward(GeographicCoordinate project)
      => new(project.Altitude.GeneralUnitValue * (project.Longitude.Radian - CenterOfMap.Longitude.Radian) * System.Math.Cos(StandardParallels), project.Altitude.GeneralUnitValue * (project.Latitude.Radian - CenterOfMap.Latitude.Radian), project.Altitude.GeneralUnitValue);
    public GeographicCoordinate ProjectReverse(CartesianCoordinate3 project)
      => new(project.X / (project.Z * System.Math.Cos(StandardParallels)) + CenterOfMap.Longitude.GeneralUnitValue, project.Y / project.Z + CenterOfMap.Latitude.GeneralUnitValue, project.Z);

    #region Overloaded operators
    public static bool operator ==(EquirectangularProjection a, EquirectangularProjection b)
      => a.Equals(b);
    public static bool operator !=(EquirectangularProjection a, EquirectangularProjection b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] EquirectangularProjection other)
      => CenterOfMap == other.CenterOfMap && StandardParallels == other.StandardParallels;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is EquirectangularProjection o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(CenterOfMap, StandardParallels);
    public override string ToString()
      => $"{GetType().Name}";
    #endregion Object overrides
  }

}