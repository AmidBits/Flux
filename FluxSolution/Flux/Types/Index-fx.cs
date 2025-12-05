namespace Flux
{
  public static partial class IndexExtensions
  {
    extension(System.Index)
    {
      /// <summary>
      /// <para>Asserts that offset is in the range of collectionLength. Throws an exception if not.</para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="collectionLength"></param>
      /// <returns></returns>
      public static void AssertInRange(int offset, int collectionLength)
      {
        if (!IsInRange(offset, collectionLength)) throw new System.ArgumentOutOfRangeException(nameof(offset));
      }

      /// <summary>
      /// <para>Indicates whether offset is in the range of collectionLength.</para>
      /// </summary>
      /// <param name="offset"></param>
      /// <param name="collectionLength"></param>
      /// <returns></returns>
      public static bool IsInRange(int offset, int collectionLength)
        => offset >= 0 && offset < collectionLength;
    }

    extension(System.Index source)
    {
      public System.Index GetOffsetFromEnd(int collectionLength)
        => new(collectionLength - source.GetOffset(collectionLength), true);
    }
  }
}
