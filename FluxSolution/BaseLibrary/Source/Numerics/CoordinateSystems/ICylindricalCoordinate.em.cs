namespace Flux
{
  public static partial class CoordinateSystems
  {
    /// <summary>Converts the cylindrical coordinates to cartesian 3D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        source.Radius * TSelf.Cos(source.Azimuth),
        source.Radius * TSelf.Sin(source.Azimuth),
        source.Height
      );

    public static CylindricalCoordinate<TSelf> ToCylindricalCoordinate<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(source.Radius, source.Azimuth, source.Height);

    /// <summary>Converts the cylindrical coordinates to polar coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static PolarCoordinate<TSelf> ToPolarCoordinate<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        source.Radius,
        source.Azimuth
      );

    public static (Length radius, Angle azimuth, Length height) ToQuantities<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => (
        new Length(double.CreateChecked(source.Radius)),
        new Angle(double.CreateChecked(source.Azimuth)),
        new Length(double.CreateChecked(source.Height))
      );

    /// <summary>Converts the cylindrical coordinates to spherical coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static SphericalCoordinate<TSelf> ToSphericalCoordinate<TSelf>(this ICylindricalCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        TSelf.Sqrt(source.Radius * source.Radius + source.Height * source.Height),
        TSelf.Atan(source.Radius / source.Height),
        source.Height
      );
  }
}
