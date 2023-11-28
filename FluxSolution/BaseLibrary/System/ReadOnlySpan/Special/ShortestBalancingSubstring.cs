namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>
    /// <para>Finds the shortest substring that can be replaced to achieve a balanced count of all <paramref name="vocabulary"/> items in <paramref name="source"/> (i.e. no new space requirements), or empty if there is none.</para>
    /// <see href="https://stackoverflow.com/questions/38877718/smallest-substring-that-can-be-replaced-to-make-the-string-have-the-same-number"/>
    /// </summary>
    /// <param name="source">The sequence in which the shortest balancing substring should be found.</param>
    /// <param name="vocabulary">The items which must be present and balanced in <paramref name="source"/>.</param>
    /// <returns></returns>
    public static (int index, int count) ShortestBalancingSubstring(this System.ReadOnlySpan<char> source, System.ReadOnlySpan<char> vocabulary)
    {
      var frequencies = new System.Collections.Generic.Dictionary<char, int>();

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
                if (subString.Count(sc) < frequencies[sc] - balancedCount)
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

    //public static System.ReadOnlySpan<char> ShortestBalancingSubstring(this System.ReadOnlySpan<char> source, System.ReadOnlySpan<char> vocabulary, out int index, out int count, out System.ReadOnlySpan<char> replacementSuggestion)
    //{
    //  var frequencies = new System.Collections.Generic.Dictionary<char, int>();

    //  for (var i = 0; i < vocabulary.Length; i++)
    //    frequencies.Add(vocabulary[i], 0);

    //  for (int i = 0; i < source.Length; ++i)
    //    frequencies[source[i]] += 1;

    //  var balancedCount = source.Length / 4;

    //  index = -1;
    //  count = 0;

    //  var result = System.ReadOnlySpan<char>.Empty;

    //  if (frequencies.Where(p => p.Value != balancedCount).Any())
    //  {
    //    var surplusCharacter = frequencies.Where(p => p.Value > balancedCount).Select(p => p.Key).ToList();

    //    result = source[..];

    //    var minLength = frequencies.Where(p => p.Value > balancedCount).Sum(p => p.Value - balancedCount);

    //    for (var left = 0; left < source.Length - minLength + 1; left++)
    //    {
    //      if (surplusCharacter.Contains(source[left]))
    //      {
    //        if (minLength == 1)
    //        {
    //          index = 0;
    //          count = 1;

    //          result = source.Slice(index, count).ToString();

    //          break;
    //        }

    //        for (var right = left + minLength - 1; right < source.Length; right++)
    //        {
    //          if (surplusCharacter.Contains(source[right]))
    //          {
    //            var subString = source[left..right];

    //            if (subString.Length >= result.Length)
    //              break;

    //            var isBalancing = true; // True (by default) if substring can be the string that needs to be replaced.

    //            foreach (var sc in surplusCharacter)
    //            {
    //              if (subString.Count(sc) < frequencies[sc] - balancedCount)
    //              {
    //                isBalancing = false;
    //                break;
    //              }
    //            }

    //            if (isBalancing)
    //            {
    //              index = left;
    //              count = right - left;

    //              result = subString;
    //            }
    //          }
    //        }
    //      }
    //    }
    //  }

    //  var missing = new SpanBuilder<char>();

    //  //if (index == 0)
    //  //  for (var i = 0; i < vocabulary.Length; i++)
    //  //    if (vocabulary[i] is var c)
    //  //      missing.Append(c, balancedCount - source[count..].Count(c));

    //  if (index >= 0)
    //    for (var i = 0; i < vocabulary.Length; i++)
    //      if (vocabulary[i] is var c)
    //        missing.Append(c, balancedCount - (source[..index].Count(c) + source[(index + count)..].Count(c)));

    //  replacementSuggestion = missing.ToString();

    //  return result.ToString();
    //}
  }
}
