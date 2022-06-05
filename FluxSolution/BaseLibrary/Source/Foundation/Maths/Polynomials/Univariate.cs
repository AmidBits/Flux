
namespace Flux
{
  public static partial class Polynomial
  {
    /// <summary>A polynomial in one indeterminate.</summary>
    /// <param name="x"></param>
    /// <param name="coefficients"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static double Univariate(double x, params double[] coefficients)
    {
      if (coefficients.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(coefficients));

      var index = coefficients.Length - 1;

      var sum = coefficients[index--] + (coefficients[index--] * x);

      var exponent = 2; // Use Pow() with 3 or more coefficients (i.e. with an exponent of 2 or more). More multiplications, potentially decrease accuracy.

      while (index >= 0)
        sum += (coefficients[index--] * System.Math.Pow(x, exponent++));

      return sum;
    }
  }
}
