namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Get the C# alias of .NET built-in types.</summary>
    public static string CsharpAliasName(this System.Type source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      else if (source == typeof(System.Boolean)) return "bool";
      else if (source == typeof(System.Byte)) return "byte";
      else if (source == typeof(System.Char)) return "char";
      else if (source == typeof(System.Decimal)) return "decimal";
      else if (source == typeof(System.Double)) return "double";
      else if (source == typeof(System.Int16)) return "short";
      else if (source == typeof(System.Int32)) return "int";
      else if (source == typeof(System.Int64)) return "long";
      else if (source == typeof(System.IntPtr)) return "nint";
      else if (source == typeof(System.Object)) return "object"; // Reference type.
      else if (source == typeof(System.SByte)) return "sbyte";
      else if (source == typeof(System.Single)) return "float";
      else if (source == typeof(System.String)) return "string"; // Reference type.
      else if (source == typeof(System.UInt16)) return "ushort";
      else if (source == typeof(System.UInt32)) return "uint";
      else if (source == typeof(System.UInt64)) return "ulong";
      else if (source == typeof(System.UIntPtr)) return "nuint";
      else return source.Name;
    }
  }
}
