namespace Flux
{
  /// <summary>
  /// <para>Factoradic permutations.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Factorial_number_system"/></para>
  /// </summary>
  public static partial class Factoradic
  {
    /// <summary>
    /// <para>Readjust values to obtain the permutation. Start from the end and check if preceding values are lower.</para>
    /// <para><see href="https://stackoverflow.com/a/7919887"/></para>
    /// </summary>
    /// <param name="factorialRepresentation"></param>
    public static void ConvertFactorialRepresentationToIndices(System.Span<int> factorialRepresentation)
    {
      for (var k = factorialRepresentation.Length - 1; k > 0; --k)
        for (var j = k - 1; j >= 0; --j)
          if (factorialRepresentation[j] <= factorialRepresentation[k])
            factorialRepresentation[k]++;
    }

    /// <summary>
    /// <para></para>
    /// <para>No checks are made.</para>
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="factoradicRepresentation"></param>
    public static void GetFactoradicRepresentation(System.Span<int> factoradicRepresentation, int rank)
    {
      var divisor = 1;

      for (var place = 1; place <= factoradicRepresentation.Length; place++)
      {
        if (rank / divisor is var quotient && quotient == 0)
          break; // All the remaining indices will be zero.

        factoradicRepresentation[^place] = quotient % place; // Compute the index at that place.

        divisor *= place;
      }
    }
  }
}
