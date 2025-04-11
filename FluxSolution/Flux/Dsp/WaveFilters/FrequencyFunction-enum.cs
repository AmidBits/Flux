namespace Flux.Dsp.WaveFilters
{
  /// <see href="https://en.wikipedia.org/wiki/Filter_design"/>
  public enum FrequencyFunction
  {
    /// <summary>Uninitialized. This is not a filter type, but a filter instance default state of being uninitialized.</summary>
    None = -1,
    /// <summary>A low-pass filter is used to cut unwanted high-frequency signals.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Low-pass_filter"/>
    LowPass = 0, // Must retain the value of 0 for varoius past filter implementations.
    /// <summary>A high-pass filter passes high frequencies fairly well; it is helpful as a filter to cut any unwanted low-frequency components.</summary>
    /// <see href="https://en.wikipedia.org/wiki/High-pass_filter"/>
    HighPass = 1, // Must retain the value of 1 for varoius past filter implementations.
    /// <summary>An all-pass filter passes through all frequencies unchanged, but changes the phase of the signal. Filters of this type can be used to equalize the group delay of recursive filters. This filter is also used in phaser effects.</summary>
    /// <see href="https://en.wikipedia.org/wiki/All-pass_filter"/>
    AllPass,
    /// <summary>A bell shaped filter.</summary>
    Bell,
    /// <summary>A band-pass filter passes a limited range of frequencies.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Band-pass_filter"/>
    BandPass,
    /// <summary>A high-shelf filter passes all frequencies, but increases or reduces frequencies above the shelf frequency by specified amount.</summary>
    HighShelf,
    /// <summary>A low-shelf filter passes all frequencies, but increases or reduces frequencies below the shelf frequency by specified amount.</summary>
    LowShelf,
    /// <summary>A band-stop filter passes frequencies above and below a certain range. A very narrow band-stop filter is known as a notch filter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Band-stop_filter"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Band-stop_filter"/>
    Notch,
    /// <summary>A peak filter makes a peak or a dip in the frequency response, commonly used in parametric equalizers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Band-pass_filter"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Equalization_(audio)"/>
    Peak,
  }
}
