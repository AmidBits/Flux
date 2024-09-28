namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <span>Returns the string builder with the specified <paramref name="characters"/> replicated <paramref name="count"/> times throughout. If no characters are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified comparer.</span>
    /// </summary>
    public static System.Text.StringBuilder Replicate(this System.Text.StringBuilder source, int count, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null, params char[] characters)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var index = 0; index < source.Length; index++)
      {
        var sourceChar = source[index];

        if (characters.Length == 0 || characters.Contains(sourceChar, equalityComparer))
        {
          source.Insert(index, sourceChar.ToString(), count);

          index += count;
        }
      }

      return source;
    }
  }
}
