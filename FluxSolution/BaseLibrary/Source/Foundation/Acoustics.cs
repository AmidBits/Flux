namespace Flux
{
  public static class Acoustics
  {
    /// <summary>Returns the frequency in Hertz (Hz) from the specified wavelength and sound velocity.</summary>
    public static double GetFrequency(double waveLength, double soundVelocity)
      => (soundVelocity / waveLength);
    /// <summary>The wavelength is the spatial period of a periodic wave, i.e. the distance over which the wave's shape repeats. The default reference value for the speed of sound is 343.21 m/s. This determines the unit of measurement (i.e. meters per seond) for the wavelength distance.</summary>
    /// <returns>The wave length in the unit specified (default is in meters per second, i.e. 343.21 m/s).</returns>
    public static double GetWaveLength(double soundVelocity, double frequency)
      => (soundVelocity / frequency);
    /// <summary>Returns the sound velocity in meters per second (m/s) from the specified frequency and wavelength.</summary>
    public static double GetSoundVelocity(double frequency, double waveLength)
      => (frequency * waveLength);
  }
}
