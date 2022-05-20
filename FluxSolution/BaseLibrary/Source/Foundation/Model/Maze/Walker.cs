namespace Flux.Model.Maze
{
  public sealed class WalkerCave
    : AMaze
  {
    public int RemovalCount = 2;

    public override void CarveMaze(Grid grid)
    {
      if (grid is null) throw new System.ArgumentNullException(nameof(grid));

      var counter = grid.Size.Width * grid.Size.Height / RemovalCount;

      var current = grid[grid.Size.Width / 2, grid.Size.Height / 2];

      while (counter > 0)
      {
        var next = current.Edges.RandomElement().Value;

        if (current.GetEdgesWithoutPaths().Contains(next))
        {
          foreach (var e in current.Edges)
            current.ConnectPath(e.Value, false);

          counter--;
        }

        current = next;
      }
    }
  }
}
