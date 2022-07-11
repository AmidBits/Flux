namespace Flux.DataStructures
{
  public interface IGraph
  {
    System.Collections.Generic.IEnumerable<(int x, int y, object value)> GetEdges();

    System.Collections.Generic.IEnumerable<int> GetVertices();
  }
}
