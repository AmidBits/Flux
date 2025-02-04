namespace Flux.Colors
{
  public enum GrayscaleMethod
  {
    /// <summary>Plain average of all colors.</summary>
    Average,
    ///// <summary>Averages the most prominent and least prominent colors.</summary>
    //Lightness,
    /// <summary>
    /// <para>A weighted average based on human perception.</para>
    /// <para>The ITU-R BT.601 formula (also known as PAL/NTSC formula) utilizes the formula: <code>0.30 * Red + 0.59 * Green + 0.11 * Blue</code></para>
    /// </summary>
    Luminosity601,
    /// <summary>
    /// <para>A second weighted average based on human perception.</para>
    /// <para>The ITU-R BT.709 formula (also known as HDTV formula), which applies different weights to the color channels according to the formula: <code>0.21 * Red + 0.72 * Green + 0.07 * Blue</code></para>
    /// </summary>
    Luminosity709,
  }
}
