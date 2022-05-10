namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Collections.Generic.IList<T> source)
      => AsSpan(source);
  }
}
