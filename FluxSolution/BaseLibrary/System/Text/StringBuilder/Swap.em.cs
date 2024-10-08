namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Swap two elements by the specified indices.</summary>
    internal static void SwapImpl(this System.Text.StringBuilder source, int indexA, int indexB)
    {
      if (indexA != indexB)
        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);
    }
    /// <summary>Swap two elements by the specified indices.</summary>
    public static void Swap(this System.Text.StringBuilder source, int indexA, int indexB)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Length == 0)
        throw new System.ArgumentException(@"The sequence is empty.");
      else if (indexA < 0 || indexA >= source.Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexA));
      else if (indexB < 0 || indexB >= source.Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexB));
      else
        SwapImpl(source, indexA, indexB);
    }

    public static void SwapFirstWith(this System.Text.StringBuilder source, int index)
      => Swap(source, 0, index);

    public static void SwapLastWith(this System.Text.StringBuilder source, int index)
      => Swap(source, index, (source ?? throw new System.ArgumentNullException(nameof(source))).Length - 1);
  }
}
