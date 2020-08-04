using System.Linq;

namespace Flux
{
  public static partial class XtensionsInteger
  {
    public static System.Collections.Generic.IEnumerable<int> SequenceFindMissings(this System.Collections.Generic.IEnumerable<int> sequence)
      => sequence.Zip(sequence.Skip(1), (a, b) => Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);

    public static bool IsSequenceBroken(this System.Collections.Generic.IEnumerable<int> sequence)
      => sequence.Zip(sequence.Skip(1), (a, b) => b - a).Any(gap => gap != 1);

    public static System.Collections.Generic.IEnumerable<int> FindMissingIntegers(this System.Collections.Generic.IEnumerable<int> source)
    {
      //return sequence.Zip(sequence.Skip(1), (a, b) => Enumerable.Range(a + 1, (b - a) - 1)).SelectMany(s => s);
      var missing = new System.Collections.Generic.List<int>();

      if ((source != null) && (source.Any()))
      {
        source.Aggregate((previous, aggregate) =>
        {
          var difference = (aggregate - previous) - 1;

          if (difference > 0)
          {
            missing.AddRange(System.Linq.Enumerable.Range((aggregate - difference), difference));
          }

          return aggregate;
        });
      }

      return missing;
    }
  }
}
