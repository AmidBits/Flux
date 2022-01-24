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

  public static partial class Maths
  {
    /// <summary>Determines if two values are almost equal by some absolute difference. Good if a consistent tolerance is required. Can be said to handle smaller values better, since the larger the values become, the absolute tolerance is rendered more and more insignificant.</summary>
    /// <param name="absoluteTolerance">A value to directly compare the difference between the two values against. I.e. the absolute tolerance does not have any relation to the values compared against.</param>
    public static bool EqualWithinAbsoluteTolerance(double a, double b, double absoluteTolerance = 1E-15)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);
  }
}
