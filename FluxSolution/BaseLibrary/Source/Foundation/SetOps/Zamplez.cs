#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the set ops zample.</summary>
    public static void RunSetOps()
    {
      System.Console.WriteLine(nameof(RunSetOps));
      System.Console.WriteLine();

      var os1 = new Flux.DataStructures.OrderedSet<int>() { 1, 2, 3, 4, 5, 6 };
      var os2 = new Flux.DataStructures.OrderedSet<int>() { 4, 5, 6, 7, 8, 9 };

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(os1)}"); foreach (var item in os1) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(os2)}"); foreach (var item in os2) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SourceDifference)}"); foreach (var item in Flux.SetOps.SourceDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SymmetricDifference)}"); foreach (var item in Flux.SetOps.SymmetricDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.TargetDifference)}"); foreach (var item in Flux.SetOps.TargetDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.Intersection)}"); foreach (var item in Flux.SetOps.Intersection(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.Union)}"); foreach (var item in Flux.SetOps.Union(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.UnionAll)}"); foreach (var item in Flux.SetOps.UnionAll(os1, os2)) System.Console.WriteLine(item);

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.Equality)} = {Flux.SetOps.Equality(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsOverlapping)} = {Flux.SetOps.IsOverlapping(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSubsetOf)} = {Flux.SetOps.IsSubsetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsProperSubsetOf)} = {Flux.SetOps.IsProperSubsetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSupersetOf)} = {Flux.SetOps.IsSupersetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsProperSupersetOf)} = {Flux.SetOps.IsProperSupersetOf(os1, os2)}");

      System.Console.WriteLine();
    }
  }
}
#endif
