﻿namespace Flux.Dsp.WaveGenerator
{
  /// <see href="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables#Square_wave"/>
  public record class PulseWave
    : IMonoWaveUiGeneratable, IMonoWavePi2Generatable
  {
    private double m_dutyCycle;
    /// <summary>The duty cycle (pulse width) in the range [0, 2PI].</summary>
    public double DutyCycle { get => m_dutyCycle; set => m_dutyCycle = value.WrapAround(0.0, 1.0); }

    public PulseWave(double dutyCycle)
      => m_dutyCycle = dutyCycle.WrapAround(0.0, 1.0);
    public PulseWave()
      : this(0.5)
    { }

    public IWaveMono<double> GenerateMonoWaveUi(double phaseUi)
      => (WaveMono<double>)SampleUi(phaseUi, m_dutyCycle);
    public IWaveMono<double> GenerateMonoWavePi2(double phasePi2)
      => (WaveMono<double>)SamplePi2(phasePi2, m_dutyCycle);

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
  }
}
