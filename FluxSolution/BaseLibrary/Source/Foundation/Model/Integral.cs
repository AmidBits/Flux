namespace Flux
{
  public static class Integral
  {
    public static double Integrate(double magnitude, int steps, System.Func<double, double> function)
    {
      if (function is null) throw new System.ArgumentNullException(nameof(function));

      var stepSize = magnitude / steps;

      var sum = 0d;
      for (var iteration = 1; iteration <= steps; iteration++)
        sum += function((double)iteration / (double)steps) * stepSize;
      return sum;
    }
  }
}