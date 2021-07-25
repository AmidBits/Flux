namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(this System.Collections.Generic.List<T> source)
      => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(source);
  }
}
