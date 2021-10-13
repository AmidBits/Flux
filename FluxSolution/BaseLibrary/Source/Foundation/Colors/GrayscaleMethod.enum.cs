namespace Flux.Colors
{
  public enum GrayscaleMethod
  {
    /// <summary>Plain average of all colors.</summary>
    Average,
    /// <summary>Averages the most prominent and least prominent colors.</summary>
    Lightness,
    /// <summary>Weighted average based on human perception.</summary>
    Luminosity
  }
}
