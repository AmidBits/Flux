namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns the index of the last character in the string. If not found a -1 is returned.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params char[] values)
    {
      foreach (var value in values)
        if (source.LastIndexOf(value) is var index && index > -1)
          return index;

      return -1;
    }

    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, Flux.StringComparer comparer, params string[] values)
    {
      foreach (var value in values)
        if (source.LastIndexOf(value, comparer) is var index && index > -1)
          return index;

      return -1;
    }
  }
}
