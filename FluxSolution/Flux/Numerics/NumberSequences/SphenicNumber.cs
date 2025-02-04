namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Yields a sequence of all sphenic numbers less than <paramref name="limitNumber"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="limitNumber"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetSphenicNumbers<TSelf>(TSelf limitNumber)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (TSelf i = TSelf.CreateChecked(30); i < limitNumber; i++)
      {
        var count = 0;

        var k = i;

        for (var j = TSelf.CreateChecked(2); k > TSelf.One && count <= 2; j++)
        {
          if (TSelf.IsZero(k % j))
          {
            k /= j;

            if (TSelf.IsZero(k % j))
              break;

            count++;
          }

          if (count == 0 && j > limitNumber / (j * j))
            break;

          if (count == 1 && j > (k / j))
            break;
        }

        if (count == 3 && k == TSelf.One)
          yield return i;
      }
    }
  }
}
