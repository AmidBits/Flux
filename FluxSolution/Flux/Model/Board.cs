//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Flux.Model
//{
//	public static class Board
//	{
//		public static string[] ToArrayOfString(this string value) { return value.ToCharArray().Select(c => c.ToString()).ToArray(); }

//		[System.Runtime.InteropServices.ComVisible(false)]
//		public class Layer
//			: System.Collections.Generic.List<Layer.Node>
//		{
//			public class Node
//			{
//				/// <summary>The name of the node.</summary>
//				public string Name { get; set; }

//				private System.Collections.Generic.Dictionary<string, object> _properties = new System.Collections.Generic.Dictionary<string, object>();
//				/// <summary>A dictionary collection for custom properties. Values from the collection can be bound.</summary>
//				public System.Collections.Generic.Dictionary<string, object> Properties { get { return _properties; } private set { } }

//				public Node() : this(string.Empty) { }
//				public Node(string name) { this.Name = name; }
//			}

//			private List<Layer> _children = new List<Layer>();
//			/// <summary>A collection of child layers, of the layer.</summary>
//			public List<Layer> Children { get { return _children; } private set { } }

//			private string _name = "Layer";
//			/// <summary>The name of the layer.</summary>
//			public string Name { get { return _name; } private set { _name = value; } }

//			/// <summary>The parent layer, of the layer.</summary>
//			public Layer Parent { get; set; }

//			public Layer(Layer parent) { this.Parent = parent; }

//			private System.Collections.Generic.Dictionary<string, object> _properties = new System.Collections.Generic.Dictionary<string, object>();
//			/// <summary>A dictionary collection for custom properties. Values from the collection can be bound.</summary>
//			public System.Collections.Generic.Dictionary<string, object> Properties { get { return _properties; } private set { } }

//			public IEnumerable<Node> GetDecendantNodes(Predicate<Node> predicate)
//			{
//				foreach (Node n in this)
//					if (predicate(n))
//						yield return n;

//				foreach (Layer l in Children)
//					foreach (Node n in l.GetDecendantNodes(predicate))
//						yield return n;
//			}

//			public IEnumerable<Layer> GetDecendantLayers(Predicate<Layer> predicate)
//			{
//				foreach (Layer l in this.Children)
//				{
//					if (predicate(l))
//					{
//						yield return l;

//						foreach (Layer dl in l.GetDecendantLayers(predicate))
//							yield return dl;
//					}
//				}
//			}

//			public enum GridMaximumColumnsAndRowsEnum
//			{
//				Column,
//				Row
//			}

//			public enum GridNotationEnum
//			{
//				ColumnAndRow,
//				RowAndColumn
//			}

//			/// <summary>Create a grid from two sets of enumerables.</summary>
//			public void CreateGrid(IEnumerable<object> columnIdentifiers, IEnumerable<object> rowIdentifiers, GridMaximumColumnsAndRowsEnum gridMaximumRowOrColumn, GridNotationEnum gridNotation)
//			{
//				this.Clear();

//				this.Name = "Grid";

//				Properties.Clear();
//				Properties.Add("Columns", columnIdentifiers.Count());
//				Properties.Add("Rows", rowIdentifiers.Count());

//				const string ColumnsAndRows = "ColumnsAndRows";
//				switch (gridMaximumRowOrColumn)
//				{
//					case GridMaximumColumnsAndRowsEnum.Column:
//						this.Properties.Add(ColumnsAndRows, columnIdentifiers.Count());
//						break;
//					case GridMaximumColumnsAndRowsEnum.Row:
//						this.Properties.Add(ColumnsAndRows, rowIdentifiers.Count());
//						break;
//				}

//				foreach (var cr in (from columnId in columnIdentifiers from rowId in rowIdentifiers select new { columnId, rowId }))
//				{
//					Node node = new Node(string.Empty);

//					node.Properties.Add("Array", cr.columnId.ToString() + "," + cr.rowId.ToString());
//					node.Properties.Add("Index", this.Count + 1);

//					switch (gridNotation)
//					{
//						case GridNotationEnum.ColumnAndRow:
//							node.Name = cr.columnId.ToString() + cr.rowId.ToString();
//							break;
//						case GridNotationEnum.RowAndColumn:
//							node.Name = cr.rowId.ToString() + cr.columnId.ToString();
//							break;
//					}

//					node.Properties.Add("Column", cr.columnId);
//					node.Properties.Add("Row", cr.rowId);

//					node.Properties.Add("Value", string.Empty);

//					Add(node);
//				}
//			}

//			public Layer CreateGridByColumnsAndRows(int columns, int rows) { CreateGrid(Enumerable.Range(1, (int)columns).Select(i => "C" + i.ToString() as object), Enumerable.Range(1, (int)rows).Select(i => "R" + i.ToString() as object), GridMaximumColumnsAndRowsEnum.Column, GridNotationEnum.RowAndColumn); return this; }
//			public Layer CreateGridChess() { CreateGrid("abcdefgh".ToCharArray().Select(c => c.ToString()), "87654321".ToCharArray().Select(c => c.ToString()), GridMaximumColumnsAndRowsEnum.Column, GridNotationEnum.ColumnAndRow); return this; }
//			public Layer CreateGridSudoku() { CreateGrid("123456789".ToArrayOfString(), "ABCDEFGHI".ToArrayOfString(), GridMaximumColumnsAndRowsEnum.Row, GridNotationEnum.RowAndColumn); return this; }
//			public Layer CreateGridTicTacToe() { CreateGrid("123".ToArrayOfString(), "123".ToArrayOfString(), GridMaximumColumnsAndRowsEnum.Row, GridNotationEnum.RowAndColumn); return this; }

//			public void CreateHexagonCircular(int rings)
//			{
//				this.Clear();

//				this.Name = "HexagonCircular";

//				Properties.Clear();
//				Properties.Add("Rings", rings);

//				Action<int, int> appendChild = (ring, position) =>
//				{
//					Node n = new Node("R" + ring.ToString() + "P" + position.ToString());
//					n.Properties.Add("Index", this.Count + 1);
//					n.Properties.Add("Ring", ring);
//					n.Properties.Add("Position", position);
//					this.Add(n);
//				};

//				appendChild(0, 1);
//				for (int r = 1; r <= rings; r++)
//					for (int p = 1; p <= (r * 6); p++)
//						appendChild(r, p);
//			}
//		}
//	}
//}
