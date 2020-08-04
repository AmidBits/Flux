namespace Flux.Wpf
{
  public static partial class VisualTree
  {
    /// <summary>Returns all ancestors, in reverse succession, of the object.</summary>
    public static System.Collections.Generic.IEnumerable<System.Windows.DependencyObject> GetAncestors(this System.Windows.DependencyObject self)
    {
      for (var parent = System.Windows.Media.VisualTreeHelper.GetParent(self); parent != null; parent = System.Windows.Media.VisualTreeHelper.GetParent(parent))
        yield return parent;
    }
    /// <summary>Returns all visual tree children of the object.</summary>
    public static System.Collections.Generic.IEnumerable<System.Windows.DependencyObject> GetChildren(this System.Windows.DependencyObject self)
    {
      var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(self);

      for (var index = 0; index < count; index++)
        yield return System.Windows.Media.VisualTreeHelper.GetChild(self, index);
    }
    /// <summary>Returns all visual tree descendants of the object.</summary>
    public static System.Collections.Generic.IEnumerable<System.Windows.DependencyObject> GetDescendants(this System.Windows.DependencyObject self)
    {
      var queue = new System.Collections.Generic.Queue<System.Windows.DependencyObject>();
      queue.Enqueue(self);

      while (queue.Count > 0)
      {
        foreach (var child in queue.Dequeue().GetChildren())
        {
          yield return child;

          queue.Enqueue(child);
        }
      }
    }
  }
}
