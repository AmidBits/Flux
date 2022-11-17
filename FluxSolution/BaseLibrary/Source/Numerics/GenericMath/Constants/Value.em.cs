namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="source"/> (enum of <see cref="MathematicalConstant"/>) as a double.</summary>
    public static double Value(this MathematicalConstant source)
        => source switch
        {
          MathematicalConstant.C10 => 0.123456789101112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555657585960,
          MathematicalConstant.DoubleEpsilon => 1E-15,
          MathematicalConstant.EarthNullGravityMeterPerSecond => 9.80665,
          MathematicalConstant.EulerConstant => 0.57721566490153286060651209008240243,
          MathematicalConstant.RiemannZetaFunction2 => System.Math.PI * System.Math.PI / 6,
          MathematicalConstant.SingleEpsilon => 1E-7,
          _ => throw new System.ArgumentOutOfRangeException(nameof(source))
        };
  }
}
