namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes(this SequenceBuilder<char> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        var c1 = source[index];

        if (char.IsHighSurrogate(c1))
        {
          var c2 = source[++index];

          if (!char.IsLowSurrogate(c2))
            throw new System.InvalidOperationException(@"Missing low surrogate (required after high surrogate).");

          yield return new System.Text.Rune(c1, c2);
        }
        else if (char.IsLowSurrogate(c1))
          throw new System.InvalidOperationException(@"Unexpected low surrogate (only allowed after high surrogate).");
        else
          yield return new System.Text.Rune(c1);
      }
    }
  }
}
