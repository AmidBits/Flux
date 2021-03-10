using System.Linq;

namespace Flux.Model.Maze
{
#pragma warning disable CA1031 // Do not catch general exception types
	public class Cell
	{
		/// <summary>Edges are possible pathways.</summary>
		public System.Collections.Generic.Dictionary<int, Cell> Edges { get; private set; } = new System.Collections.Generic.Dictionary<int, Cell>();

		public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithoutPaths()
			=> Edges.Where(kvp => !kvp.Value.Paths.Any()).Select(kvp => kvp.Value);
		public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithPaths()
			=> Edges.Where(kvp => kvp.Value.Paths.Any()).Select(kvp => kvp.Value);

		/// <summary>Paths are established pathways to, from or both ways between the target cell.</summary>
		public System.Collections.Generic.Dictionary<int, Cell?> Paths { get; private set; } = new System.Collections.Generic.Dictionary<int, Cell?>();

		public Cell ConnectPath(Cell cell, bool biDirectional)
		{
			if (cell is null) throw new System.ArgumentNullException(nameof(cell));

			try
			{
				var index = Edges.Where(kvp => kvp.Value.Equals(cell)).First().Key;

				Paths[index] = cell;

				if (biDirectional)
					cell.ConnectPath(this, false);
			}
			catch { }

			return cell;
		}
		public void DisconnectPath(Cell cell, bool biDirectional)
		{
			if (cell is null) throw new System.ArgumentNullException(nameof(cell));

			try
			{
				var index = Edges.Where(e => e.Value.Equals(cell)).First().Key;

				Paths[index] = default;

				if (biDirectional)
					cell.DisconnectPath(this, false);
			}
			catch { }
		}

		public float Weight { get; private set; } = 1;
	}
#pragma warning restore CA1031 // Do not catch general exception types
}
