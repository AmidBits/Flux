namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Returns a null, if the string is empty.</summary>
    public static string? NullIfEmpty(this string? source)
      => string.IsNullOrEmpty(source) ? null : source;
  }
}
