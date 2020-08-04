namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    public static bool EndsWith(this System.Text.StringBuilder source, string value)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));
      else if (source.Length < value.Length) return false;

      for (int indexOfSource = source.Length - 1, indexOfValue = value.Length - 1; indexOfSource >= 0 && indexOfValue >= 0; indexOfSource--, indexOfValue--)
      {
        if (source[indexOfSource] != value[indexOfValue])
        {
          return false;
        }
      }

      return true;
    }
  }
}
