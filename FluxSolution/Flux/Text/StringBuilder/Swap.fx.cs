namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Swap the two values specified by <paramref name="indexA"/> and <paramref name="indexB"/>.</para>
    /// </summary>
    internal static bool SwapImpl(this System.Text.StringBuilder source, int indexA, int indexB)
    {
      var isUnequal = indexA != indexB;

      if (isUnequal)
        (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

      return isUnequal;
    }

    /// <summary>
    /// <para>Swap the two values specified by <paramref name="indexA"/> and <paramref name="indexB"/>.</para>
    /// </summary>
    public static bool Swap(this System.Text.StringBuilder source, int indexA, int indexB)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (indexA < 0 || indexA >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexA));
      if (indexB < 0 || indexB >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(indexB));

      return SwapImpl(source, indexA, indexB);
    }

    /// <summary>
    /// <para>Swap the first value in the <paramref name="source"/> with the value at the specified <paramref name="index"/>.</para>
    /// </summary>
    public static bool SwapFirstWith(this System.Text.StringBuilder source, int index)
      => Swap(source, 0, index);

    /// <summary>
    /// <para>Swap the last value in the <paramref name="source"/> with the value at the specified <paramref name="index"/>.</para>
    /// </summary>
    public static bool SwapLastWith(this System.Text.StringBuilder source, int index)
      => Swap(source, index, (source ?? throw new System.ArgumentNullException(nameof(source))).Length - 1);
  }
}
