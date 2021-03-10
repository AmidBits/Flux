using System.Linq;

namespace Flux.Model.Maze
{
	public class Grid
		: Grid<Cell>, System.ICloneable
	{
		public Grid(Geometry.Size2 size)
			: base(size.Height, size.Width)
		{
			// Instantiate each cell?
		}

		/// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
		public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds()
			=> Values.Where(cell => cell.Paths.Count == 1);

		/// <summary>Reset edges with one optional 4-way N,E,S,W and/or one 4-way NE,SE,SW,NW.</summary>
		public void ResetEdges(bool orthogonal, bool diagonal)
		{
			for (var y = 0; y < Size.Height; y++) // 
			{
				for (var x = 0; x < Size.Width; x++)
				{
					var cell = this[y, x];

					cell.Edges.Clear();

					var n = (y > 0); // North (positive vertical axis).
					var e = (x < (Size.Width - 1)); // East (negative horizontal axis).
					var s = (y < (Size.Height - 1)); // South (negative vertical axis).
					var w = (x > 0); // West (positive horizontal axis).

					if (orthogonal)
					{
						if (n) cell.Edges.Add((int)EightWindCompassRose.N, this[y - 1, x]);
						if (e) cell.Edges.Add((int)EightWindCompassRose.E, this[y, x + 1]);
						if (s) cell.Edges.Add((int)EightWindCompassRose.S, this[y + 1, x]);
						if (w) cell.Edges.Add((int)EightWindCompassRose.W, this[y, x - 1]);
					}

					if (diagonal)
					{
						if (n && e) cell.Edges.Add((int)EightWindCompassRose.NE, this[y - 1, x + 1]);
						if (s && e) cell.Edges.Add((int)EightWindCompassRose.SE, this[y + 1, x + 1]);
						if (s && w) cell.Edges.Add((int)EightWindCompassRose.SW, this[y + 1, x - 1]);
						if (n && w) cell.Edges.Add((int)EightWindCompassRose.NW, this[y - 1, x - 1]);
					}
				}
			}
		}

		/// <summary>Reset all pathway connection states as either connected or not.</summary>
		public void ResetPaths(bool isConnected)
		{
			foreach (var cell in Values)
			{
				cell.Paths.Clear();

				if (isConnected)
					foreach (var edge in cell.Edges)
						cell.Paths[edge.Key] = edge.Value;
			}
		}

		// System.ICloneable
		public object Clone()
			=> new Grid(Size) { Values = new System.Collections.Generic.List<Cell>(Values) };
	}
}
