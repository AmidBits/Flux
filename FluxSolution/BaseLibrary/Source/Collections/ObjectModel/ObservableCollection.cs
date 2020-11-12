using System.Linq;

namespace Flux
{
  /// <summary>An ObservableCollection class with added functionality for items PropertyChanged causing OnCollectionChanged to be fired when item fields/properties are changed.</summary>
  public class ObservableCollectionX<T>
    : System.Collections.ObjectModel.ObservableCollection<T>
    where T : System.ComponentModel.INotifyPropertyChanged
  {
    private bool _suppressOnCollectionChanged;

    /// <summary>AddItems (with suppressed OnCollectionChanged while adding)</summary>
    public void AddItems(System.Collections.Generic.IEnumerable<T> items)
    {
      if (items is null) throw new System.ArgumentNullException(nameof(items));

      _suppressOnCollectionChanged = true;

      foreach (T item in items)
        Add(item);

      _suppressOnCollectionChanged = false;

      OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
    }

    /// <summary>Override ClearItems to unsubscribe OnItemPropertyChanged from the cleared items PropertyChanged events.</summary>
    protected override void ClearItems()
    {
      foreach (T item in this)
        item.PropertyChanged -= OnItemPropertyChanged;

      base.ClearItems();
    }

    /// <summary>Override OnCollectionChanged to manage subscription of to OnPropertyChangedItem to [item].PropertyChanged.</summary>
    protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (e is null) throw new System.ArgumentNullException(nameof(e));

      if (e.OldItems != null)
        foreach (T item in e.OldItems.Cast<T>())
          item.PropertyChanged -= OnItemPropertyChanged;

      if (e.NewItems != null)
        foreach (T item in e.NewItems.Cast<T>())
          item.PropertyChanged += OnItemPropertyChanged;

      if (!_suppressOnCollectionChanged)
        base.OnCollectionChanged(e);
    }

    /// <summary>Event handler hooked to all items PropertyChanged.</summary>
    private void OnItemPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      OnPropertyChanged(new PropertyChangedEventArgsX(e.PropertyName, sender));
    }
  }
}
