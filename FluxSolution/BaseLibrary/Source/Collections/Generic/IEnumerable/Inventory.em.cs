using System.Linq;

namespace Flux
{
  public static partial class IEnumerableEm
  {
    /// <summary>Returns a new dictionary with a fill inventory of items, where source items are added and target items are subtracted. The counts are aggregated by <typeparamref name="TKey"/> into a new inventory dictionary.<summary>
    /// <param name="source">Count of items in stock, can be scattered.</param>
    /// <param name="target">Count of items in stock, can be scattered.</param>
    /// <returns>A dictionary with negative values for understock (missing/expected items), zero for even tally, and positive values above zero for overstock (too many).</returns>
    public static System.Collections.Generic.Dictionary<TKey, int> Inventory<TValue, TKey>(this System.Collections.Generic.IEnumerable<TValue> source, System.Collections.Generic.IEnumerable<TValue> target, System.Func<TValue, TKey> keySelector, System.Func<TValue, int> countSelector)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (countSelector is null) throw new System.ArgumentNullException(nameof(countSelector));

      var inventory = new System.Collections.Generic.Dictionary<TKey, int>();

      foreach (var value in source)
      {
        var key = keySelector(value);
        var count = countSelector(value);

        if (inventory.ContainsKey(key)) inventory[key] += count;
        else inventory.Add(key, count);
      }

      foreach (var value in target)
      {
        var key = keySelector(value);
        var count = countSelector(value);

        if (inventory.ContainsKey(key)) inventory[key] -= count;
        else inventory.Add(key, count);
      }

      return inventory;
    }
  }
}
