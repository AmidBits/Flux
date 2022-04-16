namespace Flux
{
  public static partial class ListEm
  {
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Collections.Generic.List<T> source)
      => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(source);
  }
}
