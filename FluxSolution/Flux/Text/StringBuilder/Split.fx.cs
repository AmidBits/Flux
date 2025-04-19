namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Splits a <see cref="System.Text.StringBuilder"/> into substrings based on the specified <paramref name="predicate"/> and <paramref name="options"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<string> Split(this System.Text.StringBuilder source, System.Func<char, bool> predicate, System.StringSplitOptions options = StringSplitOptions.None)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var te = (options & StringSplitOptions.TrimEntries) != 0;
      var ree = (options & StringSplitOptions.RemoveEmptyEntries) != 0;

      var list = new System.Collections.Generic.List<string>();

      var atIndex = 0;

      var maxIndex = source.Length - 1;

      for (var index = 0; index <= maxIndex; index++)
      {
        if ((predicate(source[index]) ? source.ToString(atIndex, index - atIndex) : (index == maxIndex) ? source.ToString(atIndex) : default) is var s && s is not null)
        {
          if (!(ree && (string.IsNullOrEmpty(s) || (te && string.IsNullOrWhiteSpace(s)))))
            list.Add(te ? s.Trim() : s);

          atIndex = index + 1;
        }
      }

      return list;
    }

    /// <summary>
    /// <para>Splits a <see cref="System.Text.StringBuilder"/> into substrings based on the specified <paramref name="options"/> and <paramref name="separators"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="options"></param>
    /// <param name="separators"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<string> Split(this System.Text.StringBuilder source, System.StringSplitOptions options, params char[] separators)
      => source.Split(c => separators.Contains(c), options);
  }
}
