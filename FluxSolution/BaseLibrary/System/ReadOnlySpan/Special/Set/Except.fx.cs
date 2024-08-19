namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.Collections.Generic.HashSet<T> Except<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int additionalCapacity = 0)
    {
      if (source == target || source.Length == 0) return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // A set minus itself or if source is empty is an empty set.

      var except = source.ToHashSet(equalityComparer, additionalCapacity);

      if (target.Length > 0) except.RemoveSpan(target); // If target is empty, source is the result.

      return except;
    }
  }
}
