namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Get the C# alias of the .NET type.</summary>
    public static string CsharpAliasName(this System.Type source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      else if (source == typeof(System.Boolean)) return @"bool";
      else if (source == typeof(System.Byte)) return @"byte";
      else if (source == typeof(System.Char)) return @"char";
      else if (source == typeof(System.Decimal)) return @"decimal";
      else if (source == typeof(System.Double)) return @"double";
      else if (source == typeof(System.Int16)) return @"short";
      else if (source == typeof(System.Int32)) return @"int";
      else if (source == typeof(System.Int64)) return @"long";
      //else if (source == typeof(System.Int128)) throw new System.NotImplementedException(); // Cannot find alias at this time.
      else if (source == typeof(System.IntPtr)) return @"nint";
      else if (source == typeof(System.Object)) return @"object";
      else if (source == typeof(System.SByte)) return @"sbyte";
      else if (source == typeof(System.Single)) return @"float";
      else if (source == typeof(System.String)) return @"string";
      else if (source == typeof(System.UInt16)) return @"ushort";
      else if (source == typeof(System.UInt32)) return @"uint";
      else if (source == typeof(System.UInt64)) return @"ulong";
      //else if (source == typeof(System.UInt128)) throw new System.NotImplementedException(); // Cannot find alias at this time.
      else if (source == typeof(System.UIntPtr)) return @"nuint";
      else return source.Name;
    }
  }
}
