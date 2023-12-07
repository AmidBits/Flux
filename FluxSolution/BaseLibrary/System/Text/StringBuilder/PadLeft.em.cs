namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public static System.Text.StringBuilder PadLeft(this System.Text.StringBuilder source, int totalWidth, char padding)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, padding.ToString(), totalWidth - source.Length);
    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public static System.Text.StringBuilder PadLeft(this System.Text.StringBuilder source, int totalWidth, System.ReadOnlySpan<char> padding)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      while (source.Length < totalWidth)
        source.Insert(0, padding);

      source.Remove(0, source.Length - totalWidth);

      return source;
    }
  }
}
