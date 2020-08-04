using System.Linq;

namespace Flux.Model.Maze
{
  public interface IMaze
  {
    void Braid(Grid grid, double threshold);

    void Build(Grid grid);
  }

  public abstract class Maze
    : IMaze
  {
    protected System.Random m_rng = new System.Random();

    public virtual void Braid(Grid grid, double threshold = 0.5)
    {
      foreach (var cell in grid.GetDeadEnds().ToList())
      {
        if (m_rng.NextDouble() <= threshold)
        {
          var unlinkedNeighbors = cell.Edges.Where(kvp => !cell.Paths.ContainsValue(kvp.Value));

          var preferedNeighbors = unlinkedNeighbors.OrderBy(kvp => kvp.Value.Paths.Count);

          var neighbor = preferedNeighbors.RandomElement(m_rng).Value;

          cell.ConnectPath(neighbor, true);
        }
      }
    }

    public abstract void Build(Grid grid);
  }

  public class AldusBroder
    : Maze
  {
    public override void Build(Grid grid)
    {
      var current = grid.Cells.RandomElement(m_rng);

      var unvisitedCount = grid.Cells.Count - 1;

      while (unvisitedCount > 0)
      {
        var neighbor = current.Edges.Select(kvp => kvp.Value).RandomElement(m_rng);

        if (!neighbor.Paths.Any())
        {
          current.ConnectPath(neighbor, true);

          unvisitedCount--;
        }

        current = neighbor;
      }
    }
  }

  public class Backtracker
    : Maze
  {
    public override void Build(Grid grid)
    {
      var stack = new System.Collections.Generic.Stack<Cell>();
      stack.Push(grid.Cells.RandomElement(m_rng));
      while (stack.Any())
      {
        if (stack.Peek() is Cell current && current.GetEdgesWithoutPaths() is System.Collections.Generic.IEnumerable<Cell> unvisited && unvisited.Any())
        {
          stack.Push(current.ConnectPath(unvisited.RandomElement(m_rng), true));
        }
        else
        {
          stack.Pop();
        }
      }
    }
  }

  public class BinaryTree
    : Maze
  {
    private DirectionEnum _diagonal = DirectionEnum.NorthEast;
    public DirectionEnum Diagonal
    {
      get { return _diagonal; }
      set
      {
        switch (value)
        {
          case DirectionEnum.NorthEast:
          case DirectionEnum.NorthWest:
          case DirectionEnum.SouthEast:
          case DirectionEnum.SouthWest:
            _diagonal = value;
            break;
          default:
            throw new System.ArgumentOutOfRangeException("Must be a diagonal.");
        }
      }
    }

    public override void Build(Grid grid)
    {
      var direction1 = (int)_diagonal - 45;
      var direction2 = (int)_diagonal + 45;

      foreach (var cell in grid.Cells)
      {
        var direction = cell.Edges.Where(kvp => kvp.Key == direction1 || kvp.Key == direction2);
        if (direction.Any())
        {
          cell.ConnectPath(direction.RandomElement(m_rng).Value, true);
        }
      }
    }
  }

  public class GrowingTree
    : Maze
  {
    public System.Func<System.Collections.Generic.List<Cell>, System.Random, Cell> Selector = (list, random) =>
    {
      return list.RandomElement(); // Prim's algorithm by default
    };

    public override void Build(Grid grid)
    {
      var active = new System.Collections.Generic.List<Cell> { Selector(grid.Cells, m_rng) };
      while (active.Any())
      {
        var cell = Selector(active, m_rng);

        var unvisited = cell.GetEdgesWithoutPaths();
        if (unvisited.Any())
        {
          active.Add(cell.ConnectPath(unvisited.RandomElement(m_rng), true));
        }
        else
        {
          active.Remove(cell);
        }
      }
    }
  }

  public class HuntAndKill
    : Maze
  {
    public override void Build(Grid grid)
    {
      Cell? current = grid.Cells.RandomElement(m_rng);

      while (current != null)
      {
        var unvisited = current.GetEdgesWithoutPaths();
        if (unvisited.Any())
        {
          current = current.ConnectPath(unvisited.RandomElement(m_rng), true);
        }
        else
        {
          current = null;

          foreach (var cell in grid.Cells.Where(c => !c.Paths.Any()))
          {
            var visited = cell.GetEdgesWithPaths();
            if (visited.Any())
            {
              cell.ConnectPath(visited.RandomElement(m_rng), true);

              current = cell;

              break;
            }
          }
        }
      }
    }
  }

