//using System.Data;
//using System.Linq;

//namespace Flux.Model
//{
//#pragma warning disable CA1031 // Do not catch general exception types
//  public enum DirectionEnum
//  {
//    North = 0,
//    NorthEast = 45,
//    East = 90,
//    SouthEast = 135,
//    South = 180,
//    SouthWest = 225,
//    West = 270,
//    NorthWest = 315
//  }

//  //public class Cells
//  //  : System.Collections.Generic.Dictionary<int, Cell>
//  //{
//  //  private System.Collections.Generic.Dictionary<int, Cell> _cells { get; set; }

//  //  new public Cell this[int direction]
//  //  {
//  //    get
//  //    {
//  //      if (_cells.ContainsKey(direction))
//  //        return _cells[direction];

//  //      return null;
//  //    }
//  //    set
//  //    {
//  //      if (_cells.ContainsKey(direction))
//  //      {
//  //        if (value != null)
//  //          _cells[direction] = value;
//  //        else
//  //          _cells.Remove(direction);
//  //      }
//  //      else if (value != null)
//  //        _cells.Add(direction, value);
//  //    }
//  //  }
//  //}

//  //public interface INode<TValue, TNexus>
//  //{
//  //  bool IsEmpty { get; }
//  //  TValue Value { get; }
//  //}

//  //public class Node<TValue, TNexus>
//  //  : INode<TValue, TNexus>
//  //  where TNexus : notnull
//  //{
//  //  public static INode<TValue, TNexus> Empty = new EmptyNode();

//  //  public bool IsEmpty => false;
//  //  public TValue Value { get; set; }

//  //  public System.Collections.Generic.Dictionary<TNexus, Node<TValue, TNexus>> Edges { get; private set; } = new System.Collections.Generic.Dictionary<TNexus, Node<TValue, TNexus>>();

//  //  public System.Collections.Generic.Dictionary<TNexus, Node<TValue, TNexus>> Gates { get; private set; } = new System.Collections.Generic.Dictionary<TNexus, Node<TValue, TNexus>>();

//  //  public System.Collections.Generic.IEnumerable<Node<TValue, TNexus>> GetGatesWithoutPaths() => Gates.Where(kvp => !kvp.Value.Paths.Any()).Select(kvp => kvp.Value);
//  //  public System.Collections.Generic.IEnumerable<Node<TValue, TNexus>> GetGatesWithPaths() => Gates.Where(kvp => kvp.Value.Paths.Any()).Select(kvp => kvp.Value);

//  //  public System.Collections.Generic.Dictionary<TNexus, Node<TValue, TNexus>> Paths { get; private set; } = new System.Collections.Generic.Dictionary<TNexus, Node<TValue, TNexus>>();


//  //  public Node(TValue value)
//  //  {
//  //    Value = value;
//  //  }

//  //  public void CreatePath(Node<TValue, TNexus> node, bool biDirectional)
//  //  {
//  //    if (node is null) throw new System.ArgumentNullException(nameof(node));

//  //    try
//  //    {
//  //      var key = Gates.Where(kvp => kvp.Value.Equals(node)).First().Key;

//  //      Paths[key] = node;

//  //      if (biDirectional) node.CreatePath(this, false);
//  //    }
//  //    catch { }
//  //  }
//  //  public void DestroyPath(Node<TValue, TNexus> node, bool biDirectional)
//  //  {
//  //    if (node is null) throw new System.ArgumentNullException(nameof(node));

//  //    try
//  //    {
//  //      var key = Gates.Where(e => e.Value.Equals(node)).First().Key;

//  //      Paths.Remove(key);

//  //      if (biDirectional) node.DestroyPath(this, false);
//  //    }
//  //    catch { }
//  //  }
//  //  public void ResetPaths(bool asCreated)
//  //  {
//  //    Paths.Clear();

//  //    if (asCreated)
//  //      foreach (var gate in Gates)
//  //        Paths[gate.Key] = gate.Value;
//  //  }

//  //  private class EmptyNode
//  //    : INode<TValue, TNexus>
//  //  {
//  //    public bool IsEmpty => true;
//  //    public TValue Value => throw new System.ArgumentException(nameof(EmptyNode));
//  //  }
//  //}

