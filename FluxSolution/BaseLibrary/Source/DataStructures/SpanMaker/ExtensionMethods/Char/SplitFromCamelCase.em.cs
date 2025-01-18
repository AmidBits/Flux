namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first character in the string.</summary>
    public static void SplitFromCamelCase(this ref SpanMaker<char> source, char separator = ' ', System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.InvariantCulture;

      var sm = source;

      for (var index = sm.Length - 1; index >= 0; index--)
      {
        var left = index > 0 ? sm[index - 1] : default;
        var current = sm[index];
        var right = index < sm.Length - 1 ? sm[index + 1] : default;

        if (char.IsUpper(current))
        {
          if (char.IsLower(right))
            sm[index] = char.ToLower(current, culture);

          if (index > 0 && (char.IsLower(left) || char.IsLower(right)))
            sm = sm.Insert(index, 1, separator);
        }
      }

      source = sm;
    }
  }
}
