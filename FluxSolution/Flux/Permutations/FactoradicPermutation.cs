namespace Flux
{
  public static partial class FactoradicPermutation
  {
    public static void CreateIndices(int permutationIndex, System.Span<int> indicesOfFactoradicRepresentation)
    {
      var divisor = 1;

      for (var place = 1; place <= indicesOfFactoradicRepresentation.Length; place++)
      {
        if (permutationIndex / divisor is var quotient && quotient == 0)
          break; // All the remaining indices will be zero.

        indicesOfFactoradicRepresentation[indicesOfFactoradicRepresentation.Length - place] = quotient % place; // Compute the index at that place.

        divisor *= place;
      }
    }

    public static void PermuteValues<T>(System.Span<T> originalValues, System.ReadOnlySpan<int> indicesOfFactoradicRepresentation)
    {
      for (var i = 0; i < indicesOfFactoradicRepresentation.Length; i++) // Permute the values array according to the indices.
      {
        var index = indicesOfFactoradicRepresentation[i] + i;

        if (index != i) // Take the element at index and place it at i, moving the rest up.
        {
          var temp = originalValues[index];

          for (var j = index; j > i; j--)
            originalValues[j] = originalValues[j - 1];

          originalValues[i] = temp;
        }
      }
    }
  }
}
