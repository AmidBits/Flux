namespace Flux
{
  public static partial class Maths
  {
    /// <summary>The 'machine' epsilon, constant for System.Single, from the 32-bit float CPP definition of epsilon.</summary>
    public const float EpsilonCpp32 = 1.1920929E-07F; // System.Math.Exp(-23 * System.Math.Log(2)); System.Math.Pow(2, -23);
    /// <summary>A C# epsilon for System.Single.</summary>
    public const float Epsilon1E7 = 1E-7f;

    /// <summary>The 'machine' epsilon, constant for System.Double, from the 64-bit double CPP definition of epsilon.</summary>
    public const double EpsilonCpp64 = 2.2204460492503131E-016; // System.Math.Exp(-52 * System.Math.Log(2)); System.Math.Pow(2, -52);
    /// <summary>A C# epsilon for System.Double.</summary>
    public const double Epsilon1E15 = 1E-15d;

    public static double ComputeMachineEpsilonNegative()
    {
      var epsilon = 1d;

      while (epsilon / 2d is var half && (1d - half) < 1d)
        epsilon = half;

      return epsilon;
    }
    public static double ComputeMachineEpsilonPositive()
    {
      var epsilon = 1d;

      while (epsilon / 2d is var half && (1d + half) > 1d)
        epsilon = half;

      return epsilon;
    }
  }
}
