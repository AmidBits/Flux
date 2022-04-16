namespace Flux
{
  public static partial class ListEm
  {
    public static System.Span<T> AsSpan<T>(this System.Collections.Generic.List<T> list)
      => System.Runtime.InteropServices.CollectionsMarshal.AsSpan<T>(list);
  }
}
