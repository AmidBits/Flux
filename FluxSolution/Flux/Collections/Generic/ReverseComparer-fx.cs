namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.Comparer<T> CreateReverseComparer<T>(this System.Collections.Generic.IComparer<T> source)
      => new Collections.Generic.ReverseComparer<T>(source);
  }
}
