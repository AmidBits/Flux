namespace Flux.Units
{
  public enum FlowUnit
  {
    /// <summary>This is the default unit for <see cref="Flow"/>.</summary>
    CubicMeterPerSecond,
    /// <summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sverdrup"/>
    /// </summary>
    /// <remarks>Not to be confused with <see cref="EquivalentDoseUnit.Sievert"/>, unit of <see cref="EquivalentDose"/>.</remarks>
    Sverdrup,
    USGallonPerMinute,
  }
}
