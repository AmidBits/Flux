//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para>Yields the number of characters that the source and the target have in common in reverse.</para>
//    /// </summary>
//    public static int ReverseMatchLength(this System.Text.StringBuilder source, int offset, System.Func<char, bool> predicate)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);
//      System.ArgumentNullException.ThrowIfNull(predicate);

//      var length = 0;
//      for (var index = source.Length - 1 - offset; index >= 0 && predicate(source[index]); index--)
//        length++;
//      return length;
//    }

//    /// <summary>
//    /// <para>Yields the number of characters that the source and the target have in common at the end.</para>
//    /// </summary>
//    public static int ReverseMatchLength(this System.Text.StringBuilder source, int offset, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);

//      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

//      var length = 0;
//      for (var index = source.Length - 1 - offset; index >= 0 && equalityComparer.Equals(source[index], target); index--)
//        length++;
//      return length;
//    }

//    /// <summary>
//    /// <para>Yields the number of characters that the source and the target have in common at the end.</para>
//    /// </summary>
//    public static int ReverseMatchLength(this System.Text.StringBuilder source, int offset, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);

//      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

//      var sourceIndex = source.Length - offset;
//      var targetIndex = target.Length;

//      var length = 0;
//      while (--sourceIndex >= 0 && --targetIndex >= 0 && equalityComparer.Equals(source[sourceIndex], target[targetIndex]))
//        length++;
//      return length;
//    }
//  }
//}