//  //public class Travel
//  //{
//  //  public System.Collections.Generic.Dictionary<int, Cell> Edges { get; private set; } = new System.Collections.Generic.Dictionary<int, Cell>();

//  //  public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithoutPaths() => Edges.Where(kvp => !kvp.Value.Paths.Any()).Select(kvp => kvp.Value);
//  //  public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithPaths() => Edges.Where(kvp => kvp.Value.Paths.Any()).Select(kvp => kvp.Value);

//  //  public System.Collections.Generic.Dictionary<int, Cell> Paths { get; private set; } = new System.Collections.Generic.Dictionary<int, Cell>();
//  //}

//  public class Cell
//  {
//    //public int Column { get; set; }
//    //public int Row { get; set; }

//    public System.Collections.Generic.Dictionary<int, Cell> Edges { get; private set; } = new System.Collections.Generic.Dictionary<int, Cell>();

//    public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithoutPaths() => Edges.Where(kvp => !kvp.Value.Paths.Any()).Select(kvp => kvp.Value);
//    public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithPaths() => Edges.Where(kvp => kvp.Value.Paths.Any()).Select(kvp => kvp.Value);

//    public System.Collections.Generic.Dictionary<int, Cell?> Paths { get; private set; } = new System.Collections.Generic.Dictionary<int, Cell?>();

//    public Cell ConnectPath(Cell cell, bool biDirectional)
//    {
//      if (cell is null) throw new System.ArgumentNullException(nameof(cell));

//      try
//      {
//        var index = Edges.Where(kvp => kvp.Value.Equals(cell)).First().Key;

//        Paths[index] = cell;

//        if (biDirectional)
//          cell.ConnectPath(this, false);
//      }
//      catch { }

//      return cell;
//    }
//    public void DisconnectPath(Cell cell, bool biDirectional)
//    {
//      if (cell is null) throw new System.ArgumentNullException(nameof(cell));

//      try
//      {
//        var index = Edges.Where(e => e.Value.Equals(cell)).First().Key;

//        Paths[index] = default;

//        if (biDirectional)
//          cell.DisconnectPath(this, false);
//      }
//      catch { }
//    }

//    public void ResetPaths(bool asConnected)
//    {
//      Paths.Clear();

//      if (asConnected)
//        foreach (var edge in Edges)
//          Paths[edge.Key] = edge.Value;
//    }

//    public float Weight { get; private set; } = 1;
//  }

//  public class GridX<TValue>
//  {
//    private readonly System.Collections.Generic.List<TValue> m_values = new System.Collections.Generic.List<TValue>();
//    public System.Collections.Generic.IReadOnlyList<TValue> Cells => m_values;

//    public int Columns { get; set; }
//    public int Rows { get; set; }

//    public (int Row, int Column) IndexToRowColumn(int index)
//    {
//      if (index < 0 || index >= Rows * Columns) throw new System.ArgumentOutOfRangeException(nameof(index));

//      return (index / Columns, index % Columns);
//    }

//    public int RowColumnToIndex(int row, int column)
//    {
//      if (row < 0 && row >= Rows) throw new System.ArgumentOutOfRangeException(nameof(row));
//      if (column < 0 && column >= Columns) throw new System.ArgumentOutOfRangeException(nameof(column));

//      return row * Columns + column;
//    }

//    public TValue this[int row, int column]
//    {
//      get => m_values[RowColumnToIndex(row, column)];
//      set => m_values[RowColumnToIndex(row, column)] = value;
//    }

//    public GridX(int rows, int columns)
//    {
//      Columns = columns;
//      Rows = rows;

//      Clear();
//    }

//    public void Clear()
//    {
//      m_values.Clear();

//      for (var r = 0; r < Rows; r++)
//        for (var c = 0; c < Columns; c++)
//          m_values.Add(default!);
//    }
//  }

//  public class GridX
//    : GridX<Cell>
//  {
//    public GridX(int rows, int columns)
//      : base(rows, columns)
//    {
//    }

//    //public int Columns { get; set; }
//    //public int Rows { get; set; }

//    //public Cell this[int row, int column] => (row >= 0 && row < Rows) ? (column >= 0 && column < Columns) ? Cells[row * Columns + column] : throw new System.ArgumentOutOfRangeException(nameof(column)) : throw new System.ArgumentOutOfRangeException(nameof(row));

