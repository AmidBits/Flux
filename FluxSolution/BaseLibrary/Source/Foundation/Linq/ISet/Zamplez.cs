#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the set ops zample.</summary>
    public static void RunSetOps()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunSetOps));
      System.Console.WriteLine();

      var os1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
      var os2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6, 7, 8, 9 };

      var padding = 2;
      padding++;

      var (minLeft, minTop, maxLeft, maxTop) = os1.Select(v => v.ToString()).Prepend("Set:1").WriteToConsole();
      (minLeft, minTop, maxLeft, maxTop) = os2.Select(v => v.ToString()).Prepend("Set:2").WriteToConsole(maxLeft + padding, minTop);

      (minLeft, minTop, maxLeft, maxTop) = os1.SourceDifference(os2).Select(v => v.ToString()).Prepend("Source-Diff.").WriteToConsole(maxLeft + padding, minTop);
      (minLeft, minTop, maxLeft, maxTop) = os1.SymmetricDifference(os2).Select(v => v.ToString()).Prepend("Sym-Diff.").WriteToConsole(maxLeft + padding, minTop);
      (minLeft, minTop, maxLeft, maxTop) = os1.TargetDifference(os2).Select(v => v.ToString()).Prepend("Target-Diff.").WriteToConsole(maxLeft + padding, minTop);
      (minLeft, minTop, maxLeft, maxTop) = os1.CartesianProduct(new System.Collections.Generic.HashSet<int>[] { os2 }).Select(v => string.Join(",", v)).Prepend($"Cartesian-Product ").WriteToConsole(maxLeft + padding, minTop);
      (minLeft, minTop, maxLeft, maxTop) = os1.PowerSet().Select(v => string.Join(",", v)).Prepend($"Power-Set ").WriteToConsole(maxLeft + padding, minTop);

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.ISet.Equality)} = {os1.Equality(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.ISet.IsOverlapping)} = {os1.IsOverlapping(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.ISet.IsSubsetOf)} \u2286 = {os1.IsSubsetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.ISet.IsProperSubsetOf)} \u2282 = {os1.IsProperSubsetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.ISet.IsSupersetOf)} \u2287 = {os1.IsSupersetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.ISet.IsProperSupersetOf)} \u2283 = {os1.IsProperSupersetOf(os2)}");

      System.Console.WriteLine();
    }
  }
}
#endif
