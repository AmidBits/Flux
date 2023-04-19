namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Represents the Champernowne constant. A transcendental real constant whose decimal expansion has important properties.</summary>
    public const double C10 = 0.123456789101112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555657585960;

#if NET7_0_OR_GREATER

    /// <summary>Computed on the fly.</summary>
    public static TSelf ComputeMachineEpsilon<TSelf>()
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var epsilon = TSelf.One;

      while (epsilon.Divide(2) is var half && (TSelf.One + half) > TSelf.One)
        epsilon = half;

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

    /// <summary>Presented in standard unit of meters per second.</summary>
    public const double EarthNullGravity = 9.80665;

    /// <summary>The 'machine' epsilon, constant for System.Single, from the 32-bit float CPP definition of epsilon.</summary>
    /// <remarks>System.Math.Exp(-23 * System.Math.Log(2)); System.Math.Pow(2, -23);</remarks>
    public const float EpsilonCpp32 = 1.1920929E-07F;
    /// <summary>The 'machine' epsilon, constant for System.Double, from the 64-bit double CPP definition of epsilon.</summary>
    /// <remarks>System.Math.Exp(-52 * System.Math.Log(2)); System.Math.Pow(2, -52);</remarks>
    public const double EpsilonCpp64 = 2.2204460492503131E-016;
    /// <summary>A C# epsilon for System.Single.</summary>
    public const float Epsilon1E7 = 1E-7f;
    /// <summary>A C# epsilon for System.Double.</summary>
    public const double Epsilon1E15 = 1E-15d;

    /// <summary>Represents mathematical constants.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Euler%27s_constant"/>
    public const double EulerConstant = 0.57721566490153286060651209008240243;

    /// <summary>Represents the ratio of two quantities being the same as the ratio of their sum to their maximum. (~1.618)</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Golden_ratio"/>
    public const double GoldenRatio = 1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374;

    /// <summary>Represents (180 / PI)</summary>
    public const double PiInto180 = 180 / System.Math.PI;
    /// <summary>Represents (2 / PI)</summary>
    public const double PiInto2 = 2 / System.Math.PI;
    /// <summary>Represents (200 / PI)</summary>
    public const double PiInto200 = 200 / System.Math.PI;

    /// <summary>Represents (PI / 180).</summary>
    public const double PiOver180 = System.Math.PI / 180;
    /// <summary>Represents (PI / 2)</summary>
    public const double PiOver2 = System.Math.PI / 2;
    /// <summary>Represents (PI / 200)</summary>
    public const double PiOver200 = System.Math.PI / 200;

    /// <summary>Represents (PI / 4)</summary>
    public const double PiOver4 = System.Math.PI / 4;

    /// <summary>Represents (PI * (4.0 / 3.0)).</summary>
    public const double PiTimesFourThirds = System.Math.PI * 4.0 / 3.0;

    /// <summary>Represents the square root of 2.</summary>
    public const double PythagorasConstant = 1.414213562373095048801688724209698078569671875376948073176679737990732478462;

    public const double RiemannZetaFunction2 = System.Math.PI * System.Math.PI / 6;

    /// <summary>Represents the square root of 3.</summary>
    public const double TheodorusConstant = 1.732050807568877293527446341505872366942805253810380628055806979451933016909;
  }
}
