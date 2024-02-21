namespace Flux
{
  public static partial class Fx
  {
    /// <summary>In-place swap of two elements by the specified indices.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static void Swap<T>(this System.Collections.Generic.IList<T> source, int indexA, int indexB)
    {
      if (indexA != indexB) // No need to do anything, if the indices are the same.
      {
        System.ArgumentNullException.ThrowIfNull(source);

        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);
      }
    }
  }
}
