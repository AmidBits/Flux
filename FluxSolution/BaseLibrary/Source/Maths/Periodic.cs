namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Generates a pulse wave using radians [0, PI2].</summary>
    public static double PulseWave(double phasePi2, double pulseWidthPi2 = System.Math.PI)
      => Wrap(phasePi2, 0, PiX2) < Wrap(pulseWidthPi2, 0, PiX2)
      ? 1
      : -1;

    /// <summary>Generates a sawtooth wave using radians [0, PI2].</summary>
    public static double SawtoothWave(double phasePi2)
      => 1 - Wrap(phasePi2, 0, PiX2) / System.Math.PI;

    /// <summary>Generates a sine wave using radians [0, PI2].</summary>
    public static double SineWave(double phasePi2)
      => System.Math.Sin(phasePi2);

    /// <summary>Generates a square wave using radians [0, PI2].</summary>
    public static double SquareWave(double phasePi2)
      => Wrap(phasePi2, 0, PiX2) < System.Math.PI
      ? 1
      : -1;

    /// <summary>Generates a triangle wave using radians [0, PI2].</summary>
    public static double TriangleWave(double phasePi2)
      => Wrap(phasePi2, 0, PiX2) is var phase && phase < System.Math.PI
      ? phase / PiOver2 - 1
      : 3 - phase / PiOver2;

    public static double WhiteNoise()
      => 1 - 2 * Flux.Random.NumberGenerator.Crypto.NextDouble();
  }
}
