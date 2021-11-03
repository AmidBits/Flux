namespace Flux.Metrical
{
	/// <summary>Represents some arbitrary values with two sequences using a dynamic programming matrix.</summary>
	public interface IMatrixDp<T>
	{
    /// <summary>Compute a value matrix with the two sequences.</summary>
    /// <param name="source">The source (or a) sequence.</param>
    /// <param name="target">The target (or b) sequence.</param>
    /// <returns>A value matrix with the two sequences.</returns>
    int[,] GetDpMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
