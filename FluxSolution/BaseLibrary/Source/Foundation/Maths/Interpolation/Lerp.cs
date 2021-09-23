namespace Flux
{
  public static partial class Maths
  {
    public static decimal Lerp(decimal x1, decimal x2, decimal mu)
      => x1 * (1 - mu) + x2 * mu;
    public static double Lerp(double x1, double x2, double mu)
      => x1 * (1 - mu) + x2 * mu;
    public static float Lerp(float x1, float x2, float mu)
      => x1 * (1 - mu) + x2 * mu;
  }
}
