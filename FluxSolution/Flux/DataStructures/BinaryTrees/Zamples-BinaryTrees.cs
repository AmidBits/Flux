#if DEBUG
using Flux.Globalization.En;

namespace Flux
{
  public static partial class Zamples
  {
    public static void DataStructuresImmutableAvlTree()
    {
      var bst = Flux.DataStructures.Immutable.ImmutableAvlTree<int, string>.Empty;

      for (var index = 0; bst.GetNodeCount() < 16; index++)
      {
        var r = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, 31);

        if (!bst.Contains(r))
          bst = bst.Add(r, r.ToEnglishWordString());
      }

      System.Console.WriteLine(bst.ToConsoleBlock().ToString());

      var counter = 0;
      foreach (var item in bst.TraverseDfsInOrder())
        System.Console.WriteLine($"{counter++:D2} : {item.Value}");
    }
  }
}
#endif
