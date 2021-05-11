namespace Flux.Memory.Metrics
{
	/// <summary>Represents some arbitrary measure of distance between two sequences.</summary>
	public interface ICustomFullMatrix<T>
	{
    /// <summary>Compute a measured distance for the two sequences.</summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="target">The target sequence.</param>
    /// <returns>A measure that represents an arbitrary distance between the two sequences.</returns>
    double[,] GetCustomFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target);
  }
}
