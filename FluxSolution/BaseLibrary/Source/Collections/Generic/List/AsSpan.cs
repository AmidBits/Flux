namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Span<T> AsSpan<T>(this System.Collections.Generic.List<T> list)
      => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(list);
  }
}
