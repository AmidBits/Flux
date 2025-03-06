namespace Flux.Dsp.WaveProcessor
{
  public enum MonoRectifierMode
  {
    Bypass,
    /// <summary>Perform a positive full (the wave signal below the threshold is mirrored above the threshold) wave rectification.</summary>
    FullWave,
    /// <summary>Perform a positive half (the wave signal below the threshold is removed) wave rectification.</summary>
    HalfWave,
    /// <summary>Perform a negative full (the wave signal above the threshold is mirrored below the threshold) wave rectification.</summary>
    NegativeFullWave,
    /// <summary>Perform a negative half (the wave signal above the threshold is removed) wave rectification.</summary>
    NegativeHalfWave,
  }
}
