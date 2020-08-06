namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    public static bool EndsWith(this System.Text.StringBuilder source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));
      else if (source.Length < value.Length) return false;

      for (int indexOfSource = source.Length - 1, indexOfValue = value.Length - 1; indexOfSource >= 0 && indexOfValue >= 0; indexOfSource--, indexOfValue--)
      {
        if (!comparer.Equals(source[indexOfSource], value[indexOfValue]))
        {
          return false;
        }
      }

      return true;
    }
    public static bool EndsWith(this System.Text.StringBuilder source, string value)
      => EndsWith(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    public static bool StartsWith(this System.Text.StringBuilder source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));
      else if (source.Length < value.Length) return false;

      for (var index = 0; index < value.Length; index++)
      {
        if (!comparer.Equals(source[index], value[index]))
        {
          return false;
        }
      }

      return true;
    }
    public static bool StartsWith(this System.Text.StringBuilder source, string value)
       => EndsWith(source, value, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
