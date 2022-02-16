namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Given an array of integers, and a number sum, find the number of pairs of integers in the array whose sum is equal to sum.</summary>
    public static int GetCountOfPairsEqualToSum(this System.Collections.Generic.IList<int> source, int sum)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var count = 0;

      for (var i = source.Count - 1; i >= 0; i--)
      {
        var vi = source[i]; 

        for (var j = i - 1; j >= 0; j--)
          if (vi + source[j] == sum)
            count++;
      }
 
      return count;
    }
  }
}
