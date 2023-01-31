namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.ReadOnlySpan<T> Intersect<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var intersection = new System.Collections.Generic.List<T>();

      var set = new HashSet<T>(equalityComparer);

      foreach (var element in target)
        set.Add(element);

      foreach (var element in source)
        if (set.Remove(element))
          intersection.Add(element);

      return intersection.AsSpan();
    }
  }
}
