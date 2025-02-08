namespace Flux.Units
{
  [System.ComponentModel.DefaultValue(Second)]
  public enum TimeUnit
  {
    /// <summary>This is the default unit for <see cref="Time"/>.</summary>
    Second,
    /// <summary>
    /// <para>The unit of .NET ticks.</para>
    /// </summary>
    Tick,
    Minute,
    Hour,
    Day,
    Week,
    /// <summary>Represents the musical BPM.</summary>
    BeatPerMinute
  }
}
