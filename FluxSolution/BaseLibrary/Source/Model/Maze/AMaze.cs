using System.Linq;

namespace Flux.Model.Maze
{
	public abstract class AMaze
		: IMazeBraidable, IMazeCarvable
	{
		protected System.Random Rng { get; set; } = new System.Random();

		public virtual void BraidMaze(Grid grid, double threshold = 0.5)
		{
			if (grid is null) throw new System.ArgumentNullException(nameof(grid));

			foreach (var cell in grid.GetDeadEnds().ToList())
			{
				if (Rng.NextDouble() <= threshold)
				{
					var unlinkedNeighbors = cell.Edges.Where(kvp => !cell.Paths.ContainsValue(kvp.Value));

					var preferredNeighbors = unlinkedNeighbors.OrderBy(kvp => kvp.Value.Paths.Count);

					preferredNeighbors.RandomElement(out var neighbor, Rng);

					cell.ConnectPath(neighbor.Value, true);
				}
			}
		}

		public abstract void CarveMaze(Grid grid);
	}
}
