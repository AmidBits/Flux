namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>In-place swap of two elements by the specified indices.</summary>
    public static void Swap<T>(this System.Collections.Generic.IList<T> source, int indexA, int indexB)
    {
      if (source is null) throw new System.ArgumentOutOfRangeException(nameof(source));
      if (source.Count == 0) throw new System.ArgumentException(@"The sequence is empty.");
      if (indexA < 0 || indexA >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(indexA));
      if (indexB < 0 || indexB >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(indexB));

      if (indexA != indexB) // No need to actually swap if the indices are the same.
        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);
    }
  }
}
