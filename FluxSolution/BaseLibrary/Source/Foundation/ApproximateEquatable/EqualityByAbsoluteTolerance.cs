namespace Flux
{
  public sealed class EqualityByAbsoluteTolerance
    : IApproximateEquatable
  {
    private double m_absoluteTolerance;

    public EqualityByAbsoluteTolerance(double absoluteTolerance)
      => m_absoluteTolerance = absoluteTolerance;
    public EqualityByAbsoluteTolerance()
      : this(1E-15)
    {
    }

    public bool IsApproximatelyEqual(double a, double b)
      => IsApproximatelyEqual(a, b, m_absoluteTolerance);

    public static bool IsApproximatelyEqual(double a, double b, double absoluteTolerance = 1E-15)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);
  }
}
