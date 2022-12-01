namespace Flux
{
  /// <summary>Cartesian 2D coordinate.</summary>
  public interface ICartesianCoordinate2<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }

    /// <summary>For 2D vectors, the cross product of two vectors, is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</summary>
    static TSelf CrossProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b)
      => a.X * b.Y - a.Y * b.X;

    /// <summary>Returns the dot product of two normalized 2D vectors.</summary>
    static TSelf DotProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b)
      => a.X * b.X + a.Y * b.Y;

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)} }}";
  }
}
