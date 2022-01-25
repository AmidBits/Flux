namespace Flux
{
  public sealed class EqualityByRelativeTolerance
   : IApproximateEquatable
  {
    private double m_relativeTolerance;

    public EqualityByRelativeTolerance(double relativeTolerance)
      => m_relativeTolerance = relativeTolerance;
    public EqualityByRelativeTolerance()
      : this(1E-15)
    {
    }

    public bool IsApproximatelyEqual(double a, double b)
      => IsApproximatelyEqual(a, b, m_relativeTolerance);

    public static bool IsApproximatelyEqual(double a, double b, double relativeTolerance = 1E-15)
       => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);
  }
}
