namespace Flux
{
  public static partial class Reflect
  {
    public static bool EqualsEx<TClass>(this TClass source, TClass target)
      => source is null || target is null ? source is null && target is null : source.Equals(target);

    /// <summary>Get the C# alias of the .NET type.</summary>
    public static string GetCsharpAlias(this System.Type type)
    {
      if (type is null) throw new System.ArgumentNullException(nameof(type));
      else if (type == typeof(byte)) return @"byte";
      else if (type == typeof(sbyte)) return @"sbyte";
      else if (type == typeof(short)) return @"short";
      else if (type == typeof(ushort)) return @"ushort";
      else if (type == typeof(int)) return @"int";
      else if (type == typeof(uint)) return @"uint";
      else if (type == typeof(long)) return @"long";
      else if (type == typeof(ulong)) return @"ulong";
      else if (type == typeof(float)) return @"float";
      else if (type == typeof(double)) return @"double";
      else if (type == typeof(decimal)) return @"decimal";
      else if (type == typeof(object)) return @"object";
      else if (type == typeof(bool)) return @"bool";
      else if (type == typeof(char)) return @"char";
      else if (type == typeof(string)) return @"string";
      else if (type == typeof(void)) return @"void";
      else return type.Name;
    }
  }
}
