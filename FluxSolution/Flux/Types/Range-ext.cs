namespace Flux
{
  public static partial class RangeExtensions
  {
    extension(System.Range source)
    {
      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      public static System.Range FromOffsetAndLength(int offset, int length)
        => new(offset, offset + length);

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
