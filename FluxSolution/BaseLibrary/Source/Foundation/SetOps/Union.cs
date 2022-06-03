namespace Flux
{
  public static partial class SetOps
  {
    public static System.Text.Rune UnicodeUnion => (System.Text.Rune)0x222A;

    /// <summary>Creates a new sequence of all (unique) elements from both the source set and the specified target set.</summary>
    public static System.Collections.Generic.IEnumerable<T> Union<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      var hs = new System.Collections.Generic.HashSet<T>(source);

      foreach (var te in target)
        if (!hs.Contains(te))
          hs.Add(te);

      return hs;
    }
  }
}
