namespace Flux.Model
{
  /// <summary>Tower of Hanoi puzzle solver.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
  public static class TowerOfHanoi
  {
    /// <summary>Tower of Hanoi puzzle solver for three rods.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
    public static System.Collections.Generic.IEnumerable<(int from, int to)> SolveForThreeRods(int disks)
    {
      return MovesOfHanoiTowersImpl(disks, 0, 2, 1);

      static System.Collections.Generic.IEnumerable<(int, int)> MovesOfHanoiTowersImpl(int remainingDisks, int rod0, int rod2, int rod1)
      {
        if (remainingDisks > 0)
        {
          foreach (var move in MovesOfHanoiTowersImpl(remainingDisks - 1, rod0, rod1, rod2))
          {
            yield return move;
          }

          yield return (rod0, rod2);

          foreach (var move in MovesOfHanoiTowersImpl(remainingDisks - 1, rod1, rod2, rod0))
          {
            yield return move;
          }
        }
      }
    }
  }
}
