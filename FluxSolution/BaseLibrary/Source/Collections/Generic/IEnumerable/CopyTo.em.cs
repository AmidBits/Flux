namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Copies the elements from the sequence into the array starting at the specified index.</summary>
    public static int CopyTo<T>(this System.Collections.Generic.IEnumerable<T> source, System.Array array, int startIndex, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      if (startIndex < 0 || startIndex >= array.Length) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (startIndex + count >= array.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      var offset = 0;

      foreach (var item in source)
      {
        if (count-- == 0)
          break;

        array.SetValue(item, startIndex + offset++);
      }

      return offset;
    }
    /// <summary>Copies the elements from the sequence into the array starting at the specified index.</summary>
    public static int CopyTo<T>(this System.Collections.Generic.IEnumerable<T> source, System.Array array)
      => CopyTo(source, array, 0, -1);
  }
}
