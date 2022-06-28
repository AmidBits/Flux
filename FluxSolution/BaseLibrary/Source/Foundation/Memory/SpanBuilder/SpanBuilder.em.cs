namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static void AppendLine(ref this SpanBuilder<char> source, System.ReadOnlySpan<char> value)
    {
      source.Append(value);
      source.Append(System.Environment.NewLine);
    }

    public static void InsertLine(ref this SpanBuilder<char> source, int index, System.ReadOnlySpan<char> value)
    {
      source.Insert(index, value);
      source.Insert(index + value.Length, System.Environment.NewLine);
    }

    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.), if the predicate is satisfied.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    /// <param name="predicate">The first string is the string up until and including the numeric value, and the second string is the suffix to be affixed.</param>
    public static void InsertOrdinalIndicatorSuffix(ref this SpanBuilder<char> source, System.Func<string, string, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var wasDigit = false;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        var isDigit = char.IsDigit(c);

        if (isDigit && !wasDigit)
        {
          var isBetweenTenAndTwenty = index > 0 && source[index - 1] == '1';

          var suffix = c switch
          {
            '1' when !isBetweenTenAndTwenty => @"st",
            '2' when !isBetweenTenAndTwenty => @"nd",
            '3' when !isBetweenTenAndTwenty => @"rd",
            _ => @"th"
          };

          if (predicate(source.ToString(0, index + 1), suffix))
            source.Insert(index + 1, suffix);
        }

        wasDigit = isDigit;
      }
    }
    /// <summary>Returns the source with ordinal extensions (e.g. rd, th, etc.) added for all numeric substrings (e.g. 3rd, 12th, etc.).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ordinal_indicator"/>
    public static void InsertOrdinalIndicatorSuffix(ref this SpanBuilder<char> source)
      => InsertOrdinalIndicatorSuffix(ref source, (s1, s2) => true);

    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(ref this SpanBuilder<char> source, System.Func<char, bool> predicate)
    {
      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            source[index] = char.ToUpper(source[index]);
        }
    }
    public static void JoinToCamelCase(ref this SpanBuilder<char> source)
      => JoinToCamelCase(ref source, char.IsWhiteSpace);

    public static void MakeNumbersFixedLength(ref this SpanBuilder<char> source, int length)
    {
      bool wasDigit = false;
      var digitCount = 0;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var isDigit = char.IsDigit(source[index]);

        if (!isDigit && wasDigit && digitCount < length)
          source.Insert(index + digitCount, '0', length - digitCount);
        else if (isDigit && !wasDigit)
          digitCount = 1;
        else
          digitCount++;

        wasDigit = isDigit;
      }

      if (wasDigit) source.Insert(0, '0', length - digitCount);
    }

    /// <summary>Create a new char array with all diacritical (latin) strokes, which are not covered by the normalization forms in NET, replaced. Can be done simplistically because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
    public static void ReplaceDiacriticalLatinStrokes(ref this SpanBuilder<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = (char)((System.Text.Rune)source[index]).ReplaceDiacriticalLatinStroke().Value;
    }

    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static void SplitFromCamelCase(ref this SpanBuilder<char> source)
    {
      for (var index = source.Length - 1; index > 0; index--)
        if (char.IsUpper(source[index]) && (!char.IsUpper(source[index - 1]) || char.IsLower(source[index + 1])))
          source.Insert(index, ' ');
    }

    public static SpanBuilder<char> ToCharSpanBuilder(this ref SpanBuilder<System.Text.Rune> source)
    {
      var target = new SpanBuilder<char>();

      for (var index = 0; index < source.Length; index++)
        target.Append(source[index].ToString());

      return target;
    }

    //public static SpanBuilder<System.Text.Rune> ToRuneBuilder(ref this SpanBuilder<char> source)
    //{
    //  var runes = new SpanBuilder<System.Text.Rune>();

    //  for (var index = 0; index < source.Length; index++)
    //  {
    //    var c1 = source[index];

    //    if (char.IsHighSurrogate(c1))
    //    {
    //      var c2 = source[++index];

    //      if (!char.IsLowSurrogate(c2))
    //        throw new System.InvalidOperationException(@"Missing low surrogate (required after high surrogate).");

    //      runes.Append(new System.Text.Rune(c1, c2));
    //    }
    //    else if (char.IsLowSurrogate(c1))
    //      throw new System.InvalidOperationException(@"Unexpected low surrogate (only allowed after high surrogate).");
    //    else
    //      runes.Append(new System.Text.Rune(c1));
    //  }

    //  return runes;
    //}

    public static SpanBuilder<System.Text.Rune> ToRuneSpanBuilder(this ref SpanBuilder<char> source)
    {
      var target = new SpanBuilder<System.Text.Rune>();

      foreach (var rune in source.AsReadOnlySpan().EnumerateRunes())
        target.Append(rune);

      return target;
    }

    public static string ToString(ref this SpanBuilder<char> source, int startIndex, int length)
      => source.AsReadOnlySpan().Slice(startIndex, length).ToString();
    public static string ToString(ref this SpanBuilder<char> source, int startIndex)
      => source.ToString(startIndex, source.Length - startIndex);
  }
}
