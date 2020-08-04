namespace Flux
{
  public static partial class Math
  {
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static System.Numerics.BigInteger Rescale(System.Numerics.BigInteger value, System.Numerics.BigInteger sourceMinimum, System.Numerics.BigInteger sourceMaximum, System.Numerics.BigInteger targetMinimum, System.Numerics.BigInteger targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static decimal Rescale(decimal value, decimal sourceMinimum, decimal sourceMaximum, decimal targetMinimum, decimal targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static float Rescale(float value, float sourceMinimum, float sourceMaximum, float targetMinimum, float targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static double Rescale(double value, double sourceMinimum, double sourceMaximum, double targetMinimum, double targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static int Rescale(int value, int sourceMinimum, int sourceMaximum, int targetMinimum, int targetMaximum)
      => (value >= sourceMinimum && value <= sourceMaximum && sourceMaximum > sourceMinimum) ? (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum : throw new System.ArgumentOutOfRangeException(nameof(value), $"The value ({value}) must be within the specified source range [{sourceMinimum}, {sourceMaximum}], and the range must greater than 0.");
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    public static long Rescale(long value, long sourceMinimum, long sourceMaximum, long targetMinimum, long targetMaximum)
      => (value >= sourceMinimum && value <= sourceMaximum && sourceMaximum > sourceMinimum) ? (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum : throw new System.ArgumentOutOfRangeException(nameof(value), $"The value ({value}) must be within the specified source range [{sourceMinimum}, {sourceMaximum}], and the range must greater than 0.");

    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    [System.CLSCompliant(false)]
    public static uint Rescale(uint value, uint sourceMinimum, uint sourceMaximum, uint targetMinimum, uint targetMaximum)
      => (value >= sourceMinimum && value <= sourceMaximum && sourceMaximum > sourceMinimum) ? (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum : throw new System.ArgumentOutOfRangeException(nameof(value), $"The value ({value}) must be within the specified source range [{sourceMinimum}, {sourceMaximum}], and the range must greater than 0.");
    /// <summary>Proportionally re-scale a value from within one range (between source minimum and source maximum) to within another (between target minimum and target maximum), i.e. a value retains its ratio from one range to another. E.g. a 5 in the range [0, 10] becomes 200 when rescaled to the range [100, 300].</summary>
    [System.CLSCompliant(false)]
    public static ulong Rescale(ulong value, ulong sourceMinimum, ulong sourceMaximum, ulong targetMinimum, ulong targetMaximum)
      => (value >= sourceMinimum && value <= sourceMaximum && sourceMaximum > sourceMinimum) ? (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum : throw new System.ArgumentOutOfRangeException(nameof(value), $"The value ({value}) must be within the specified source range [{sourceMinimum}, {sourceMaximum}], and the range must greater than 0.");
  }
}
