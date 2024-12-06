//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para>Yields the number of characters that the source and the target have in common from the start.</para>
//    /// </summary>
//    public static int MatchLength(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);
//      System.ArgumentNullException.ThrowIfNull(predicate);

//      var length = 0;
//      while (length < source.Length && predicate(source[offset++]))
//        length++;
//      return length;
//    }

//    /// <summary>
//    /// <para>Yields the number of characters that the <paramref name="source"/> contains of <paramref name="target"/> the start.</para>
//    /// </summary>
//    public static int StartMatchLength(this System.Text.StringBuilder source, int offset, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);

//      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

//      var length = 0;
//      while (length < source.Length && equalityComparer.Equals(source[offset++], target))
//        length++;
//      return length;
//    }

//    /// <summary>
//    /// <para>Yields the number of characters that the source and the target have in common from the start.</para>
//    /// </summary>
//    public static int StartMatchLength(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);

//      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

//      var minLength = System.Math.Min(source.Length, target.Length);

//      var length = 0;
//      while (length < minLength && equalityComparer.Equals(source[offset++], target[length]))
//        length++;
//      return length;
//    }
//  }
//}
