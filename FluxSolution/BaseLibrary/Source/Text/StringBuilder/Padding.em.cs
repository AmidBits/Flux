namespace Flux
{
  public static partial class XtendStringBuilder
  {
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public static System.Text.StringBuilder PadEven(this System.Text.StringBuilder source, int totalWidth, char paddingLeft, char paddingRight)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (totalWidth > source.Length)
      {
        source.PadLeft(source.Length + (totalWidth - source.Length) / 2, paddingLeft);
        source.PadRight(totalWidth, paddingRight);
      }

      return source;
    }
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
    public static System.Text.StringBuilder PadEven(this System.Text.StringBuilder source, int totalWidth, string paddingLeft, string paddingRight)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (totalWidth > source.Length)
      {
        source.PadLeft(source.Length + (totalWidth - source.Length) / 2, paddingLeft);
        source.PadRight(totalWidth, paddingRight);
      }

      return source;
    }

    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
    public static System.Text.StringBuilder PadLeft(this System.Text.StringBuilder source, int totalWidth, char paddingChar)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Insert(0, paddingChar.ToString(), totalWidth - source.Length);
    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
    public static System.Text.StringBuilder PadLeft(this System.Text.StringBuilder source, int totalWidth, string paddingString)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (paddingString is null) throw new System.ArgumentNullException(nameof(paddingString));

      source.Insert(0, paddingString, (totalWidth - source.Length) / paddingString.Length);
      source.Insert(0, paddingString.Substring(paddingString.Length - (totalWidth - source.Length)));

      return source;
    }

    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
    public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, char paddingChar)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Append(paddingChar, totalWidth - source.Length);
    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
    public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, string paddingString)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (paddingString is null) throw new System.ArgumentNullException(nameof(paddingString));

      while (source.Length + paddingString.Length < totalWidth) source.Append(paddingString);
      source.Append(paddingString, 0, totalWidth - source.Length);

      return source;
    }
  }
}
