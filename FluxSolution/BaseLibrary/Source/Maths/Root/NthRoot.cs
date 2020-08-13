namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Represents the exponent of a cube root calculation.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Nth_root"/>
    public static double NthRootExponent(double root) => 1.0 / root;

    /// <summary>Returns the nth root (index) of a specified value (radicand).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Nth_root"/>
    public static double NthRootOf(double radicand, int root) => CopySign(System.Math.Pow(System.Math.Abs(radicand), 1.0 / root), radicand);
  }
}
