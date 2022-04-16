namespace Flux
{
  public static partial class IListEm
  {
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Collections.Generic.IList<T> source)
      => AsSpan(source);
  }
}
