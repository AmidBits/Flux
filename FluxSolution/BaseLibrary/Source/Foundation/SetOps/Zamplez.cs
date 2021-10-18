#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the set ops zample.</summary>
    public static void RunSetOps()
    {
      var os1 = new Flux.DataStructures.OrderedSet<int>() { 1, 2, 3, 4, 5, 6 };
      var os2 = new Flux.DataStructures.OrderedSet<int>() { 4, 5, 6, 7, 8, 9 };

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(os1)}"); foreach (var item in os1) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(os2)}"); foreach (var item in os2) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetSourceDifference)}"); foreach (var item in Flux.SetOps.SetSourceDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetSymmetricDifference)}"); foreach (var item in Flux.SetOps.SetSymmetricDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetTargetDifference)}"); foreach (var item in Flux.SetOps.SetTargetDifference(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetIntersection)}"); foreach (var item in Flux.SetOps.SetIntersection(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetUnion)}"); foreach (var item in Flux.SetOps.SetUnion(os1, os2)) System.Console.WriteLine(item);
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetUnionAll)}"); foreach (var item in Flux.SetOps.SetUnionAll(os1, os2)) System.Console.WriteLine(item);

      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.SetEquality)} = {Flux.SetOps.SetEquality(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetOverlapping)} = {Flux.SetOps.IsSetOverlapping(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetSubsetOf)} = {Flux.SetOps.IsSetSubsetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetProperSubsetOf)} = {Flux.SetOps.IsSetProperSubsetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetSupersetOf)} = {Flux.SetOps.IsSetSupersetOf(os1, os2)}");
      System.Console.WriteLine($"{System.Environment.NewLine}{nameof(Flux.SetOps.IsSetProperSupersetOf)} = {Flux.SetOps.IsSetProperSupersetOf(os1, os2)}");

      System.Console.WriteLine();
    }
  }
}
#endif
