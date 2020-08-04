namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    public static bool StartsWith(this System.Text.StringBuilder source, string value)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));
      else if (source.Length < value.Length) return false;

      for (var index = 0; index < value.Length; index++)
      {
        if (source[index] != value[index])
        {
          return false;
        }
      }

      return true;
    }
  }
}
