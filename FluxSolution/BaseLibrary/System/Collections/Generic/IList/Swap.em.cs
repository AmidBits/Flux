namespace Flux
{
  public static partial class ExtensionMethodsList
  {
    /// <summary>In-place swap of two elements by the specified indices.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static void Swap<T>(this System.Collections.Generic.IList<T> source, int indexA, int indexB)
    {
      if (indexA != indexB) // No need to do anything, if the indices are the same.
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);
      }
    }
  }
}