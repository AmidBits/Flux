namespace Flux
{
  public interface IApproximateEquatable
  {
    /// <summary>Determines if two values are almost equal</summary>
    bool IsApproximatelyEqual(double a, double b);
  }
}
