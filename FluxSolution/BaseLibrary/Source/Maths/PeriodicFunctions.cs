namespace Flux
{
  public struct WaveForm
  {
    public double m_amplitude { get; }

    public double m_period { get; }

    public double m_periodMultiplierFor2PI { get; }

    public double m_periodOver2 { get; }
    public double m_periodOver4 { get; }
    public double m_periodOver8 { get; }

    public double m_phase;
    public double Phase { get => m_phase; set => m_phase = ConstrainToPeriod(m_phaseOffset + value); }

    public double m_phaseOffset;

    public double m_pulseWidth;
    public double PulseWidth { get => m_pulseWidth; set => m_pulseWidth = ConstrainToPeriod(value); }

    public WaveForm(double amplitude, double period, double phaseOffset)
    {
      m_amplitude = amplitude;

      m_period = period;

      m_periodMultiplierFor2PI = 1 / (m_period / Maths.PiX2);

      m_periodOver2 = m_period / 2;
      m_periodOver4 = m_period / 4;
      m_periodOver8 = m_period / 8;

      m_phase = 0;

      m_phaseOffset = phaseOffset;

      m_pulseWidth = m_periodOver2;
    }

    public double ConstrainToPeriod(double value)
      => Maths.Wrap(value, 0, m_period) % m_period;

    /// <summary>Generates a pulse wave.</summary>
    public double PulseWave
      => m_phase < m_pulseWidth ? m_amplitude : -m_amplitude;
  
    /// <summary>Generates a sawtooth wave.</summary>
    public double SawWave
      => (1 - m_phase / m_periodOver2) * m_amplitude;
 
    /// <summary>Generates a sine wave.</summary>
    public double SineWave
      => System.Math.Sin(m_phase * m_periodMultiplierFor2PI) * m_amplitude;
 
    /// <summary>Generates a square wave.</summary>
    public double SquareWave
      => m_phase < m_periodOver2 ? m_amplitude : -m_amplitude;
  
    /// <summary>Generates a triangle wave.</summary>
    public double TriangleWave
      => (m_phase < m_periodOver2 ? m_phase / m_periodOver4 - 1 : 3 - m_phase / m_periodOver4) * m_amplitude;

    public double WhiteNoise
      => (1 - 2 * Randomization.NumberGenerator.Crypto.NextDouble()) * m_amplitude;
  }

  public static partial class Maths
  {
    #region Periodic waves with phase [0, 2PI)
    public static double ConstrainPhasePi2(double phasePi2)
      => Wrap(phasePi2, 0, PiX2) % PiX2;

    /// <summary>Generates a pulse wave using radians [0, PI2].</summary>
    public static double PulseWavePi2(double phasePi2, double pulseWidthPi2 = System.Math.PI)
      => ConstrainPhasePi2(phasePi2) < ConstrainPhasePi2(pulseWidthPi2)
      ? 1
      : -1;
    /// <summary>Generates a sawtooth wave using radians [0, PI2].</summary>
    public static double SawtoothWavePi2(double phasePi2)
      => 1 - ConstrainPhasePi2(phasePi2) / System.Math.PI;
    /// <summary>Generates a sine wave using radians [0, PI2].</summary>
    public static double SineWavePi2(double phasePi2)
      => System.Math.Sin(phasePi2);
    /// <summary>Generates a square wave using radians [0, PI2].</summary>
    public static double SquareWavePi2(double phasePi2)
      => ConstrainPhasePi2(phasePi2) < System.Math.PI
      ? 1
      : -1;
    /// <summary>Generates a triangle wave using radians [0, PI2].</summary>
    public static double TriangleWavePi2(double phasePi2)
      => ConstrainPhasePi2(phasePi2) is var phase && phase < System.Math.PI
      ? phase / PiOver2 - 1
      : 3 - phase / PiOver2;
    #endregion Periodic waves with phase [0, 2PI)

    #region Periodic waves with phase [0, 1)
    public static double ConstrainPhase1(double phase1)
      => Wrap(phase1, 0, 1) % 1;

    /// <summary>Generates a pulse wave using unit interval [0, 1].</summary>
    public static double PulseWave1(double phase1, double pulseWidth1 = 0.5)
      => ConstrainPhase1(phase1) < ConstrainPhase1(pulseWidth1)
      ? 1
      : -1;
    /// <summary>Generates a sawtooth wave using unit interval [0, 1].</summary>
    public static double SawtoothWave1(double phase1)
      => 1 - ConstrainPhase1(phase1) / 0.5;
    /// <summary>Generates a sine wave using unit interval [0, 1].</summary>
    public static double SineWave1(double phase1)
      => System.Math.Sin(phase1 * PiX2);
    /// <summary>Generates a square wave using unit interval [0, 1].</summary>
    public static double SquareWave1(double phase1)
      => ConstrainPhase1(phase1) < 0.5
      ? 1
      : -1;
    /// <summary>Generates a triangle wave using unit interval [0, 1].</summary>
    public static double TriangleWave1(double phase1)
      => ConstrainPhase1(phase1) is var phase && phase < 0.5
      ? phase / 0.25 - 1
      : 3 - phase / 0.25;
    #endregion Periodic waves with phase [0, 1)

    public static double WhiteNoise()
      => 1 - 2 * Randomization.NumberGenerator.Crypto.NextDouble();
  }
}
