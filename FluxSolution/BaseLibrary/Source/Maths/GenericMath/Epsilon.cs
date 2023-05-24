namespace Flux
{
  public static partial class GenericMath
  {
    public static readonly double DoubleEpsilonNegative = ComputeNegativeEpsilonDouble();
    public static readonly double DoubleEpsilonPositive = ComputePositiveEpsilonDouble();

    public static readonly float SingleEpsilonNegative = ComputeNegativeEpsilonSingle();
    public static readonly float SingleEpsilonPositive = ComputePositiveEpsilonSingle();

    ///// <summary>The 'machine' epsilon, constant for System.Single, from the 32-bit float CPP definition of epsilon.</summary>
    //public const float EpsilonCpp32 = 1.1920929E-07F; // System.Math.Exp(-23 * System.Math.Log(2)); System.Math.Pow(2, -23);
    ///// <summary>A C# epsilon for System.Single.</summary>
    //public const float Epsilon1E7 = 1E-7f;

    ///// <summary>The 'machine' epsilon, constant for System.Double, from the 64-bit double CPP definition of epsilon.</summary>
    //public const double EpsilonCpp64 = 2.2204460492503131E-016; // System.Math.Exp(-52 * System.Math.Log(2)); System.Math.Pow(2, -52);
    ///// <summary>A C# epsilon for System.Double.</summary>
    //public const double Epsilon1E15 = 1E-15d;

    /// <summary>Compute (machine) epsilon in negative direction.</summary>
    public static double ComputeNegativeEpsilonDouble()
    {
      var epsilonNegative = -1d;

      while (epsilonNegative / 2 is var halfEpsilonNegative && (-1 + halfEpsilonNegative) < -1)
        epsilonNegative = halfEpsilonNegative;

      return epsilonNegative;
    }
    /// <summary>Compute (machine) epsilon in negative direction.</summary>
    public static float ComputeNegativeEpsilonSingle()
    {
      var epsilonNegative = -1f;

      while (epsilonNegative / 2 is var halfEpsilonNegative && (-1 + halfEpsilonNegative) < -1)
        epsilonNegative = halfEpsilonNegative;

      return epsilonNegative;
    }

    /// <summary>Compute (machine) epsilon in positive direction.</summary>
    public static double ComputePositiveEpsilonDouble()
    {
      var epsilonPositive = 1d;

      while (epsilonPositive / 2 is var halfEpsilonPositive && (1 + halfEpsilonPositive) > 1)
        epsilonPositive = halfEpsilonPositive;

      return epsilonPositive;
    }
    /// <summary>Compute (machine) epsilon in positive direction.</summary>
    public static float ComputePositiveEpsilonSingle()
    {
      var epsilonPositive = 1f;

      while (epsilonPositive / 2 is var halfEpsilonPositive && (1 + halfEpsilonPositive) > 1)
        epsilonPositive = halfEpsilonPositive;

      return epsilonPositive;
    }
  }
}
