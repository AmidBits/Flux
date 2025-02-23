namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns whether the <see cref="System.IComparable{T}"/> <paramref name="source"/> (<typeparamref name="T"/>) is between <paramref name="start"/> and <paramref name="end"/>.</para>
    /// </summary>
    public static bool IsBetween<T>(this T source, T start, T end)
      where T : System.IComparable<T>
      => start.CompareTo(end) <= 0
      ? source.CompareTo(start) >= 0 && source.CompareTo(end) <= 0
      : source.CompareTo(end) >= 0 || source.CompareTo(start) <= 0;
  }
}
