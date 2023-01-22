#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    public static void RunImmutableDataStructures()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunImmutableDataStructures));
      System.Console.WriteLine();

      RunAvlTree();
    }

    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunAvlTree()
    {
      var bst = Flux.DataStructures.ImmutableAvlTree<int, string>.Empty;

      for (var index = 0; bst.GetNodeCount() < 16; index++)
      {
        var r = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 31);

        if (!bst.Contains(r))
          bst = bst.Add(r, r.ToNamedGrouping());
      }

      System.Console.WriteLine(bst.ToConsoleBlock());

      var counter = 0;
      foreach (var item in bst.GetNodesInOrder())
        System.Console.WriteLine($"{counter++:D2} : {item.Value}");
    }
  }
}
#endif
