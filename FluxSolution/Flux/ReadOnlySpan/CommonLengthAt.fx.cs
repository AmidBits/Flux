//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the specified comparer.</para>
//    /// </summary>
//    public static int CommonLengthAt<T>(this System.ReadOnlySpan<T> source, int sourceIndex, System.ReadOnlySpan<T> target, int targetIndex, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
//      => source[sourceIndex..].CommonPrefixLength(target[targetIndex..], equalityComparer, maxLength);
//    //{
//    //  if (sourceIndex < 0 || sourceIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
//    //  if (targetIndex < 0 || targetIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));

//    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

//    //  var minLength = int.Min(source.Length - sourceIndex, target.Length - targetIndex);

//    //  var length = 0;
//    //  while (length < minLength && equalityComparer.Equals(source[sourceIndex++], target[targetIndex++]))
//    //    length++;
//    //  return length;
//    //}
//  }
//}
