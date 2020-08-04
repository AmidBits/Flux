namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Trim all repeated characters in the string.</summary>
    public static System.Text.StringBuilder TrimRepeats(this System.Text.StringBuilder source)
    {
      var index = 0;
      var previous = '\0';

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if (current != previous)
        {
          source[index++] = current;

          previous = current;
        }
      }

      return source.Remove(index, source.Length - index);
    }

    /// <summary>Trim aany repeated combination of the specified characters in the string.</summary>
    public static System.Text.StringBuilder TrimRepeats(this System.Text.StringBuilder source, params char[] characters)
    {
      var index = 0;
      var previous = '\0';

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if (current != previous || !System.Array.Exists(characters, c => c == current))
        {
          source[index++] = current;

          previous = current;
        }
      }

      return source.Remove(index, source.Length - index);
    }
  }
}
