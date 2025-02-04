namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, char padding)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.Append(padding, totalWidth - source.Length);
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, System.ReadOnlySpan<char> padding)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      while (source.Length < totalWidth)
        source.Append(padding);

      source.Remove(totalWidth, source.Length - totalWidth);

      return source;
    }
  }
}
