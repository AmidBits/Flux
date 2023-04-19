namespace Flux.Model
{
  public sealed partial class TowerOfHanoi
  {
    /// <summary>The Frame-Stewart algorithm computes the optimal number of moves.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Tower_of_Hanoi#With_four_pegs_and_beyond"/>
    /// <see cref="https://core.ac.uk/download/pdf/81954097.pdf"/>
    public static int OptimalNumberOfMoves(int numberOfRods, int numberOfDisks)
    {
      if (numberOfRods < 3) throw new System.ArgumentOutOfRangeException(nameof(numberOfRods));
      if (numberOfDisks <= 0) throw new System.ArgumentOutOfRangeException(nameof(numberOfDisks));

      var moves = int.MaxValue;

      if (numberOfDisks <= 1)
        return numberOfDisks;

      if (numberOfRods == 3)
        return (int)Flux.GenericMath.IntegerPow(2, numberOfDisks) - 1;
      else if (numberOfRods >= 3 && numberOfDisks > 0)
      {
        var potentialMoves = new int[numberOfDisks - 1];
        for (var i = 1; i < numberOfDisks; i++)
          potentialMoves[i - 1] = 2 * OptimalNumberOfMoves(numberOfRods, i) + OptimalNumberOfMoves(numberOfRods - 1, numberOfDisks - i);
        return System.Linq.Enumerable.Min(potentialMoves);
      }

      return moves;
    }
  }
}
