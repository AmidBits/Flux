namespace Flux
{
  public static partial class ExtensionMethods
  {
    //public static System.Collections.Generic.IDictionary<string, double> GetSimpleMatchingCoefficients<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    //{
    //  var scores = new System.Collections.Generic.Dictionary<string, double>();

    //  var sarray = source.ToArray();
    //  var tarray = target.ToArray();

    //  typeof(Flux.Metrical.ISimpleMatchingCoefficient<>).GetDerivedTypes().AsParallel().ForAll(type =>
    //  {
    //    var instance = (Flux.Metrical.ISimpleMatchingCoefficient<T>?)type.CreateGenericInstance(typeof(char));

    //    scores.Add(type.Name.Replace("`1", string.Empty), instance?.GetSimpleMatchingCoefficient(sarray, tarray) ?? 0);
    //  });

    //  return scores;
    //}

    /// <summary>Indicates whether any element in the sequence satisfies the predicate.</summary>
    public static bool FuzzyEquals<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double minimumScore)
      where T : notnull
    {
      if (minimumScore <= new Metrical.DamerauLevenshteinDistance<T>().GetSimpleMatchingCoefficient(source, target))
        return true;
      if (source.Length == target.Length && minimumScore <= new Metrical.HammingDistance<T>().GetSimpleMatchingCoefficient(source, target))
        return true;
      if (minimumScore <= new Metrical.JaroWinklerDistance<T>().GetNormalizedSimilarity(source, target))
        return true;
      if (minimumScore <= new Metrical.LevenshteinDistance<T>().GetSimpleMatchingCoefficient(source, target))
        return true;
      if (minimumScore <= new Metrical.OptimalStringAlignment<T>().GetSimpleMatchingCoefficient(source, target))
        return true;

      return false;
    }
  }
}
