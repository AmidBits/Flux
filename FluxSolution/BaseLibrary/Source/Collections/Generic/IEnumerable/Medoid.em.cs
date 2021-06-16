using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the medoid of a sequence. A value sequence will need to pre-ordered in order to arrive at a mathematically oriented medoid. This version does not order the sequence beforehand. Medoid is not the same as median.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Medoid"/>
    public static T Medoid<T>(this System.Collections.Generic.IEnumerable<T> source, out int index, out int count)
    {
      switch (source)
      {
        case null:
          throw new System.ArgumentNullException(nameof(source));
        case System.Collections.Generic.ICollection<T> ict when ict.Count > 0:
          return source.ElementAt(index = ((count = ict.Count) - 1) / 2);
        case System.Collections.ICollection ic when ic.Count > 0:
          return source.ElementAt(index = ((count = ic.Count) - 1) / 2);
        default:
          using (var e = source.GetEnumerator())
          {
            var queue = new System.Collections.Generic.Queue<T>();

            count = 0;

            while (e.MoveNext())
            {
              queue.Enqueue(e.Current);

              // Remove every other one when the ordinal count is odd and greater than 1.
              if ((++count & 1) != 0 && queue.Count > 1) queue.Dequeue();
            }

            index = (count - 1) / 2;

            return queue.Count > 0 ? queue.Dequeue() : throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
          }
      }
    }
    /// <summary>Returns the medoid of a sequence. Medoid is not the same as median.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Medoid"/>
    /// <remarks>This is an extension method of convenience.</remarks>
    public static T Medoid<T>(this System.Collections.Generic.IEnumerable<T> source)
      => Medoid(source, out var _, out var _);
  }
}
