namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new list of ranges and sub-strings in <paramref name="source"/> matching <paramref name="regexPattern"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regexPattern"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<(System.Range Range, string Text)> RegexMatches(this System.ReadOnlySpan<char> source, string regexPattern)
    {
      var list = new System.Collections.Generic.List<(System.Range Range, string Text)>();
      foreach (var vm in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateMatches(source))
      {
        var range = new System.Range(vm.Index, vm.Index + vm.Length);

        list.Add((range, source[range].ToString()));
      }
      return list;
    }
  }
}
