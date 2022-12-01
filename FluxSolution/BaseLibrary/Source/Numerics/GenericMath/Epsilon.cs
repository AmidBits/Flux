namespace Flux
{
  public static partial class GenericMath
  {
    public static double DoubleEpsilonNegative => ComputeNegativeEpsilon<double>();
    public static double DoubleEpsilonPositive => ComputePositiveEpsilon<double>();

    public static float SingleEpsilonNegative => ComputeNegativeEpsilon<float>();
    public static float SingleEpsilonPositive => ComputePositiveEpsilon<float>();

    ///// <summary>The 'machine' epsilon, constant for System.Single, from the 32-bit float CPP definition of epsilon.</summary>
    //public const float EpsilonCpp32 = 1.1920929E-07F; // System.Math.Exp(-23 * System.Math.Log(2)); System.Math.Pow(2, -23);
    ///// <summary>A C# epsilon for System.Single.</summary>
    //public const float Epsilon1E7 = 1E-7f;

    ///// <summary>The 'machine' epsilon, constant for System.Double, from the 64-bit double CPP definition of epsilon.</summary>
    //public const double EpsilonCpp64 = 2.2204460492503131E-016; // System.Math.Exp(-52 * System.Math.Log(2)); System.Math.Pow(2, -52);
    ///// <summary>A C# epsilon for System.Double.</summary>
    //public const double Epsilon1E15 = 1E-15d;

    /// <summary>Compute (machine) epsilon in negtive direction.</summary>
    public static TSelf ComputeNegativeEpsilon<TSelf>()
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var epsilonNegative = -TSelf.One;

      while (epsilonNegative.Divide(2) is var halfEpsilonNegative && (-TSelf.One + halfEpsilonNegative) < -TSelf.One)
        epsilonNegative = halfEpsilonNegative;

      return epsilonNegative;
    }

    /// <summary>Compute (machine) epsilon in positive direction.</summary>
    public static TSelf ComputePositiveEpsilon<TSelf>()
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var epsilonPositive = TSelf.One;

      while (epsilonPositive.Divide(2) is var halfEpsilonPositive && (TSelf.One + halfEpsilonPositive) > TSelf.One)
        epsilonPositive = halfEpsilonPositive;

      return epsilonPositive;
    }
  }
}
