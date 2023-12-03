using System.Linq;

namespace Flux
{
  public static partial class Fx
  {
    private static string ToOpenTelemetryName(this string? name)
      => name is null ? throw new System.ArgumentNullException(nameof(name))
      : name.AsSpan().AsSpan().FromCamelCase().ToLowerCase().ToString();

    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
    public static string ToOpenTelemetryName(this System.Type source)
      => ToOpenTelemetryName(source.FullName);
  }
}
