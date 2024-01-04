namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compute the medoid of <paramref name="source"/>, also return the <paramref name="index"/> and the <paramref name="count"/> of elements as output parameters.</para>
    /// <see href="http://en.wikipedia.org/wiki/Medoid"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static TSelf Medoid<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source, out int index, out int count)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      System.ArgumentNullException.ThrowIfNull(source);

      switch (source)
      {
        case System.Collections.Generic.ICollection<TSelf> ict when ict.Count > 0:
          return source.ElementAt(index = ((count = ict.Count) - 1) / 2);
        case System.Collections.ICollection ic when ic.Count > 0:
          return source.ElementAt(index = ((count = ic.Count) - 1) / 2);
        default:
          {
            var queue = new System.Collections.Generic.Queue<TSelf>();

            count = 0;

            foreach (var item in source)
            {
              queue.Enqueue(item);

              // Remove every other one when the ordinal count is odd and greater than 1.
              if ((++count & 1) != 0 && queue.Count > 1) queue.Dequeue();
            }

            index = (count - 1) / 2;

            return queue.Count > 0 ? queue.Dequeue() : throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
          }
      }
    }
  }
}
