namespace Flux
{
  public static partial class CoordinateSystems
  {
    /// <summary>Converts the polar coordinates to cartesian 2D coordinates.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static CartesianCoordinate2<TSelf> ToCartesianCoordinate2<TSelf>(this IPolarCoordinate<TSelf> source)
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

    public static PolarCoordinate<TSelf> ToPolarCoordinate<TSelf>(this IPolarCoordinate<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(source.Radius, source.Azimuth);

    public static (Length radius, Angle azimuth) ToQuantities<TSelf>(this IPolarCoordinate<TSelf> source)
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
    => (
      new Length(double.CreateChecked(source.Radius)),
      new Angle(double.CreateChecked(source.Azimuth))
    );
  }
}
