namespace Flux
{
  public static partial class Math
  {
    /// <summary>Generates a pulse wave using radians [0, PI2].</summary>
    public static double PulseWave(double phasePi2, double pulseWidthPi2 = System.Math.PI)
      => phasePi2 < pulseWidthPi2 ? 1 : -1;

    /// <summary>Generates a sawtooth wave using radians [0, PI2].</summary>
    public static double SawtoothWave(double phasePi2)
      => 1 - phasePi2 / System.Math.PI;

    /// <summary>Generates a sine wave using radians [0, PI2].</summary>
    public static double SineWave(double phasePi2)
      => System.Math.Sin(phasePi2);

    /// <summary>Generates a square wave using radians [0, PI2].</summary>
    public static double SquareWave(double phasePi2)
      => phasePi2 < System.Math.PI ? 1 : -1;

    /// <summary>Generates a triangle wave using radians [0, PI2].</summary>
    public static double TriangleWave(double phasePi2)
      => phasePi2 < System.Math.PI ? phasePi2 / PiOver2 - 1 : 3 - phasePi2 / PiOver2;

    private static readonly System.Random m_rngWhiteNoise = new Random.SplitMix64();
    public static double WhiteNoise()
      => 1 - 2 * m_rngWhiteNoise.NextDouble();
  }
}
