//namespace Flux
//{
//  public static partial class IConvertibleExtensions
//  {
//    extension(System.IConvertible value)
//    {
//      /// <summary>Complement the built-in System.IConvertible functionality using the Try paradigm.</summary>
//      [System.CLSCompliant(false)]
//      public bool TryChangeType<T>(out T result, System.IFormatProvider? provider = null)
//      {
//        try
//        {
//          result = (T)System.Convert.ChangeType(value, typeof(T), provider);
//          return true;
//        }
//        catch { }

//        result = default!;
//        return false;
//      }

//      /// <summary>Complement the built-in System.IConvertible functionality with a sequential conversion chain.</summary>
//      [System.CLSCompliant(false)]
//      public object ChangeType(System.IFormatProvider? provider, params System.Type[] conversionSequence)
//      {
//        var result = default(object);

//        foreach (var type in conversionSequence)
//        {
//          result = System.Convert.ChangeType(value, type, provider);

//          value = (System.IConvertible)result!;
//        }

//        return result!;
//      }

//      /// <summary>Complement the built-in System.IConvertible functionality with a sequential conversion chain using the Try paradigm.</summary>
//      [System.CLSCompliant(false)]
//      public bool TryChangeType(out object result, System.IFormatProvider? provider, params System.Type[] conversionSequence)
//      {
//        try
//        {
//          result = ChangeType(value, provider, conversionSequence);
//          return true;
//        }
//        catch { }

//        result = default!;
//        return false;
//      }
//    }
//  }
//}
