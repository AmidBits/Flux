namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Normalize all consecutive character instances satisfying the <paramref name="predicate"/> to <paramref name="maxConsecutiveLength"/>.</para>
    /// </summary>
    public static System.Text.StringBuilder NormalizeConsecutive(this System.Text.StringBuilder source, int maxConsecutiveLength, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (maxConsecutiveLength < 1) throw new System.ArgumentOutOfRangeException(nameof(maxConsecutiveLength));

      var normalizedIndex = 0;
      var isPrevious = false;
      var consecutiveLength = 1;

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

        var isCurrent = predicate(c);

        var nonAdjacent = !(isCurrent && isPrevious);

        if (nonAdjacent || consecutiveLength < maxConsecutiveLength)
        {
          source[normalizedIndex++] = c;

          isPrevious = isCurrent;
        }

        if (nonAdjacent) consecutiveLength = 1;
        else consecutiveLength++;
      }

      return source.Remove(normalizedIndex, source.Length - normalizedIndex);
    }

    /// <summary>
    /// <para>Normalize all consecutive character instances of the specified <paramref name="charactersToNormalize"/> to <paramref name="maxConsecutiveLength"/>.</para>
    /// </summary>
    public static System.Text.StringBuilder NormalizeConsecutive(this System.Text.StringBuilder source, int maxConsecutiveLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] charactersToNormalize)
      => source.NormalizeConsecutive(maxConsecutiveLength, c => charactersToNormalize is null || charactersToNormalize.Length == 0 || charactersToNormalize.Contains(c, equalityComparer));
  }
}
