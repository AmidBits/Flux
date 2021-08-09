namespace Flux.Model.TowerOfHanoi
{
  /// <summary>Tower of Hanoi puzzle solver.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
  public static class ThreeRodSolver
  {
    /// <summary>Tower of Hanoi puzzle solver for three rods.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi"/>
    public static System.Collections.Generic.IEnumerable<(TLabel from, TLabel to)> Solve<TLabel>(int numberOfDisks, TLabel startRodIdentifier, TLabel temporaryRodIdentifier, TLabel destinationRodIdentifier)
    {
      return Move(numberOfDisks, startRodIdentifier, destinationRodIdentifier, temporaryRodIdentifier);

      static System.Collections.Generic.IEnumerable<(TLabel, TLabel)> Move(int remainingDisks, TLabel source, TLabel target, TLabel auxiliary)
      {
        if (remainingDisks > 0)
        {
          foreach (var move in Move(remainingDisks - 1, source, auxiliary, target)) // Move all but 1, from source to aux, so they are out of the way.
            yield return move;

          yield return (source, target); // Move 1 from source to target.

          foreach (var move in Move(remainingDisks - 1, auxiliary, target, source)) // Move all but 1, from aux to target.
            yield return move;
        }
      }
    }
  }
}
