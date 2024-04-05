//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <see href="https://opentelemetry.io/docs/specs/otel/common/attribute-naming/"/>
//    /// </summary>
//    private static string ToOpenTelemetryName(this string? name)
//    {
//      System.ArgumentNullException.ThrowIfNull(name);

//      return new System.Text.StringBuilder(name).ToLowerCase().ReplaceAll(e => e == '.' ? e : '_').ToString();
//    }

//    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
//    public static string ToOpenTelemetryName(this System.Type source)
//      => ToOpenTelemetryName(source.FullName);
//  }
//}
