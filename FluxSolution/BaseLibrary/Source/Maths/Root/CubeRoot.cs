namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Represents the cube root of 2. (Delian's constant)</summary>
    public const double CubeRootOf2 = 1.2599210498948731647672106072782;
    /// <summary>Represents the cube root of 3.</summary>
    public const double CubeRootOf3 = 1.4422495703074083823216383107801;
    /// <summary>Represents the cube root of 4.</summary>
    public const double CubeRootOf4 = 1.5874010519681994747517056392723;
    /// <summary>Represents the cube root of 5.</summary>
    public const double CubeRootOf5 = 1.7099759466766969893531088725439;
    /// <summary>Represents the cube root of 6.</summary>
    public const double CubeRootOf6 = 1.8171205928321396588912117563272;
    /// <summary>Represents the cube root of 7.</summary>
    public const double CubeRootOf7 = 1.9129311827723891011991168395487;
    /// <summary>Represents the cube root of 9.</summary>
    public const double CubeRootOf9 = 2.0800838230519041145300568243579;

    /// <summary>Represents the exponent of a cube root calculation.</summary>
    public const double CubeRootExponent = 1.0 / 3.0;

    /// <summary>Returns the cube root of an arbitrary base value.</summary>
    public static double CubeRootOf(double radicand)
      => CopySign(System.Math.Pow(System.Math.Abs(radicand), CubeRootExponent), radicand);
  }
}