//    //public Grid(int rows, int columns)
//    //{
//    //  Columns = columns;
//    //  Rows = rows;

//    //  Cells.Clear();

//    //  for (var r = 0; r < Rows; r++)
//    //  {
//    //    for (var c = 0; c < Columns; c++)
//    //    {
//    //      Cells.Add(new Cell() { Column = c, Row = r });
//    //    }
//    //  }
//    //}

//    /// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
//    public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds() => Cells.Where(c => c.Paths.Count == 1);

//    /// <summary>Reset edges with one optional 4-way N,E,S,W and/or one 4-way NE,SE,SW,NW.</summary>
//    public void ResetEdges(bool orthogonal, bool diagonal)
//    {
//      for (var r = 0; r < Rows; r++)
//      {
//        for (var c = 0; c < Columns; c++)
//        {
//          var cell = this[r, c];

//          cell.Edges.Clear();

//          if (orthogonal || diagonal)
//          {
//            var north = (r > 0);
//            var east = (c < (Columns - 1));
//            var south = (r < (Rows - 1));
//            var west = (c > 0);

//            if (orthogonal && north) { cell.Edges.Add((int)DirectionEnum.North, this[r - 1, c]); }
//            if (diagonal && north && east) { cell.Edges.Add((int)DirectionEnum.NorthEast, this[r - 1, c + 1]); }
//            if (orthogonal && east) { cell.Edges.Add((int)DirectionEnum.East, this[r, c + 1]); }
//            if (diagonal && south && east) { cell.Edges.Add((int)DirectionEnum.SouthEast, this[r + 1, c + 1]); }
//            if (orthogonal && south) { cell.Edges.Add((int)DirectionEnum.South, this[r + 1, c]); }
//            if (diagonal && south && west) { cell.Edges.Add((int)DirectionEnum.SouthWest, this[r + 1, c - 1]); }
//            if (orthogonal && west) { cell.Edges.Add((int)DirectionEnum.West, this[r, c - 1]); }
//            if (diagonal && north && west) { cell.Edges.Add((int)DirectionEnum.NorthWest, this[r - 1, c - 1]); }
//            //if (orthogonal && north) { cell.Edges[(int)DirectionIndexEnum.North] = this[r - 1, c]; }
//            //if (diagonal && north && east) { cell.Edges[(int)DirectionIndexEnum.NorthEast] = this[r - 1, c + 1]; }
//            //if (orthogonal && east) { cell.Edges[(int)DirectionIndexEnum.East] = this[r, c + 1]; }
//            //if (diagonal && south && east) { cell.Edges[(int)DirectionIndexEnum.SouthEast] = this[r + 1, c + 1]; }
//            //if (orthogonal && south) { cell.Edges[(int)DirectionIndexEnum.South] = this[r + 1, c]; }
//            //if (diagonal && south && west) { cell.Edges[(int)DirectionIndexEnum.SouthWest] = this[r + 1, c - 1]; }
//            //if (orthogonal && west) { cell.Edges[(int)DirectionIndexEnum.West] = this[r, c - 1]; }
//            //if (diagonal && north && west) { cell.Edges[(int)DirectionIndexEnum.NorthWest] = this[r - 1, c - 1]; }
//          }
//        }
//      }
//    }

//    /// <summary>Reset all pathway connections to either connected state or not.</summary>
//    public void ResetPaths(bool asConnected)
//    {
//      foreach (var cell in Cells)
//        cell.ResetPaths(asConnected);
//    }

//    //public static (int Row, int Column) IndexToRowColumn(int index, int rows, int columns)
//    //  => (index < rows * columns) ? (index / columns, index % columns) : throw new System.ArgumentOutOfRangeException(nameof(index));
//    //public static int RowColumnToIndex(int row, int column, int rows, int columns)
//    //  => (row < 0 || row >= rows) ? throw new System.ArgumentOutOfRangeException(nameof(row)) : (column < 0 || column >= columns) ? throw new System.ArgumentOutOfRangeException(nameof(row)) : (row * columns + column);
//  }
//#pragma warning restore CA1031 // Do not catch general exception types

//  //public class Grid
//  //{
//  //	public System.Collections.Generic.List<Cell> Cells = new System.Collections.Generic.List<Cell>();

//  //	public int Columns { get; private set; }
//  //	public int Rows { get; private set; }