  public class RecursiveDivision
    : Maze
  {
    public override void Build(Grid grid)
    {
      Divide(grid, 0, 0, grid.Rows, grid.Columns);
    }

    private void Divide(Grid grid, int row, int column, int height, int width)
    {
      if (height <= 1 || width <= 1)
      {
        return;
      }

      //if (height < 5 && width < 5 && _random.Next(4) == 0)
      //	return;

      if (height > width)
      {
        DivideHorizontally(grid, row, column, height, width);
      }
      else
      {
        DivideVertically(grid, row, column, height, width);
      }
    }
    private void DivideHorizontally(Grid grid, int row, int column, int height, int width)
    {
      var divideSouthOf = m_rng.Next(height - 1);
      var passageAt = m_rng.Next(width);

      for (var i = 0; i < width; i++)
      {
        if (i != passageAt)
        {
          var cell = grid[row + divideSouthOf, column + i];

          cell.DisconnectPath(cell.Edges[(int)DirectionEnum.South], true);
        }
      }

      Divide(grid, row, column, divideSouthOf + 1, width);
      Divide(grid, row + divideSouthOf + 1, column, height - divideSouthOf - 1, width);
    }
    private void DivideVertically(Grid grid, int row, int column, int height, int width)
    {
      var divideEastOf = m_rng.Next(width - 1);
      var passageAt = m_rng.Next(height);

      for (var i = 0; i < height; i++)
      {
        if (i != passageAt)
        {
          var cell = grid[row + i, column + divideEastOf];

          cell.DisconnectPath(cell.Edges[(int)DirectionEnum.East], true);
        }
      }

      Divide(grid, row, column, height, divideEastOf + 1);
      Divide(grid, row, column + divideEastOf + 1, height, width - divideEastOf - 1);
    }
  }

  public class Sidewinder
    : Maze
  {
    private DirectionEnum _diagonal = DirectionEnum.NorthEast;
    public DirectionEnum Diagonal
    {
      get { return _diagonal; }
      set
      {
        switch (value)
        {
          case DirectionEnum.NorthEast:
          case DirectionEnum.NorthWest:
          case DirectionEnum.SouthEast:
          case DirectionEnum.SouthWest:
            _diagonal = value;
            break;
          default:
            throw new System.ArgumentOutOfRangeException("Must be a diagonal.");
        }
      }
    }

    public override void Build(Grid grid)
    {
      var direction1 = (int)_diagonal - 45;
      var direction2 = (int)_diagonal + 45;

      var run = new System.Collections.Generic.List<Cell>();

      for (int r = 0; r < grid.Rows; r++)
      {
        run.Clear();

        foreach (var cell in grid.Cells.Skip(r * grid.Columns).Take(grid.Columns))
        {
          run.Add(cell);

          var atBoundary1 = !cell.Edges.ContainsKey(direction1);
          var atBoundary2 = !cell.Edges.ContainsKey(direction2);

          if (atBoundary2 || (!atBoundary1 && m_rng.Next(2) == 0)) // should close out
          {
            if (run.RandomElement(m_rng) is var member && member.Edges.ContainsKey(direction1))
            {
              member.ConnectPath(member.Edges[direction1], true);
            }

            run.Clear();
          }
          else
          {
            cell.ConnectPath(cell.Edges[(int)DirectionEnum.East], true);
          }
        }
      }
    }
  }

  public class Wilsons
    : Maze
  {
    public override void Build(Grid grid)
    {
      var path = new System.Collections.Generic.List<Cell>();

      var unvisited = grid.Cells.ToList();
      unvisited.Remove(unvisited.RandomElement(m_rng));

      while (unvisited.Any())
      {
        var cell = unvisited.RandomElement(m_rng);

        path.Clear();
        path.Add(cell);

        while (unvisited.Contains(cell))
        {
          cell = cell.Edges.Select(kvp => kvp.Value).RandomElement(m_rng);

          if (path.IndexOf(cell) is int position && position > -1)
          {
            path.RemoveRange(position, path.Count - position);
          }
          else
          {
            path.Add(cell);
          }

          for (int i = 0; i < path.Count - 1; i++)
          {
            path[i].ConnectPath(path[i + 1], true);

            unvisited.Remove(path[i]);
          }
        }
      }
    }
  }
}
