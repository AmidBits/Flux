using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    public static double ProbabilityMassFunction(double k, double n, double p)
      => (n / k) * System.Math.Pow(p, k) * System.Math.Pow(1 - p, n - k);
  }
}
