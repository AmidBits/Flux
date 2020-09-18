namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Generates a pulse wave using radians [0, PI2].</summary>
    public static double PulseWave(double phasePi2, double pulseWidthPi2 = System.Math.PI)
      => phasePi2 < pulseWidthPi2 ? 1d : -1d;

    /// <summary>Generates a sawtooth wave using radians [0, PI2].</summary>
    public static double SawtoothWave(double phasePi2)
      => 1d - phasePi2 / System.Math.PI;

    /// <summary>Generates a sine wave using radians [0, PI2].</summary>
    public static double SineWave(double phasePi2)
      => System.Math.Sin(phasePi2);

    /// <summary>Generates a square wave using radians [0, PI2].</summary>
    public static double SquareWave(double phasePi2)
      => phasePi2 < System.Math.PI ? 1d : -1d;

    /// <summary>Generates a triangle wave using radians [0, PI2].</summary>
    public static double TriangleWave(double phasePi2)
      => phasePi2 < System.Math.PI ? phasePi2 / PiOver2 - 1d : 3d - phasePi2 / PiOver2;

    public static double WhiteNoise()
      => 1d - 2d * Flux.Random.NumberGenerator.Crypto.NextDouble();
  }
}
