namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Span<T> ToSpan<T>(this System.Collections.Generic.List<T> list)
      => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(list);
  }
}
