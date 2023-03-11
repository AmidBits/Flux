namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Complement the built-in System.IConvertible functionality.</summary>
    /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.convert.changetype"/>
    /// <remarks>This is the method used in the static property <see cref="m_changeTypeOfT"/>.</remarks>
    [System.CLSCompliant(false)]
    public static T ChangeType<T>(System.IConvertible value, System.IFormatProvider? provider)
      => (T)System.Convert.ChangeType(value, typeof(T), provider);

    /// <summary>Complement the built-in System.IConvertible functionality using the Try paradigm.</summary>
    [System.CLSCompliant(false)]
    public static bool TryChangeType<T>(System.IConvertible value, out T result, System.IFormatProvider? provider = null)
    {
      try
      {
        result = ChangeType<T>(value, provider);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    private static readonly System.Reflection.MethodInfo m_changeTypeOfT = System.Linq.Enumerable.Single(typeof(Convert).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static), mi => mi.IsGenericMethod && mi.Name.Equals(nameof(ChangeType), System.StringComparison.Ordinal) && mi.GetParameters().Length == 2);

    /// <summary>Complement the built-in System.IConvertible functionality with a sequential conversion chain.</summary>
    [System.CLSCompliant(false)]
    public static object ChangeType(System.IConvertible value, System.IFormatProvider? provider, params System.Type[] conversionSequence)
    {
      var parameters = new object[] { value ?? throw new System.ArgumentNullException(nameof(value)), provider! };

      var result = default(object);

      foreach (var type in conversionSequence)
      {
        result = m_changeTypeOfT.MakeGenericMethod(type).Invoke(null, parameters);

        parameters[0] = (System.IConvertible)result!;
      }

      return result!;
    }
    /// <summary>Complement the built-in System.IConvertible functionality with a sequential conversion chain using the Try paradigm.</summary>
    [System.CLSCompliant(false)]
    public static bool TryChangeType(System.IConvertible value, out object result, System.IFormatProvider? provider, params System.Type[] conversionSequence)
    {
      try
      {
        result = ChangeType(value, provider, conversionSequence);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
