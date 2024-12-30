namespace Flux
{
  public record class AssemblyInfo
  {
    public static AssemblyInfo Flux
      => new(typeof(AssemblyInfo).Assembly);

    public System.Reflection.Assembly Assembly { get; }

    public AssemblyInfo(System.Reflection.Assembly assembly)
      => Assembly = assembly;

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
      => Assembly.GetName().Version ?? throw new System.NotSupportedException();

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

    private string? GetAssemblyAttribute<T>(System.Func<T, string> selector)
      where T : System.Attribute
      => selector.Invoke((T)System.Attribute.GetCustomAttribute(Assembly, typeof(T))!) is var value && string.IsNullOrEmpty(value) ? null : value;

    public override string ToString()
      => $"{GetType().Name} {{ Assembly = {Assembly.FullName} }}";
  }
}
