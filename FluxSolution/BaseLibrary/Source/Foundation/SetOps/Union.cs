namespace Flux
{
  public static partial class SetOps
  {
    public static System.Text.Rune UnicodeUnion => (System.Text.Rune)0x222A;

    /// <summary>Creates a new sequence of all (unique) elements from both the source set and the specified target set.</summary>
    public static System.Collections.Generic.HashSet<T> Union<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var shs = new System.Collections.Generic.HashSet<T>(source, equalityComparer);

      foreach (var te in target)
        if (!shs.Contains(te))
          shs.Add(te);

      return shs;
    }
    public static System.Collections.Generic.HashSet<T> Union<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => Union(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
