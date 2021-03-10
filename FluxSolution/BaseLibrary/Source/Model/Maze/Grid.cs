using System.Linq;

namespace Flux.Model.Maze
{
	public class Grid
		: Grid<Cell>, System.ICloneable
	{
		public Grid(Geometry.Size2 size)
			: base(size.Height, size.Width)
		{
		}

		/// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
		public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds()
		{
			for (var index = 0; index < Values.Count; index++)
				if (Values[index] is var cell && cell.Paths.Count == 1)
					yield return cell;
		}
		/// <summary>Reset edges with one optional 4-way N,E,S,W and/or one 4-way NE,SE,SW,NW.</summary>
		public void ResetEdges(bool orthogonal, bool diagonal)
		{
			for (var v = 0; v < Size.Height; v++) // 
			{
				for (var h = 0; h < Size.Width; h++)
				{
					var cell = this[v, h];

					cell.Edges.Clear();

					var n = (v > 0); // North (positive vertical axis).
					var e = (h < (Size.Width - 1)); // East (negative horizontal axis).
					var s = (v < (Size.Height - 1)); // South (negative vertical axis).
					var w = (h > 0); // West (positive horizontal axis).

					if (orthogonal)
					{
						if (n) cell.Edges.Add((int)EightWindCompassRose.N, this[v - 1, h]);
						if (e) cell.Edges.Add((int)EightWindCompassRose.E, this[v, h + 1]);
						if (s) cell.Edges.Add((int)EightWindCompassRose.S, this[v + 1, h]);
						if (w) cell.Edges.Add((int)EightWindCompassRose.W, this[v, h - 1]);
					}

					if (diagonal)
					{
						if (n && e) cell.Edges.Add((int)EightWindCompassRose.NE, this[v - 1, h + 1]);
						if (s && e) cell.Edges.Add((int)EightWindCompassRose.SE, this[v + 1, h + 1]);
						if (s && w) cell.Edges.Add((int)EightWindCompassRose.SW, this[v + 1, h - 1]);
						if (n && w) cell.Edges.Add((int)EightWindCompassRose.NW, this[v - 1, h - 1]);
					}
				}
			}
		}

		/// <summary>Reset all pathway connection states as either connected or not.</summary>
		public void ResetPaths(bool isConnected)
		{
			for (var index = Values.Count - 1; index >= 0; index--)
				Values[index].ResetPaths(isConnected);
		}

		// System.ICloneable
		public object Clone()
			=> new Grid(Size) { Values = new System.Collections.Generic.List<Cell>(Values) };
	}
}
