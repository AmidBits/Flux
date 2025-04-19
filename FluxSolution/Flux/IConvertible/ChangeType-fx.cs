namespace Flux
{
  public static partial class IConvertibles
  {
    /// <summary>Complement the built-in System.IConvertible functionality using the Try paradigm.</summary>
    [System.CLSCompliant(false)]
    public static bool TryChangeType<T>(this System.IConvertible value, out T result, System.IFormatProvider? provider = null)
    {
      try
      {
        result = (T)System.Convert.ChangeType(value, typeof(T), provider);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Complement the built-in System.IConvertible functionality with a sequential conversion chain.</summary>
    [System.CLSCompliant(false)]
    public static object ChangeType(this System.IConvertible value, System.IFormatProvider? provider, params System.Type[] conversionSequence)
    {
      var result = default(object);

      foreach (var type in conversionSequence)
      {
        result = System.Convert.ChangeType(value, type, provider);

        value = (System.IConvertible)result!;
      }

      return result!;
    }
    /// <summary>Complement the built-in System.IConvertible functionality with a sequential conversion chain using the Try paradigm.</summary>
    [System.CLSCompliant(false)]
    public static bool TryChangeType(this System.IConvertible value, out object result, System.IFormatProvider? provider, params System.Type[] conversionSequence)
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
