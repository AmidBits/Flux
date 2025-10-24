﻿namespace Flux.Dsp.WaveGenerators
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
  public record class PulseWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private double m_dutyCycle;

    /// <summary>The duty cycle (pulse width) in the range [0, 2PI].</summary>
    public double DutyCycle { get => m_dutyCycle; set => m_dutyCycle = IntervalNotation.Closed.Wrap(value, 0.0, 1.0); }

    public PulseWave(double dutyCycle) => m_dutyCycle = IntervalNotation.Closed.Wrap(dutyCycle, 0.0, 1.0);

    public PulseWave() : this(0.5) { }

    /// <summary>Generates a pulse wave from a unit interval. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 1.</summary>
    public static double SampleUi(double phaseUi, double pulseWidthUi)
      => Tools.AbsolutePhaseUi(phaseUi) < Tools.AbsolutePhaseUi(pulseWidthUi)
      ? 1
      : -1;

    /// <summary>Generates a pulse wave using radians. Periodic function, with the domain [-infinity, infinity], the codomain [-1, 1], and period: 2PI.</summary>
    public static double SamplePi2(double phasePi2, double pulseWidthPi2)
      => Tools.AbsolutePhasePi2(phasePi2) < Tools.AbsolutePhasePi2(pulseWidthPi2)
      ? 1
      : -1;

    public Waves.IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (Waves.WaveMono<double>)SampleUi(phaseUi, m_dutyCycle);

    public Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (Waves.WaveMono<double>)SamplePi2(phasePi2, m_dutyCycle);
  }
}
