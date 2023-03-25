namespace Flux
{
  namespace Numerics
  {
    /// <summary>A 4D cartesian coordinate.</summary>
    public interface ICartesianCoordinate4<TSelf>
    : ICartesianCoordinate<TSelf>, System.IFormattable
    where TSelf : System.Numerics.INumber<TSelf>
    {
      TSelf X { get; }
      TSelf Y { get; }
      TSelf Z { get; }
      TSelf W { get; }

      string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)}, W = {string.Format($"{{0:{format ?? "N6"}}}", W)} }}";
    }
  }
}
