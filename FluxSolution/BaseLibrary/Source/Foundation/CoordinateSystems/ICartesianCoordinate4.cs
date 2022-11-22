namespace Flux
{
  /// <summary>Cartesian 4D coordinate.</summary>
  public interface ICartesianCoordinate4<TSelf>
    : System.IFormattable, ICartesianCoordinate3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf W { get; }

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)}, W = {string.Format($"{{0:{format ?? "N6"}}}", W)} }}";
  }
}
