using System.Linq;
using System.Reflection;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IDictionary<string, double> GetSimpleMatchingCoefficients<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var scores = new System.Collections.Generic.Dictionary<string, double>();

      var sarray = source.ToArray();
      var tarray = target.ToArray();

      typeof(Flux.Metrical.ISimpleMatchingCoefficient<>).GetDerivedTypes().AsParallel().ForAll(type =>
      {
        //try
        //{
        Flux.Metrical.ISimpleMatchingCoefficient<T>? instance = (Flux.Metrical.ISimpleMatchingCoefficient<T>?)type.CreateGenericInstance(typeof(char));

        var smc = instance?.GetSimpleMatchingCoefficient(sarray, tarray) ?? 0;

        scores.Add(type.Name.Replace("`1", string.Empty), smc);
        //}
        //catch { }
      });

      return scores;
    }

    /// <summary>Indicates whether any element in the sequence satisfies the predicate.</summary>
    public static bool FuzzyEquals<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double minimumScore)
      => GetSimpleMatchingCoefficients(source, target).Any(kvp => kvp.Value >= minimumScore);
  }
}
