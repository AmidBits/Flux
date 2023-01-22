namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilderChar
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunesReverse(this SequenceBuilder<char> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var rc2 = source[index];

        if (char.IsLowSurrogate(rc2))
        {
          var rc1 = source[--index];

          if (!char.IsHighSurrogate(rc1))
            throw new System.InvalidOperationException(@"Missing high surrogate (required before low surrogate).");

          yield return new System.Text.Rune(rc1, rc2);
        }
        else if (char.IsHighSurrogate(rc2))
          throw new System.InvalidOperationException(@"Unexpected high surrogate (only allowed before low surrogate).");
        else
          yield return new System.Text.Rune(rc2);
      }
    }
  }
}
