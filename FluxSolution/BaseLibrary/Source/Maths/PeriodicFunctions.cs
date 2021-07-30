namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Generates a pulse wave using radians [0, PI2].</summary>
    public static double PulseWavePi2(double phasePi2, double pulseWidthPi2 = System.Math.PI)
      => Wrap(phasePi2, 0, PiX2) % PiX2 < Wrap(pulseWidthPi2, 0, PiX2) % PiX2
      ? 1
      : -1;
    /// <summary>Generates a sawtooth wave using radians [0, PI2].</summary>
    public static double SawtoothWavePi2(double phasePi2)
      => 1 - Wrap(phasePi2, 0, PiX2) % PiX2 / System.Math.PI;
    /// <summary>Generates a sine wave using radians [0, PI2].</summary>
    public static double SineWavePi2(double phasePi2)
      => System.Math.Sin(phasePi2);
    /// <summary>Generates a square wave using radians [0, PI2].</summary>
    public static double SquareWavePi2(double phasePi2)
      => Wrap(phasePi2, 0, PiX2) % PiX2 < System.Math.PI
      ? 1
      : -1;
    /// <summary>Generates a triangle wave using radians [0, PI2].</summary>
    public static double TriangleWavePi2(double phasePi2)
      => PhasePi2(phasePi2) is var phase && phase < System.Math.PI
      ? phase / PiOver2 - 1
      : 3 - phase / PiOver2;

    public static double PhasePi2(double phasePi2, double constrainedPhase)
      => constrainedPhase = Wrap(phasePi2, 0, PiX2) % PiX2;

    public static double WhiteNoise()
      => 1 - 2 * Randomization.NumberGenerator.Crypto.NextDouble();

    /// <summary>Generates a pulse wave using unit interval [0, 1].</summary>
    public static double PulseWave1(double phase1, double pulseWidth1 = 0.5)
      => Wrap(phase1, 0, 1) % 1 < Wrap(pulseWidth1, 0, 1) % 1
      ? 1
      : -1;
    /// <summary>Generates a sawtooth wave using unit interval [0, 1].</summary>
    public static double SawtoothWave1(double phase1)
      => 1 - Wrap(phase1, 0, 1) % 1 / 0.5;
    /// <summary>Generates a sine wave using unit interval [0, 1].</summary>
    public static double SineWave1(double phase1)
      => System.Math.Sin(phase1 * PiX2);
    /// <summary>Generates a square wave using unit interval [0, 1].</summary>
    public static double SquareWave1(double phase1)
      => Wrap(phase1, 0, 1) % 1 < 0.5
      ? 1
      : -1;
    /// <summary>Generates a triangle wave using unit interval [0, 1].</summary>
    public static double TriangleWave1(double phase1)
      //=> Wrap(phase1, 0, 1) % 1 is var phase && phase < 0.5
      => Phase1(phase1) is var phase && phase < 0.5
      ? phase / 0.25 - 1
      : 3 - phase / 0.25;

    public static double Phase1(double phase1)
      => Wrap(phase1, 0, 1) % 1;
  }
}
