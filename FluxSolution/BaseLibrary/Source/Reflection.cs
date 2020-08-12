using System.Linq;

namespace Flux.Reflection
{
  public static class Helper
  {
    /// <summary>Returns all the fields matching the binding attributes.</summary>
    public static System.Collections.Generic.IDictionary<string, object?> GetFields<T>(T source, System.Reflection.BindingFlags bindingAttr = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
      => typeof(T).GetFields(bindingAttr).ToDictionary(fi => fi.Name, fi => fi.GetValue(source));

    /// <summary>Get the current method name without using reflection.</summary>
    /// <remarks>Using reflection System.Reflection.MethodInfo.GetCurrentMethod() also works.</remarks>
    public static string GetMethodName([System.Runtime.CompilerServices.CallerMemberName] string caller = null!)
      => caller;

    /// <summary>Returns all the properties matching the binding attributes.</summary>
    public static System.Collections.Generic.IDictionary<string, object?> GetProperties<T>(T source, System.Reflection.BindingFlags bindingAttr = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
      => typeof(T).GetProperties(bindingAttr).ToDictionary(pi => pi.Name, pi => pi.GetValue(source, new object[] { }));

    /// <summary>Returns whether the source type is a reference type. Determined by typeof(T), not source.</summary>
    public static bool IsReferenceType<T>(T source)
      => default(T)! == null && !IsSystemNullable(source);

    /// <summary>Returns whether the source type is System.Nullable<T>. Determined by typeof(T), not source.</summary>
    /// <remark>Should be able to alternatively use: (System.Nullable.GetUnderlyingType(typeof(T)) != null)</remark>
    public static bool IsSystemNullable<T>(T _)
      => typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(System.Nullable<>);

    /// <summary>Returns whether the type is System.ValueType. Determined by typeof(T), not source.</summary>
    public static bool IsSystemValueType<T>(T _)
      => typeof(T).IsValueType;

    /// <summary>Returns whether the source type is a fixed point numeric data type (i.e. decimal).</summary>
    public static bool IsTypeFixedPoint(object source)
      => source is decimal;

    /// <summary>Returns whether the source type is a floating point numeric data type (i.e. float or double).</summary>
    public static bool IsTypeFloatingPoint(object source)
      => source is double || source is float;

    /// <summary>Returns whether the source type is a floating point numeric data type (i.e. integer).</summary>
    public static bool IsTypeInteger(object source)
      => source is byte || source is short || source is int || source is long || source is sbyte || source is ushort || source is uint || source is ulong || source is System.Numerics.BigInteger;

    /// <summary>Returns whether the source type is any kind of numeric data type.</summary>
    public static bool IsTypeNumeric(object source)
      => IsTypeFixedPoint(source) || IsTypeFloatingPoint(source) || IsTypeInteger(source);
  }

  public class AssemblyInfo
  {
    public static AssemblyInfo CallingAssembly => new AssemblyInfo(System.Reflection.Assembly.GetCallingAssembly());
    public static AssemblyInfo EntryAssembly => new AssemblyInfo(System.Reflection.Assembly.GetEntryAssembly() ?? throw new System.InvalidOperationException());
    public static AssemblyInfo ExecutingAssembly => new AssemblyInfo(System.Reflection.Assembly.GetExecutingAssembly());

    public static AssemblyInfo FluxAssembly => new AssemblyInfo(typeof(AssemblyInfo).Assembly);

    public string? Company
      => GetAssemblyAttribute<System.Reflection.AssemblyCompanyAttribute>(a => a.Company);
    public string? Configuration
      => GetAssemblyAttribute<System.Reflection.AssemblyConfigurationAttribute>(a => a.Configuration);
    public string? Copyright
      => GetAssemblyAttribute<System.Reflection.AssemblyCopyrightAttribute>(a => a is null ? string.Empty : a.Copyright);
    public string? Culture
      => GetAssemblyAttribute<System.Reflection.AssemblyCultureAttribute>(a => a is null ? string.Empty : a.Culture);
    public string? DefaultAlias
      => GetAssemblyAttribute<System.Reflection.AssemblyDefaultAliasAttribute>(a => a is null ? string.Empty : a.DefaultAlias);
    public string? Description
      => GetAssemblyAttribute<System.Reflection.AssemblyDescriptionAttribute>(a => a is null ? string.Empty : a.Description);
    public string? FileVersion
      => GetAssemblyAttribute<System.Reflection.AssemblyFileVersionAttribute>(a => a.Version);
    public string? Product
      => GetAssemblyAttribute<System.Reflection.AssemblyProductAttribute>(a => a.Product);
    public string? Title
      => GetAssemblyAttribute<System.Reflection.AssemblyTitleAttribute>(a => a.Title);
    public string? Trademark
      => GetAssemblyAttribute<System.Reflection.AssemblyTrademarkAttribute>(a => a is null ? string.Empty : a.Trademark);

    public System.Version Version
      => _assembly.GetName().Version ?? throw new System.NotSupportedException();

    public string VersionBuild
      => Version.Build.ToString(System.Globalization.CultureInfo.CurrentCulture);
    public string VersionMajor
      => Version.Major.ToString(System.Globalization.CultureInfo.CurrentCulture);
    public string VersionMajorRevision
      => Version.MajorRevision.ToString(System.Globalization.CultureInfo.CurrentCulture);
    public string VersionMinor
      => Version.Minor.ToString(System.Globalization.CultureInfo.CurrentCulture);
    public string VersionMinorRevision
      => Version.MinorRevision.ToString(System.Globalization.CultureInfo.CurrentCulture);
    public string VersionRevision
      => Version.Revision.ToString(System.Globalization.CultureInfo.CurrentCulture);
    public string VersionString
      => Version.ToString();

    private readonly System.Reflection.Assembly _assembly;

    public AssemblyInfo(System.Reflection.Assembly assembly)
      => _assembly = assembly;

    private string? GetAssemblyAttribute<T>(System.Func<T, string> selector) where T : System.Attribute
      => selector.Invoke((T)System.Attribute.GetCustomAttribute(_assembly, typeof(T))!).NullIfEmpty();
  }

  // public static class TypeBuilder
  // {
  //   private static void AddConstructor(System.Reflection.Emit.TypeBuilder typeBuilder)
  //   {
  //     typeBuilder.DefineDefaultConstructor(System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.RTSpecialName);
  //   }

  //   public static void AddProperty(System.Reflection.Emit.TypeBuilder typeBuilder, string name, System.Type type, bool isNullable)
  //   {
  //     var propertyName = name;

  //     if (isNullable && (type.IsValueType || type.IsGenericType)) // This is not needed if the type is a reference type, since reference types can always be null.
  //     {
  //       type = typeof(System.Nullable<>).MakeGenericType(type);
  //     }

  //     var fieldBuilder = typeBuilder.DefineField(@"_" + propertyName, type, System.Reflection.FieldAttributes.Private);

  //     var propertyBuilder = typeBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.HasDefault, type, null);

  //     var getPropertyMethodBuilder = typeBuilder.DefineMethod(@"get_" + propertyName, System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.HideBySig, type, System.Type.EmptyTypes);

  //     var gilg = getPropertyMethodBuilder.GetILGenerator();

  //     gilg.Emit(System.Reflection.Emit.OpCodes.Ldarg_0);
  //     gilg.Emit(System.Reflection.Emit.OpCodes.Ldfld, fieldBuilder);
  //     gilg.Emit(System.Reflection.Emit.OpCodes.Ret);

  //     var setPropertyMethodBuilder = typeBuilder.DefineMethod(@"set_" + propertyName, System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.HideBySig, null, new[] { type });

  //     var silg = setPropertyMethodBuilder.GetILGenerator();

  //     var modifyProperty = silg.DefineLabel();
  //     var exitSet = silg.DefineLabel();

  //     silg.MarkLabel(modifyProperty);
  //     silg.Emit(System.Reflection.Emit.OpCodes.Ldarg_0);
  //     silg.Emit(System.Reflection.Emit.OpCodes.Ldarg_1);
  //     silg.Emit(System.Reflection.Emit.OpCodes.Stfld, fieldBuilder);

  //     silg.Emit(System.Reflection.Emit.OpCodes.Nop);
  //     silg.MarkLabel(exitSet);
  //     silg.Emit(System.Reflection.Emit.OpCodes.Ret);

  //     propertyBuilder.SetGetMethod(getPropertyMethodBuilder);
  //     propertyBuilder.SetSetMethod(setPropertyMethodBuilder);
  //   }

  //   public static object GetPropertyValue(object instance, string propertyName) => instance.GetType().GetProperty(propertyName).GetValue(instance);
  //   public static object GetPropertyValue(object instance, string propertyName, object[] index) => instance.GetType().GetProperty(propertyName).GetValue(instance, index);
  //   public static void SetPropertyValue(object instance, string propertyName, object propertyValue) => instance.GetType().GetProperty(propertyName).SetValue(instance, propertyValue);

  //   public static System.Reflection.Emit.TypeBuilder Create(string className)
  //   {
  //     var an = new System.Reflection.AssemblyName(className);

  //     var ab = System.Reflection.Emit.AssemblyBuilder.DefineDynamicAssembly(an, System.Reflection.Emit.AssemblyBuilderAccess.Run);
  //     var mb = ab.DefineDynamicModule(an.Name);

  //     var typeBuilder = mb.DefineType(an.Name, System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Class | System.Reflection.TypeAttributes.AutoClass | System.Reflection.TypeAttributes.AnsiClass | System.Reflection.TypeAttributes.BeforeFieldInit | System.Reflection.TypeAttributes.AutoLayout, null);

  //     typeBuilder.DefineDefaultConstructor(System.Reflection.MethodAttributes.Public | System.Reflection.MethodAttributes.SpecialName | System.Reflection.MethodAttributes.RTSpecialName);

  //     return typeBuilder;
  //   }
  // }
}
