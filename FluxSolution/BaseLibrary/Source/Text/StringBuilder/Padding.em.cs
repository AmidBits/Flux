namespace Flux
{
  public static partial class StringBuilderEm
  {
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public static System.Text.StringBuilder PadEven(this System.Text.StringBuilder source, int totalWidth, char paddingLeft, char paddingRight)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (totalWidth > source.Length)
      {
        PadLeft(source, source.Length + (totalWidth - source.Length) / 2, paddingLeft);
        PadRight(source, totalWidth, paddingRight);
      }

      return source;
    }
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
    public static System.Text.StringBuilder PadEven(this System.Text.StringBuilder source, int totalWidth, string paddingLeft, string paddingRight)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (totalWidth > source.Length)
      {
        PadLeft(source, source.Length + (totalWidth - source.Length) / 2, paddingLeft);
        PadRight(source, totalWidth, paddingRight);
      }

      return source;
    }

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public static System.Text.StringBuilder PadLeft(this System.Text.StringBuilder source, int totalWidth, char padding)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, padding.ToString(), totalWidth - source.Length);
    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public static System.Text.StringBuilder PadLeft(this System.Text.StringBuilder source, int totalWidth, string padding)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (padding is null) throw new System.ArgumentNullException(nameof(padding));

      source.Insert(0, padding, (totalWidth - source.Length) / padding.Length);
      source.Insert(0, padding.Substring(padding.Length - (totalWidth - source.Length)));

      return source;
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, char padding)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Append(padding, totalWidth - source.Length);
    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, string padding)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (padding is null) throw new System.ArgumentNullException(nameof(padding));

      source.Insert(source.Length, padding, (totalWidth - source.Length) / padding.Length);
      source.Append(padding, 0, totalWidth - source.Length);

      return source;
    }
  }
}
