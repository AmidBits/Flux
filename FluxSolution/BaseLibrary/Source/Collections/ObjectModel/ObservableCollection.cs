namespace Flux
{
  /// <summary>An ObservableCollection class with added functionality for items PropertyChanged causing OnCollectionChanged to be fired when item fields/properties are changed.</summary>
  public sealed class ObservableCollectionEx<T>
    : System.Collections.ObjectModel.ObservableCollection<T>
    where T : System.ComponentModel.INotifyPropertyChanged
  {
    private bool m_suppressOnCollectionChanged;

    public bool SuppressOnCollectionChanged => m_suppressOnCollectionChanged;

    /// <summary>AddItems (with suppressed OnCollectionChanged while adding)</summary>
    public void AddItems(System.Collections.Generic.IEnumerable<T> items)
    {
      System.ArgumentNullException.ThrowIfNull(items);

      m_suppressOnCollectionChanged = true;

      foreach (T item in items)
        Add(item);

      m_suppressOnCollectionChanged = false;

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
      System.ArgumentNullException.ThrowIfNull(e);

      if (e.OldItems != null)
        foreach (T item in e.OldItems.Cast<T>())
          item.PropertyChanged -= OnItemPropertyChanged;

      if (e.NewItems != null)
        foreach (T item in e.NewItems.Cast<T>())
          item.PropertyChanged += OnItemPropertyChanged;

      if (!m_suppressOnCollectionChanged)
        base.OnCollectionChanged(e);
    }

    /// <summary>Event handler hooked to all items PropertyChanged.</summary>
    private void OnItemPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) => OnPropertyChanged(new PropertyChangedEventArgsEx(e.PropertyName, sender));
  }
}
