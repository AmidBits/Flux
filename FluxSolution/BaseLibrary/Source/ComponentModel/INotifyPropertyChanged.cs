namespace Flux
{
  /// <summary>Base implementation of System.ComponentModel.INotifyPropertyChanged.</summary>
  public abstract class NotifyPropertyChanged
    : System.ComponentModel.INotifyPropertyChanged
  {
    /// <summary>Enforced by INotifyPropertyChanged.</summary>
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null!) => OnPropertiesChanged(name);

    /// <summary>Raise the PropertyChanged event chain for the property name.</summary>
    public virtual void OnPropertiesChanged(params string[] names)
    {
      foreach (var name in names)
        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
    }
  }

  /// <summary>Extended implementation of System.ComponentModel.INotifyPropertyChanged.</summary>
  public abstract class NotifyPropertyChangedEx
    : NotifyPropertyChanged
  {
    protected System.Collections.Generic.List<System.Reflection.MethodInfo> Explicit { get; private set; }

    private readonly System.Collections.Generic.IDictionary<string, string[]> m_mapMiExplicit;
    private readonly System.Collections.Generic.IDictionary<string, string[]> m_mapPiNotICommand;

    public NotifyPropertyChangedEx()
    {
      Explicit = GetType().GetType().GetMethods().Where(mi => !mi.IsSpecialName).ToList(); // Explicit methods, i.e. not accessor, etc.

      m_mapMiExplicit = DependsOnAttribute.MapDependencies(Explicit);

      var piNotICommand = GetType().GetType().GetProperties().Where(pi => !pi.PropertyType.GetType().Equals(typeof(System.Windows.Input.ICommand)) && !pi.PropertyType.GetType().GetInterfaces().Contains(typeof(System.Windows.Input.ICommand)));

      m_mapPiNotICommand = DependsOnAttribute.MapDependencies(piNotICommand);
    }

    /// <summary>Raise the PropertyChanged event chain for the property names.</summary>
    public override void OnPropertiesChanged(params string[] names)
    {
      base.OnPropertiesChanged(names);

      foreach (var name in names)
      {
        if (m_mapPiNotICommand.TryGetValue(name, out var piNotCommandValue))
          OnPropertiesChanged(piNotCommandValue);

        if (m_mapMiExplicit.TryGetValue(name, out var miExplicitValue))
          RunMethods(miExplicitValue);
      }
    }

    public void RunMethod([System.Runtime.CompilerServices.CallerMemberName] string name = null!) => RunMethods(name);

    /// <summary>Run the method event chain for the dependency name.</summary>
    public virtual void RunMethods(params string[] names) => names.Select(name => GetType().GetMethod(name)).ToList().ForEach(mi => mi?.Invoke(this, mi.GetParameters().Length > 0 ? new object[mi.GetParameters().Length] : null));
  }
}
