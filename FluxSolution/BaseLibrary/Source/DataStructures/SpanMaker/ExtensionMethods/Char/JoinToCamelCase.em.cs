namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static void JoinToCamelCase(this ref SpanMaker<char> source, System.Func<char, bool> predicate, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      var sm = source;

      for (var index = 0; index < sm.Length; index++)
        if (index == 0 || predicate(sm[index]))
        {
          if (index == 0)
            sm[index] = char.ToLower(sm[index], culture);

          while (predicate(sm[index]))
            sm = sm.Remove(index, 1);

          if (index > 0 && index < sm.Length)
            sm[index] = char.ToUpper(sm[index], culture);
        }

      source = sm;
    }
  }
}
