namespace Flux
{
  public static partial class Em
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first character in the string.</summary>
    public static SpanBuilder<System.Text.Rune> SplitFromCamelCase(ref this SpanBuilder<System.Text.Rune> source, System.Text.Rune separator, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.InvariantCulture;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var left = index > 0 ? source[index - 1] : default;
        var current = source[index];
        var right = index < source.Length - 1 ? source[index + 1] : default;

        if (System.Text.Rune.IsUpper(current))
        {
          if (System.Text.Rune.IsLower(right))
            source[index] = System.Text.Rune.ToLower(current, culture);

          if (index > 0 && (System.Text.Rune.IsLower(left) || System.Text.Rune.IsLower(right)))
            source.Insert(index, separator, 1);
        }
      }

      return source;
    }
  }
}
