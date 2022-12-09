namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the polar coordinates to cartesian 2D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static CoordinateSystems.CartesianCoordinate2<TSelf> ToCartesianCoordinate2<TSelf>(this IPolarCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        source.Radius * TSelf.Cos(source.Azimuth),
        source.Radius * TSelf.Sin(source.Azimuth)
      );

    /// <summary>Converts the polar coordinates to a complex number.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static System.Numerics.Complex ToComplex<TSelf>(this IPolarCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => System.Numerics.Complex.FromPolarCoordinates(
        double.CreateChecked(source.Radius),
        double.CreateChecked(source.Azimuth)
      );

    public static CoordinateSystems.PolarCoordinate<TSelf> ToPolarCoordinate<TSelf>(this IPolarCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(source.Radius, source.Azimuth);

    public static (Quantities.Length radius, Quantities.Angle azimuth) ToQuantities<TSelf>(this IPolarCoordinate<TSelf> source)
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
    => (
      new Quantities.Length(double.CreateChecked(source.Radius)),
      new Quantities.Angle(double.CreateChecked(source.Azimuth))
    );
  }
  #endregion ExtensionMethods

  /// <summary>The polar coordinate system is a two-dimensional coordinate system in which each point on a plane is determined by a distance from a reference point and an angle from a reference direction.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  public interface IPolarCoordinate<TSelf>
      : System.IFormattable
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    /// <summary>Radius. A.k.a. radial coordinate, or radial distance.</summary>
    TSelf Radius { get; init; }
    /// <summary>The azimuth angle in radians. A.k.a. angular coordinate, or polar angle.</summary>
    /// <remarks>The angle φ is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
    TSelf Azimuth { get; init; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Azimuth = {new Quantities.Angle(double.CreateChecked(Azimuth)).ToUnitString(Quantities.AngleUnit.Degree, format ?? "N3", true)} }}";
  }
}
