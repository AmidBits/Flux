namespace Flux.Dsp.AudioProcessor
{
  public enum MonoInverterMode
  {
    Bypass,
    /// <summary>Invert the negative polarity only.</summary>
    NegativePeekOnly,
    /// <summary>Invert each polarity independently of each other.</summary>
    PeeksIndependently,
    /// <summary>Invert across both polarities.</summary>
    PeekToPeek,
    /// <summary>Invert the positive polarity only.</summary>
    PositivePeekOnly
  }
}
