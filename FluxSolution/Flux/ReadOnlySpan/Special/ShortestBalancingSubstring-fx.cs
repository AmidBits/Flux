namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Finds the shortest substring that can be replaced to achieve a balanced count of all <paramref name="vocabulary"/> elements in <paramref name="source"/> with no change in space requirements.</para>
    /// <see href="https://stackoverflow.com/questions/38877718/smallest-substring-that-can-be-replaced-to-make-the-string-have-the-same-number"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="vocabulary"></param>
    /// <returns>A 2-tuple with (index, count) of elements in the shortest balancing substring, or (-1, 0) if not found.</returns>
    public static (int index, int count) ShortestBalancingSubstring<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> vocabulary)
      where T : notnull
    {
      var frequencies = new System.Collections.Generic.Dictionary<T, int>();

      for (var i = 0; i < vocabulary.Length; i++)
        frequencies.Add(vocabulary[i], 0);

      for (int i = 0; i < source.Length; ++i)
        frequencies[source[i]] += 1;

      var balancedCount = source.Length / 4; // This is the target count for each item in the vocabulary.

      if (!frequencies.Where(p => p.Value != balancedCount).Any())
        return (-1, 0);

      var minLength = frequencies.Where(p => p.Value > balancedCount).Sum(p => p.Value - balancedCount);

      var surplusCharacter = frequencies.Where(p => p.Value > balancedCount).Select(p => p.Key).ToList();

      var index = -1;
      var count = 0;

      var shortest = source;

      for (var left = 0; left < source.Length - minLength + 1; left++)
        if (surplusCharacter.Contains(source[left]))
        {
          if (minLength == 1)
            return (0, 1);

          for (var right = left + minLength - 1; right < source.Length; right++)
            if (surplusCharacter.Contains(source[right]))
            {
              var subString = source[left..right];

              if (subString.Length >= shortest.Length)
                break;

              var isBalancing = true; // True (by default) if substring can be the string that needs to be replaced.

              foreach (var sc in surplusCharacter)
                if (subString.Count(sc, null) < frequencies[sc] - balancedCount)
                {
                  isBalancing = false;
                  break;
                }

              if (isBalancing)
              {
                index = left;
                count = right - left;

                shortest = subString;
              }
            }
        }

      return (index, count);
    }
  }
}
