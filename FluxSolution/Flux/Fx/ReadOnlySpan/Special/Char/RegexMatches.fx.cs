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
    public static DataStructures.OrderedDictionary<Slice, string> RegexMatches(this System.ReadOnlySpan<char> source, string regexPattern)
    {
      var dictionary = new DataStructures.OrderedDictionary<Slice, string>();
      foreach (var vm in new System.Text.RegularExpressions.Regex(regexPattern).EnumerateMatches(source))
        if (new Slice(vm.Index, vm.Length) is var slice)
          dictionary.Add(slice, source.Slice(slice).ToString());
      return dictionary;
    }
  }
}
