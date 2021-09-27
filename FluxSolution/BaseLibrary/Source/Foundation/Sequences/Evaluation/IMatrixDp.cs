namespace Flux.Metrical
{
	/// <summary>Represents some arbitrary measure of distance between two sequences using a dynamic programming matrix.</summary>
	public interface IMatrixDp<T>
	{
    /// <summary>Compute a measured distance for the two sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A measure that represents an arbitrary distance between the two sequences.</returns>
    int[,] GetDpMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
