namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.List{T}"/> with all elements from <paramref name="source"/>.</para>
    /// </summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, int additionalCapacity = 0)
    {
      var target = new System.Collections.Generic.List<T>(source.Length + additionalCapacity);
      target.AddRange(source);
      return target;
    }
  }
}
