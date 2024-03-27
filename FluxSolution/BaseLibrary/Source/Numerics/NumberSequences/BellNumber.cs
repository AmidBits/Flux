namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Yields a Bell number of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    public static System.Collections.Generic.IEnumerable<TSelf> GetBellNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var current = new TSelf[1] { TSelf.One };

      while (true)
      {
        yield return current[0];

        var previous = current;
        current = new TSelf[previous.Length + 1];
        current[0] = previous[^1];
        for (var i = 1; i <= previous.Length; i++)
          current[i] = previous[i - 1] + current[i - 1];
      }
    }

    /// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in a Bell triangle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    public static System.Collections.Generic.IEnumerable<TSelf[]> GetBellTriangle<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var current = new TSelf[] { TSelf.One };

      while (true)
      {
        yield return current;

        var previous = current;
        current = new TSelf[previous.Length + 1];
        current[0] = previous[^1];
        for (var i = 1; i <= previous.Length; i++)
          current[i] = previous[i - 1] + current[i - 1];
      }
    }

    /// <summary>Creates a new sequence with each element being an array (i.e. row) of Bell numbers in an augmented Bell triangle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Bell_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bell_triangle"/>
    public static System.Collections.Generic.IEnumerable<TSelf[]> GetBellTriangleAugmented<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var current = new TSelf[] { TSelf.One };

      while (true)
      {
        yield return current;

        var previous = current;
        current = new TSelf[previous.Length + 1];
        current[0] = (current[1] = previous[^1]) - previous[0];
        for (var i = 2; i <= previous.Length; i++)
          current[i] = previous[i - 1] + current[i - 1];
      }
    }
  }
}
