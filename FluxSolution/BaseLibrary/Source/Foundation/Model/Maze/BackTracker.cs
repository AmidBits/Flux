using System.Linq;

namespace Flux.Model.Maze
{
  public class BackTrackerMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var stack = new System.Collections.Generic.Stack<Cell>();
      grid.GetValues().RandomElement(out var element, Rng);
      stack.Push(element);
      while (stack.Any())
      {
        if (stack.Peek() is Cell current && current.GetEdgesWithoutPaths() is System.Collections.Generic.IEnumerable<Cell> unvisited && unvisited.Any())
        {
          unvisited.RandomElement(out var unvisitedElement, Rng);

          stack.Push(current.ConnectPath(unvisitedElement, true));
        }
        else
          stack.Pop();
      }
    }
  }
}