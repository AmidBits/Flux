namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Yields a Perrin number of the specified value number.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Perrin_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TSelf GetPerrinNumber<TSelf>(TSelf index)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetPerrinNumbers<TSelf>().Where((e, i) => TSelf.CreateChecked(i) == index).First();

    /// <summary>
    /// <para>Creates an indefinite sequence of Perrin numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Perrin_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPerrinNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      TSelf a = TSelf.CreateChecked(3), b = TSelf.Zero, c = TSelf.CreateChecked(2);

      yield return a;
      yield return b;
      yield return c;

      while (true)
      {
        var p = a + b;

        a = b;
        b = c;
        c = p;

        yield return p;
      }
    }
  }
}
