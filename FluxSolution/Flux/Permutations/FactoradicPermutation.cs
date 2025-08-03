namespace Flux
{
  public static partial class FactoradicPermutation
  { 
  	public static void CreateIndices(int permutationIndex, ref int[] indicesOfFactoradicRepresentation)
  	{
  		var divisor = 1;
  
  		for (var place = 1; place <= indicesOfFactoradicRepresentation.Length; place++)
  		{
  			if((permutationIndex / divisor) == 0)
  				break; // All the remaining indices will be zero.
  
  			indicesOfFactoradicRepresentation[indicesOfFactoradicRepresentation.Length - place] = (permutationIndex / divisor) % place; // Compute the index at that place.
  
  			divisor *= place;
  		}
  
  		//System.Console.WriteLine($"{string.Join(",", indicesOfFactoradicRepresentation)}");
  	}
  
  	public static void PermuteValues<T>(T[] values, System.ReadOnlySpan<int> indicesOfFactoradicRepresentation)
  	{
  		for (var i = 0; i < indicesOfFactoradicRepresentation.Length; i++) // Permute the values array according to the indices.
  		{
  			var index = indicesOfFactoradicRepresentation[i] + i;
  
  			if(index != i) // Take the element at index and place it at i, moving the rest up.
  			{
  				var temp = values[index];
  
  				for(var j = index; j > i; j--)
  				   values[j] = values[j - 1];
  
  				values[i] = temp;
  			}
  		}
  	}
  }
}