//  //	public Cell this[int row, int column]
//  //	{
//  //		get
//  //		{
//  //			if (row < 0 || column < 0 || row >= Rows || column >= Columns)
//  //				return null;

//  //			return Cells[row * Columns + column];
//  //		}
//  //	}

//  //	public Grid(int rows, int columns)
//  //	{
//  //		Initialize(rows, columns);
//  //	}

//  //	/// <summary>Returns a sequence with all dead end (singly linked) cells.</summary>
//  //	public System.Collections.Generic.IEnumerable<Cell> GetDeadEnds()
//  //	{
//  //		foreach (var cell in Cells)
//  //			if (cell.GetPaths().Count() == 1)
//  //				yield return cell;
//  //	}

//  //	/// <summary>Reset the entire grid, with the option to resize it as well.</summary>
//  //	public virtual void Initialize(int? rows = null, int? columns = null)
//  //	{
//  //		Cells.Clear();

//  //		if (rows.HasValue)
//  //			Rows = rows.Value;
//  //		if (columns.HasValue)
//  //			Columns = columns.Value;

//  //		for (int r = 0; r < Rows; r++)
//  //		{
//  //			for (int c = 0; c < Columns; c++)
//  //			{
//  //				Cells.Add(new Cell(r, c));
//  //			}
//  //		}
//  //	}

//  //	public void ResetEdges(bool orthogonal = true, bool diagonal = false)
//  //	{
//  //		for (int r = 0; r < Rows; r++)
//  //		{
//  //			for (int c = 0; c < Columns; c++)
//  //			{
//  //				var cell = this[r, c];

//  //				cell.ClearEdges();

//  //				if (orthogonal || diagonal)
//  //				{
//  //					var north = (r > 0);
//  //					var east = (c < (Columns - 1));
//  //					var south = (r < (Rows - 1));
//  //					var west = (c > 0);

//  //					if (orthogonal && north) { cell.Edges[(int)DirectionIndexEnum.North] = this[r - 1, c]; }
//  //					if (diagonal && north && east) { cell.Edges[(int)DirectionIndexEnum.NorthEast] = this[r - 1, c]; }
//  //					if (orthogonal && east) { cell.Edges[(int)DirectionIndexEnum.East] = this[r, c + 1]; }
//  //					if (diagonal && south && east) { cell.Edges[(int)DirectionIndexEnum.SouthEast] = this[r - 1, c]; }
//  //					if (orthogonal && south) { cell.Edges[(int)DirectionIndexEnum.South] = this[r + 1, c]; }
//  //					if (diagonal && south && west) { cell.Edges[(int)DirectionIndexEnum.SouthWest] = this[r - 1, c]; }
//  //					if (orthogonal && west) { cell.Edges[(int)DirectionIndexEnum.West] = this[r, c - 1]; }
//  //					if (diagonal && north && west) { cell.Edges[(int)DirectionIndexEnum.NorthWest] = this[r - 1, c]; }
//  //				}
//  //			}
//  //		}
//  //	}

//  //	public void ResetPaths(bool asConnected)
//  //	{
//  //		foreach (var cell in Cells)
//  //			cell.ResetPaths(asConnected);
//  //	}

//  //	public class Cell
//  //	{
//  //		public int Row { get; private set; }
//  //		public int Column { get; private set; }

//  //		public Cell[] Edges { get; private set; }
//  //		public Cell[] Paths { get; private set; }

//  //		public float Weight { get; private set; }

//  //		#region EdgeTo.. properties
//  //		public Cell EdgeToNorth { get { return Edges[0]; } set { Edges[0] = value; } }
//  //		public Cell EdgeToNorthEast { get { return Edges[1]; } set { Edges[1] = value; } }
//  //		public Cell EdgeToEast { get { return Edges[2]; } set { Edges[2] = value; } }
//  //		public Cell EdgeToSouthEast { get { return Edges[3]; } set { Edges[3] = value; } }
//  //		public Cell EdgeToSouth { get { return Edges[4]; } set { Edges[4] = value; } }
//  //		public Cell EdgeToSouthWest { get { return Edges[5]; } set { Edges[5] = value; } }
//  //		public Cell EdgeToWest { get { return Edges[6]; } set { Edges[6] = value; } }
//  //		public Cell EdgeToNorthWest { get { return Edges[7]; } set { Edges[7] = value; } }
//  //		#endregion

