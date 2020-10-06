using System.Linq;
using System.Reflection;

namespace Flux
{
  /// <summary>Base implementation of System.ComponentModel.INotifyPropertyChanged.</summary>
  public abstract class NotifyPropertyChanged
    : System.ComponentModel.INotifyPropertyChanged
  {
    /// <summary>Enforced by INotifyPropertyChanged.</summary>
    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null!) 
      => OnPropertiesChanged(name);

    /// <summary>Raise the PropertyChanged event chain for the property name.</summary>
    public virtual void OnPropertiesChanged(params string[] names)
    {
      foreach (var name in names)
      {
        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
      }
    }
  }

  /// <summary>Extended implementation of System.ComponentModel.INotifyPropertyChanged.</summary>
  public abstract class NotifyPropertyChangedX
    : NotifyPropertyChanged
  {
    protected System.Collections.Generic.List<System.Reflection.MethodInfo> Explicit { get; private set; }

    private readonly System.Collections.Generic.IDictionary<string, string[]> m_mapMiExplicit;
    private readonly System.Collections.Generic.IDictionary<string, string[]> m_mapPiNotICommand;

    public NotifyPropertyChangedX()
    {
      Explicit = GetType().GetTypeInfo().DeclaredMethods.Where(mi => !mi.IsSpecialName).ToList(); // Explicit methods, i.e. not accessor, etc.

      m_mapMiExplicit = Flux.DependsOnAttribute.MapDependencies(Explicit);

      var piNotICommand = GetType().GetTypeInfo().DeclaredProperties.Where(pi => !pi.PropertyType.GetTypeInfo().Equals(typeof(System.Windows.Input.ICommand)) && !pi.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(System.Windows.Input.ICommand)));

      m_mapPiNotICommand = Flux.DependsOnAttribute.MapDependencies(piNotICommand);
    }

    /// <summary>Raise the PropertyChanged event chain for the property names.</summary>
    public override void OnPropertiesChanged(params string[] names)
    {
      base.OnPropertiesChanged(names);

      foreach (var name in names)
      {
        if (m_mapPiNotICommand.ContainsKey(name))
        {
          OnPropertiesChanged(m_mapPiNotICommand[name]);
        }

        if (m_mapMiExplicit.ContainsKey(name))
        {
          RunMethods(m_mapMiExplicit[name]);
        }
      }
    }

    public void RunMethod([System.Runtime.CompilerServices.CallerMemberName] string name = null!) => RunMethods(name);

    /// <summary>Run the method event chain for the dependency name.</summary>
    public virtual void RunMethods(params string[] names) => names.Select(name => GetType().GetTypeInfo().GetDeclaredMethod(name)).ToList().ForEach(mi => mi?.Invoke(this, mi.GetParameters().Length > 0 ? new object[mi.GetParameters().Length] : null));
  }
}
