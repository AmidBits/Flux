﻿#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the set ops zample.</summary>
    public static void RunISetOps()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunISetOps));
      System.Console.WriteLine();

      var os1 = new System.Collections.Generic.HashSet<int>() { 1, 2, 3, 4, 5, 6 };
      var os2 = new System.Collections.Generic.HashSet<int>() { 4, 5, 6, 7, 8, 9 };

      var padding = 2;
      padding++;

      System.Console.WriteLine($"Set:1");
      System.Console.WriteLine($"{string.Join(", ", os1)}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Set:2");
      System.Console.WriteLine($"{string.Join(", ", os2)}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Source-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.SourceDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Sym-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.SymmetricDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Target-Diff.");
      System.Console.WriteLine($"{string.Join(", ", os1.TargetDifference(os2))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Cartesian-Product");
      System.Console.WriteLine($"{string.Join("; ", os1.CartesianProduct(new System.Collections.Generic.HashSet<int>[] { os2 }).Select(v => string.Join(',', v)))}");
      System.Console.WriteLine();

      System.Console.WriteLine($"Power-Set");
      System.Console.WriteLine($"{string.Join("; ", os1.PowerSet().Where(list => list.Count > 0).Select(list => string.Join(',', list)))}");
      System.Console.WriteLine();

      //var (minLeft, minTop, maxLeft, maxTop) = os1.Select(v => v.ToString()).Prepend("Set:1").WriteToConsole();
      //(minLeft, minTop, maxLeft, maxTop) = os2.Select(v => v.ToString()).Prepend("Set:2").WriteToConsole(maxLeft + padding, minTop);

      //(minLeft, minTop, maxLeft, maxTop) = os1.SourceDifference(os2).Select(v => v.ToString()).Prepend("Source-Diff.").WriteToConsole(maxLeft + padding, minTop);
      //(minLeft, minTop, maxLeft, maxTop) = os1.SymmetricDifference(os2).Select(v => v.ToString()).Prepend("Sym-Diff.").WriteToConsole(maxLeft + padding, minTop);
      //(minLeft, minTop, maxLeft, maxTop) = os1.TargetDifference(os2).Select(v => v.ToString()).Prepend("Target-Diff.").WriteToConsole(maxLeft + padding, minTop);
      // (minLeft, minTop, maxLeft, maxTop) = os1.CartesianProduct(new System.Collections.Generic.HashSet<int>[] { os2 }).Select(v => string.Join(",", v)).Prepend($"Cartesian-Product ").WriteToConsole(maxLeft + padding, minTop);
      //(minLeft, minTop, maxLeft, maxTop) = os1.PowerSet().Select(v => string.Join(",", v)).Prepend($"Power-Set ").WriteToConsole(maxLeft + padding, minTop);

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.SetEquals)} = {os1.SetEquals(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.Overlaps)} = {os1.Overlaps(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsSubsetOf)} \u2286 = {os1.IsSubsetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsProperSubsetOf)} \u2282 = {os1.IsProperSubsetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsSupersetOf)} \u2287 = {os1.IsSupersetOf(os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(System.Collections.Generic.ISet<int>.IsProperSupersetOf)} \u2283 = {os1.IsProperSupersetOf(os2)}");

      System.Console.WriteLine();
    }
  }
}
#endif