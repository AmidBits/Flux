namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static void SwapImpl<T>(this System.Span<T> source, int indexA, int indexB)
    {
      var tmp = source[indexA];
      source[indexA] = source[indexB];
      source[indexB] = tmp;
    }

    /// <summary>Swap two elements by the specified indices.</summary>
    public static void Swap<T>(this System.Span<T> source, int indexA, int indexB)
    {
      if (source.Length == 0) throw new System.ArgumentException(@"The sequence is empty.");
      if (indexA < 0 || indexA >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexA));
      if (indexB < 0 || indexB >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexB));

      if (indexA != indexB) // No need to actually swap if the indices are the same.
        SwapImpl(source, indexA, indexB);
    }

    public static void SwapFirstWith<T>(this System.Span<T> source, int index)
    {
      if (index <= 0 && index >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      SwapImpl(source, 0, index);
    }

    public static void SwapLastWith<T>(this System.Span<T> source, int index)
    {
      var lastIndex = source.Length - 1;

      if (index < 0 && index >= lastIndex) throw new System.ArgumentOutOfRangeException(nameof(index));

      SwapImpl(source, index, lastIndex);
    }
  }
}
