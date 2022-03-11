
namespace Flux
{
  public static partial class Polynomial
  {
    public static double Function(params double[] xAndAlphas)
    {
      var sum = 0.0;

      for (int index = 1, exponent = xAndAlphas.Length - 2; exponent >= 0; index++, exponent--)
        sum += xAndAlphas[index] * System.Math.Pow(xAndAlphas[0], exponent);

      return sum;
    }
  }
}
