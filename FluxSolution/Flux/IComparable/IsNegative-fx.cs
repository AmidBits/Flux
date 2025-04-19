namespace Flux
{
  public static partial class IComparables
  {
    public static int CompareToDefault<T>(this T source)
      where T : System.IComparable<T>
      => System.Collections.Generic.Comparer<T>.Default.Compare(source, default).Sign();
  }
}
