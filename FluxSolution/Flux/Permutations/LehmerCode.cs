namespace Flux.Permutations
{
  public static class LehmerCode
  {
  	public static System.Collections.Generic.List<T> NthPermutation<T>(T[] atoms, int index, int size)
  	{
  		var result = new System.Collections.Generic.List<T>();
  		for (var i = 0; i < size; i++)
  		{
  			var item = index % atoms.Length;
  			index /= atoms.Length;
  			result.Add(atoms[item]);
  			RemoveInPlace(ref atoms, item, 1);
  		}
  		return result;
  	}
    
    // https://stackoverflow.com/a/24257996
    // https://stackoverflow.com/a/24257996
    public static int Pack(System.ReadOnlySpan<int> digits, System.ReadOnlySpan<int> radixes)
    {
        var n = 0;
        for (var i = 0; i < digits.Length; i++) {
            n = n * radixes[i] + digits[i];
        }
        return n;
    }

    // https://stackoverflow.com/a/24257996
    // https://stackoverflow.com/a/24257996
    public static System.Collections.Generic.List<int> Unpack(int n, System.ReadOnlySpan<int> radixes)
    {
      var digits = new System.Collections.Generic.List<int>();
      for (var i = radixes.Length - 1; i >= 0; i--)
      {
          digits.Insert(0, n % radixes[i]);
          n /= radixes[i];
      }
      return digits;
    }
  }
}
