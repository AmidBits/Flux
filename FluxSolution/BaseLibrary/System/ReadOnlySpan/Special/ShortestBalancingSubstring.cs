namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static System.ReadOnlySpan<char> ShortestBalancingSubstring(this System.ReadOnlySpan<char> source)
    {
      var frequencies = new System.Collections.Generic.Dictionary<char, int>() { { 'A', 0 }, { 'C', 0 }, { 'G', 0 }, { 'T', 0 } };
      for (int i = 0; i < source.Length; ++i)
        frequencies[source[i]] += 1;

      var balancedCount = source.Length / 4;

      if (!frequencies.Where(p => p.Value != balancedCount).Any())
        return System.ReadOnlySpan<char>.Empty;

      var surplusCharacter = frequencies.Where(p => p.Value > balancedCount).Select(p => p.Key).ToList();

      var result = source[..];

      var minLength = frequencies.Where(p => p.Value > balancedCount).Sum(p => p.Value - balancedCount);

      for (var left = 0; left < source.Length - minLength + 1; left++)
      {
        if (surplusCharacter.Contains(source[left]))
        {
          if (minLength == 1)
            return source[..1].ToString();

          for (var right = left + minLength - 1; right < source.Length; right++)
          {
            if (surplusCharacter.Contains(source[right]))
            {
              var subString = source[left..right];

              if (subString.Length >= result.Length)
                break;

              var isBalancing = true; // True (by default) if substring can be the string that needs to be replaced.

              foreach (var sc in surplusCharacter)
              {
                if (subString.Count(sc) < frequencies[sc] - balancedCount)
                {
                  isBalancing = false;
                  break;
                }
              }

              if (isBalancing)
                result = subString;
            }
          }
        }
      }

      return result.ToString();
    }
  }
}
