using System.Diagnostics;
using System.Linq;

namespace Flux.Model
{
  /// <summary>Tower of Hanoi puzzle solver.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
  public static class TowerOfHanoi
  {
    /// <summary>Tower of Hanoi puzzle solver for three rods.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
    public static System.Collections.Generic.IEnumerable<(T from, T to)> SolveForThreeRods<T>(int numberOfDisks, T startRodIdentifier, T temporaryRodIdentifier, T destinationRodIdentifier)
    {
      return Move(numberOfDisks, startRodIdentifier, destinationRodIdentifier, temporaryRodIdentifier);

      System.Collections.Generic.IEnumerable<(T, T)> Move(int remainingDisks, T source, T target, T auxiliary)
      {
        if (remainingDisks > 0)
        {
          foreach (var move in Move(remainingDisks - 1, source, auxiliary, target)) // Move n - 1 disks from source to auxiliary, so they are out of the way
          {
            yield return move;
          }

          yield return (source, target);

          foreach (var move in Move(remainingDisks - 1, auxiliary, target, source))
          {
            yield return move;
          }
        }
      }
    }

    //public static int Power(int a, int b)
    //{
    //  int p = 1, i;
    //  for (i = 0; i < b; i++)
    //  {
    //    p *= a;
    //  }
    //  return p;
    //}

    //public static int Min(int[] abc, int n)
    //{
    //  System.Diagnostics.Debug.WriteLine($"Min {string.Join(", ", abc.Select(n => n.ToString()))} : {n}");
    //  if (abc is null) throw new System.ArgumentNullException(nameof(abc));

    //  var min = abc[0];

    //  for (var i = 1; i < n; i++)
    //    if (abc[i] < min)
    //      min = abc[i];

    //  return min;
    //}

    //public static int FrameStewartNumberOfMoves(int rods, int disks)
    //{
    //  int moves = 2147483647;

    //  if (disks == 0)
    //  {
    //    System.Diagnostics.Debug.WriteLine($"Solve,disks==0");
    //    return 0;
    //  }
    //  if (disks == 1)
    //  {
    //    System.Diagnostics.Debug.WriteLine($"Solve,disks==1");
    //    return 1;
    //  }
    //  if (rods == 3)
    //  {
    //    System.Diagnostics.Debug.WriteLine($"Solve,rods==3");
    //    return Power(2, disks) - 1;
    //  }
    //  if (rods >= 3 && disks > 0)
    //  {
    //    System.Diagnostics.Debug.WriteLine($"Solve,rods>=3&disks>0");
    //    var potential_moves = new int[disks - 1];

    //    for (var i = 1; i < disks; i++)
    //    {
    //      potential_moves[i - 1] = 2 * Solve(rods, i) + Solve(rods - 1, disks - i);
    //    }

    //    return Min(potential_moves, disks - 1);
    //  }
    //  return moves;
    //}

  }
}
