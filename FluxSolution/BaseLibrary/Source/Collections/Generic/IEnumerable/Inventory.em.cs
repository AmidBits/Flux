using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns a new dictionary with a fill inventory of items.<summary>
    /// <typeparam name="TKey">Inventory items.</typeparam>
    /// <param name="source">Count of items in stock, can be scattered.</param>
    /// <param name="target">Count of items in stock, can be scattered.</param>
    /// <returns>A dictionary with negative values for understock (missing/expected items), zero for even tally, and positive values above zero for overstock (too many).</returns>
    public static System.Collections.Generic.Dictionary<TKey, int> Inventory<TKey>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> source, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> target)
      where TKey : notnull
    {
      var inventory = source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

      foreach (var kvp in target ?? throw new System.ArgumentNullException(nameof(target)))
      {
        if (inventory.ContainsKey(kvp.Key)) inventory[kvp.Key] -= kvp.Value;
        else inventory.Add(kvp.Key, kvp.Value);
      }

      return inventory;
    }
  }
}
