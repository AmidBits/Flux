namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the nth root (index) of a specified value (radicand).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Nth_root"/>
    public static double Nthrt(double radicand, int root)
    {
      if (radicand < 0)
        return double.NaN;
      if (root == 0)
        return double.NaN;
      if (root == 1)
        return radicand;
      if (root == 2)
        return System.Math.Sqrt(radicand);
      if (root == 3)
        return System.Math.Cbrt(radicand);

      return root < 0
        ? System.Math.Pow(1 / System.Math.Abs(radicand), -(1.0 / root))
        : System.Math.CopySign(System.Math.Pow(System.Math.Abs(radicand), 1.0 / root), radicand);
    }
  }
}
