namespace Flux
{
  public static partial class RangeExtensions
  {
    extension(System.Range)
    {
      /// <summary>
      /// <para>Asserts that offset and length is in the range of collectionLength.</para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <param name="collectionLength"></param>
      /// <returns>The endIndex (offset + length - 1).</returns>
      public static int AssertInRange(int offset, int length, int collectionLength)
      {
        System.Index.AssertInRange(offset, collectionLength); // Assert offset first.

        if (!IsInRange(offset, length, collectionLength, out var endIndex)) throw new System.ArgumentOutOfRangeException(nameof(length)); // Assert length second.

        return endIndex;
      }

      /// <summary>
      /// <para>Indicates whether offset and length is in the range of collectionLength.</para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <param name="collectionLength"></param>
      /// <returns></returns>
      public static bool IsInRange(int offset, int length, int collectionLength)
        => IsInRange(offset, length, collectionLength, out var _);

      /// <summary>
      /// <para>Indicates whether offset and length is in the range of collectionLength.</para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <param name="collectionLength"></param>
      /// <param name="endIndex">This out parameter contains the ending (a.k.a. last) index <c>(offset + length - 1)</c>.</param>
      /// <returns></returns>
      public static bool IsInRange(int offset, int length, int collectionLength, out int endIndex)
      {
        endIndex = offset + length - 1;

        return System.Index.IsInRange(offset, collectionLength) && length >= 0 && endIndex < collectionLength;
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public static System.Range FromOffsetAndLength(int offset, int length)
        => new(offset, offset + length);

      /// <summary>
      /// <para>Computes the number of <paramref name="multiple"/>s fit in a 0-based <paramref name="length"/>. E.g. </para>
      /// </summary>
      /// <param name="multiple"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public static int CountMultiplesInRange(int multiple, int length)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiple);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

        return (length - 1) / multiple;
      }
    }

    extension(System.Range source)
    {
      /// <summary>
      /// <para>Indicates whether a <see cref="System.Range"/> is an empty range.</para>
      /// <para>A <see cref="System.Range"/> represents a half-open interval of two <see cref="System.Index"/> objects in a sequence. A <b>start index</b> which is <b>inclusive</b>, and an <b>end index</b> which is <b>exclusive</b>.</para>
      /// <para>Because of this, when a start index and an end index are equal, the range does not select any elements, i.e. it's an <b>empty</b> range.</para>
      /// </summary>
      public bool IsEmpty => source.Start.Equals(source.End);

      /// <summary>
      /// <para>Attempt to convert to Interval without knowledge of a collection length. If either of the Start or End properties are set from-end, this will fail since an offset from the end cannot be determined.</para>
      /// </summary>
      /// <remarks>The Start property of Range is inclusive, but the End property is exclusive.</remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public Interval<int> ToInterval()
      {
        System.ArgumentOutOfRangeException.ThrowIfNotEqual(source.Start.IsFromEnd, false);
        System.ArgumentOutOfRangeException.ThrowIfNotEqual(source.End.IsFromEnd, false);

        return new(source.Start.Value, source.End.Value - 1); // The End property of Range is exclusive.
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>The Start property of Range is inclusive, but the End property is exclusive.</remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public Interval<int> ToInterval(int collectionLength)
      {
        var (index, length) = source.GetOffsetAndLength(collectionLength);

        return new(index, index + length - 1); // The End property of Range is exclusive.
      }
    }
  }
}
