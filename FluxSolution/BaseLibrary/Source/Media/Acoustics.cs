namespace Flux.Media
{
  public static class Acoustics
  {
    /// <summary>Returns the frequency in Hertz (Hz) from the specified wavelength and sound velocity.</summary>
    public static double GetFrequency(double waveLength, double soundVelocity)
      => (soundVelocity / waveLength);
    /// <summary>Returns the wavelength in meters (m) from the specified sound velocity and frequency.</summary>
    public static double GetWaveLength(double soundVelocity, double frequency)
      => (soundVelocity / frequency);
    /// <summary>Returns the sound velocity in meters per second (m/s) from the specified frequency and wavelength.</summary>
    public static double GetSoundVelocity(double frequency, double waveLength)
      => (frequency * waveLength);
  }
}
