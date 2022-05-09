using System.Linq;

namespace Flux.Model.Maze
{
  public sealed class BackTrackerMaze
    : AMaze
  {
    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var stack = new System.Collections.Generic.Stack<Cell>();
      grid.GetValues().TryGetRandomElement(out var element, Rng);
      stack.Push(element);
      while (stack.Any())
      {
        if (stack.Peek() is Cell current && current.GetEdgesWithoutPaths() is System.Collections.Generic.IEnumerable<Cell> unvisited && unvisited.Any())
        {
          unvisited.TryGetRandomElement(out var unvisitedElement, Rng);

          stack.Push(current.ConnectPath(unvisitedElement, true));
        }
        else
          stack.Pop();
      }
    }
  }
}
