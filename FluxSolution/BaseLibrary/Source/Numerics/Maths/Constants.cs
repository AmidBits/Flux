namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Represents the Champernowne constant. A transcendental real constant whose decimal expansion has important properties.</summary>
    public const double C10 = 0.123456789101112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555657585960;

#if NET7_0_OR_GREATER

    /// <summary>Computed on the fly.</summary>
    public static TSelf ComputeMachineEpsilon<TSelf>()
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var epsilon = TSelf.One;

      while (epsilon / TSelf.CreateChecked(2) is var halfEpsilon && (TSelf.One + halfEpsilon) > TSelf.One)
        epsilon = halfEpsilon;

      return epsilon;
    }

#else

    /// <summary>Computed on the fly.</summary>
    public static double ComputeMachineEpsilon()
    {
      var epsilon = 1d;

      while (epsilon / 2 is var half && (1 + half) > 1)
        epsilon = half;

      return epsilon;
    }

#endif

    /// <summary>Represents the cube root of 2.</summary>
    public const double DeliansConstant = 1.2599210498948731647672106072782;

    /// <summary>
    /// <para>The 'machine' epsilon, constant for System.Single, from the 32-bit float CPP definition.</para>
    /// <example>
    /// <code>System.Math.Exp(-23 * System.Math.Log(2));</code>
    /// <code>System.Math.Pow(2, -23);</code>
    /// <code>1.1920928955078125E-07f</code>
    /// </example>
    /// </summary>
    public const float EpsilonCpp32 = 1.1920928955078125E-07f;

    /// <summary>
    /// <para>The 'machine' epsilon, constant for System.Double, from the 64-bit double CPP definition.</para>
    /// <example>
    /// <code>System.Math.Exp(-52 * System.Math.Log(2));</code>
    /// <code>System.Math.Pow(2, -52);</code>
    /// <code>2.220446049250313E-16d</code>
    /// </example>
    /// </summary>
    public const double EpsilonCpp64 = 2.220446049250313E-16d;

    /// <summary>A C# epsilon for System.Single.</summary>
    public const float Epsilon1E7 = 1E-7f;
    /// <summary>A C# epsilon for System.Double.</summary>
    public const double Epsilon1E15 = 1E-15d;

    /// <summary>Represents mathematical constants.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Euler%27s_constant"/>
    public const double EulerConstant = 0.57721566490153286060651209008240243;

    /// <summary>Represents the ratio of two quantities being the same as the ratio of their sum to their maximum. (~1.618)</summary>
    /// <see href="https://en.wikipedia.org/wiki/Golden_ratio"/>
    public const double GoldenRatio = 1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374;

    /// <summary>Represents the square root of 2.</summary>
    public const double PythagorasConstant = 1.414213562373095048801688724209698078569671875376948073176679737990732478462;

    public const double RiemannZetaFunction2 = System.Math.PI * System.Math.PI / 6;

    /// <summary>Represents the square root of 3.</summary>
    public const double TheodorusConstant = 1.732050807568877293527446341505872366942805253810380628055806979451933016909;
  }
}