//  //		public void ClearEdges()
//  //		{
//  //			Edges = new Cell[] { null, null, null, null, null, null, null, null };
//  //		}

//  //		public System.Collections.Generic.IEnumerable<Cell> GetEdges()
//  //		{
//  //			return Edges.Where(e => e != null);
//  //		}
//  //		public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithoutPaths()
//  //		{
//  //			return GetEdges().Where(e => !e.GetPaths().Any());
//  //		}
//  //		public System.Collections.Generic.IEnumerable<Cell> GetEdgesWithPaths()
//  //		{
//  //			return GetEdges().Where(e => e.GetPaths().Any());
//  //		}

//  //		#region PathTo.. properties
//  //		public Cell PathToNorth { get { return Paths[0]; } set { Paths[0] = value; } }
//  //		public Cell PathToNorthEast { get { return Paths[1]; } set { Paths[1] = value; } }
//  //		public Cell PathToEast { get { return Paths[2]; } set { Paths[2] = value; } }
//  //		public Cell PathToSouthEast { get { return Paths[3]; } set { Paths[3] = value; } }
//  //		public Cell PathToSouth { get { return Paths[4]; } set { Paths[4] = value; } }
//  //		public Cell PathToSouthWest { get { return Paths[5]; } set { Paths[5] = value; } }
//  //		public Cell PathToWest { get { return Paths[6]; } set { Paths[6] = value; } }
//  //		public Cell PathToNorthWest { get { return Paths[7]; } set { Paths[7] = value; } }
//  //		#endregion

//  //		public void ResetPaths(bool asConnected)
//  //		{
//  //			Paths = new Cell[] { null, null, null, null, null, null, null, null };

//  //			for (int i = 0; i < 8; i++)
//  //				Paths[i] = (asConnected ? Edges[i] : null);
//  //		}

//  //		public Cell ConnectPath(Cell cell, bool biDirectional)
//  //		{
//  //			int index = System.Array.IndexOf(Edges, cell);
//  //			Paths[index] = cell;
//  //			if (biDirectional)
//  //				cell.ConnectPath(this, false);
//  //			return cell;
//  //		}
//  //		public void DisconnectPath(Cell cell, bool biDirectional)
//  //		{
//  //			int index = System.Array.IndexOf(Edges, cell);
//  //			Paths[index] = null;
//  //			if (biDirectional)
//  //				cell.DisconnectPath(this, false);
//  //		}

//  //		public System.Collections.Generic.IEnumerable<Cell> GetPaths()
//  //		{
//  //			return Paths.Where(p => p != null);
//  //		}

//  //		//			public System.Collections.Generic.Dictionary<DirectionEnum, Cell> Neighbors = new System.Collections.Generic.Dictionary<DirectionEnum, Cell>();

//  //		//			public System.Collections.Generic.List<Cell> LinksTo = new System.Collections.Generic.List<Cell>();

//  //		//public Cell()
//  //		//{
//  //		//	ClearEdges();

//  //		//	ResetPaths(false);
//  //		//}
//  //		public Cell(int row, int column, float weight = 1F)
//  //		//: this()
//  //		{
//  //			Row = row;
//  //			Column = column;

//  //			Weight = weight;
//  //		}

//  //		//public System.Collections.Generic.IEnumerable<Cell> GetUnlinkedNeighbors()
//  //		//{
//  //		//	return Neighbors.Select(a => a.Value).Where(c => !c.LinksTo.Any());
//  //		//}
//  //		//public System.Collections.Generic.IEnumerable<Cell> GetLinkedNeighbors()
//  //		//{
//  //		//	return Neighbors.Select(a => a.Value).Where(c => c.LinksTo.Any());
//  //		//}

//  //		//public Cell Link(Cell cell, bool biDirectional = true)
//  //		//{
//  //		//	LinksTo.Add(cell);
//  //		//	if (biDirectional)
//  //		//		cell.Link(this, false);
//  //		//	return cell;
//  //		//}
//  //		//public void Unlink(Cell cell, bool biDirectional = true)
//  //		//{
//  //		//	LinksTo.Remove(cell);
//  //		//	if (biDirectional)
//  //		//		cell.Unlink(this, false);
//  //		//}
//  //	}
//  //}
//}
