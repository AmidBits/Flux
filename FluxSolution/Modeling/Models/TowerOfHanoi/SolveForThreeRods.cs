namespace Flux.Model
{
  public sealed partial class TowerOfHanoi
  {
    /// <summary>Tower of Hanoi puzzle solver for three rods.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
    /// <returns>The moves to solve the TOH for a three rod setup.</returns>
    public static System.Collections.Generic.IEnumerable<(TRod source, TRod target)> SolveForThreeRods<TRod>(int numberOfDisks, TRod source, TRod auxiliary, TRod target)
    {
      return Moves(numberOfDisks, source, target, auxiliary);

      static System.Collections.Generic.IEnumerable<(TRod source, TRod target)> Moves(int numberOfDisks, TRod source, TRod target, TRod auxiliary)
      {
        if (numberOfDisks > 0)
        {
          foreach (var move in Moves(numberOfDisks - 1, source, auxiliary, target)) // Move all but 1, from source to aux, so they are out of the way.
            yield return move;

          yield return (source, target); // Move 1 from source to target.

          foreach (var move in Moves(numberOfDisks - 1, auxiliary, target, source)) // Move all but 1, from aux to target.
            yield return move;
        }
      }
    }
  }
}

/*
  const int numberOfDisks = 6;
  const int numberOfRods = 3;
  var optimalNumberOfMoves = Flux.Model.TowerOfHanoi.FrameStewartAlgorithm.OptimalNumberOfMoves(numberOfRods, numberOfDisks);
  var counter = 1;
  foreach (var move in Flux.Model.TowerOfHanoi.ThreeRods.Solve(numberOfDisks, 'A', 'B', 'C'))
    System.Console.WriteLine($"{counter++}/{optimalNumberOfMoves} = {move}");
*/
