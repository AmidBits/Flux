//using System.Linq;
//using System.Reflection;

//namespace Flux.Wpf
//{
//  /// <summary>Base class for constructing a basic ViewModel class, with bindable dictionary for properties, methods and commands. It supports raising events for PropertyChanged and CanExecuteChanged. It also supports dependecies for all storage.</summary>
//  public class ViewModel
//    : NotifyPropertyChangedEx
//  {
//    public const string PostfixCanExecute = "_CanExecute";
//    public const string PostfixExecute = "_Execute";

//    private readonly System.Collections.Generic.IDictionary<string, string[]> _mapMiICommandCanExecute;

//    public ViewModel() : base()
//    {
//      var piICommand = GetType().GetTypeInfo().DeclaredProperties.Where(pi => (pi.PropertyType.GetTypeInfo().Equals(typeof(System.Windows.Input.ICommand)) || pi.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(System.Windows.Input.ICommand)))).ToArray();

//      foreach (string name in piICommand.Select(pi => pi.Name))
//      {
//        Storage.Settings.Set(name, new Command<object>(parameter => CommandExecute(name + PostfixExecute, parameter), parameter => CommandCanExecute(name + PostfixCanExecute, parameter)));
//      }

//      var miICommandCanExecute = Explicit.Where(mi => mi.Name.EndsWith(PostfixCanExecute)).Join(piICommand, mi => mi.Name, pi => pi.Name + PostfixCanExecute, (mi, pi) => mi);

//      // Map all MethodInfo dependencies with names ending with "_CanExecute" and have corresponding "Command..." properties of type ICommand.
//      _mapMiICommandCanExecute = Flux.DependsOnAttribute.MapDependencies(miICommandCanExecute);

//      var miICommandExecute = Explicit.Where(mi => mi.Name.EndsWith(PostfixExecute)).Join(piICommand, mi => mi.Name, pi => pi.Name + PostfixExecute, (mi, pi) => mi);

//      if (Flux.DependsOnAttribute.MapDependencies(miICommandExecute) is var map && map.Count > 0)
//      {
//        throw new System.ArgumentException($"Invalid dependancy ({string.Join(@", ", map.Select(kvp => $"\"{kvp.Key}\"").ToArray())}) on command execute ({string.Join(@", ", map.Select(kvp => $"\"{string.Join(", ", kvp.Value.ToArray())}\""))}).");
//      }

//      foreach (string name in GetType().GetTypeInfo().DeclaredMembers.SelectMany(mi => mi.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>()).Select(a => a.DependencyMemberName).Distinct())
//      {
//        if (GetType().GetTypeInfo().GetDeclaredProperty(name) == null)
//        {
//          throw new System.ArgumentException($"Unresolved attribute DependsOn(\"{ name }\") on {string.Join(@", ", GetType().GetTypeInfo().DeclaredMembers.Where(mi => mi.GetCustomAttributes<DependsOnAttribute>(true).Count() > 0).Select(mi => $"\"{mi.Name}\"").ToArray())}.");
//        }
//      }
//    }

//    /// <summary>Proxy method for CanExecute methods of ICommand.</summary>
//    private bool CommandCanExecute(string methodName, object parameter)
//    {
//      if (GetType().GetTypeInfo().GetDeclaredMethod(methodName) is var mi && mi != null)
//      {
//        return (bool)mi.Invoke(this, mi.GetParameters().Length == 1 ? new[] { parameter } : null);
//      }
//      else
//      {
//        throw new System.MissingMethodException(methodName);
//      }
//    }

//    /// <summary>Proxy method for Execute methods of ICommand.</summary>
//    private void CommandExecute(string methodName, object parameter)
//    {
//      if (GetType().GetTypeInfo().GetDeclaredMethod(methodName) is var mi && mi != null)
//      {
//        mi.Invoke(this, (mi.GetParameters().Length == 1 ? new[] { parameter } : null));
//      }
//      else
//      {
//        throw new System.MissingMethodException(methodName);
//      }
//    }

//    /// <summary>Raise the RaiseCanExecuteChanged event chain for the CanExecuteName method (use FULL name).</summary>
//    public void OnCanExecuteChanged(string commandName_CanExecute) => Storage.Settings.Get<Command<object>>(commandName_CanExecute.Replace(PostfixCanExecute, string.Empty))?.OnCanExecuteChanged();

//    /// <summary>Override the RaisePropertyChanged from the base class so that RaiseCanExecuteChanged is called appropriately.</summary>
//    public override void OnPropertiesChanged(params string[] names)
//    {
//      base.OnPropertiesChanged(names);

//      foreach (var name in names.Where(n => _mapMiICommandCanExecute.ContainsKey(n)))
//      {
//        foreach (var dependentName in _mapMiICommandCanExecute[name])
//        {
//          OnCanExecuteChanged(dependentName);
//        }
//      }
//    }

//    /// <summary>Set value of named (string) property in property collection, with optional default value.</summary>
//    protected T Get<T>(string name, T defaultValue = default, Storage.Settings.SettingsCustomEnum location = Storage.Settings.SettingsCustomEnum.Temporary, params string[] subContainer)
//    {
//      return Storage.Settings.Get<T>(name, defaultValue, location, subContainer);
//    }
//    /// <summary>Set value of named (string) property in property collection, with optional initial value.</summary>
//    protected T Get<T>(string name, System.Func<T> initialValue, Storage.Settings.SettingsCustomEnum location = Storage.Settings.SettingsCustomEnum.Temporary, params string[] subContainer)
//    {
//      if (!Storage.Settings.GetPropertySet(location, subContainer).ContainsKey(name))
//      {
//        Set(name, initialValue());
//      }

//      return Get<T>(name);
//    }
//    /// <summary>Set value of named (string) property in property collection.</summary>
//    protected void Set<T>(string name, T value, Storage.Settings.SettingsCustomEnum location = Storage.Settings.SettingsCustomEnum.Temporary, params string[] subContainer)
//    {
//      Storage.Settings.Set(name, value, location, subContainer);

//      OnPropertyChanged(name);
//    }
//  }
//}
