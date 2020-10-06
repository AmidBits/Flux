namespace Flux.Model
{
  public enum GitHubType
  {
    Unknown,
    Directory,
    File
  }

  public class GitHubEntry
  {
    public int Size { get; set; }
    public GitHubType Type { get; set; }
    public System.Uri Url { get; set; } = new System.Uri(string.Empty);

    public override string ToString()
      => Type switch
      {
        GitHubType.Unknown => $"{Type} ({Size}) \"\" <{Url}>",
        GitHubType.Directory => $"{Type} ({Size}) \"{System.IO.Directory.GetParent(Url.LocalPath).Name}\" <{Url}>",
        GitHubType.File => $"{Type} ({Size}) \"{System.IO.Path.GetExtension(Url.LocalPath)}\" <{Url}>",
        _ => string.Empty
      };
  }

  /// <summary></summary>
  /// <see cref="https://markheath.net/post/list-and-download-github-repo-cs"/>
  public static class GitHubApi
  {
    public static System.Uri CreateUri(string repo, string branch = @"master")
      => new System.Uri($"https://api.github.com/repos/{repo}/contents?ref={branch}");

    public static System.Collections.Generic.IEnumerable<GitHubEntry> GetEntries(System.Uri uri)
    {
      using var hc = new System.Net.Http.HttpClient();

      hc.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(Locale.AppDomainName, Locale.CommonLanguageRuntimeVersion.ToString()));

      foreach (var content in System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, object>[]>(hc.GetStringAsync(uri).Result, null))
      {
        var ghe = new GitHubEntry
        {
          Size = int.Parse($"{content["size"]}", System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture),
          Type = $"{content["type"]}" switch
          {
            var s when nameof(GitHubType.Directory).StartsWith(s, System.StringComparison.OrdinalIgnoreCase) => GitHubType.Directory,
            var s when nameof(GitHubType.File).StartsWith(s, System.StringComparison.OrdinalIgnoreCase) => GitHubType.File,
            _ => GitHubType.Unknown
          }
        };

        ghe.Url = ghe.Type switch
        {
          GitHubType.Directory => new System.Uri($"{content["url"]}"),
          GitHubType.File => new System.Uri($"{content["download_url"]}"),
          GitHubType.Unknown => uri,
          _ => throw new System.NotImplementedException()
        };

        yield return ghe;
      }
    }
    public static System.Collections.Generic.IEnumerable<GitHubEntry> GetEntries(string repo, string branch = @"master")
      => GetEntries(CreateUri(repo, branch));

    public static System.Collections.Generic.IEnumerable<GitHubEntry> GetAllFiles(string repo, string branch = @"master")
    {
      return GetFiles(CreateUri(repo, branch));

      static System.Collections.Generic.IEnumerable<GitHubEntry> GetFiles(System.Uri uri)
      {
        foreach (var ghe in GetEntries(uri))
        {
          switch (ghe.Type)
          {
            case GitHubType.Directory:
              foreach (var subghe in GetFiles(ghe.Url))
                yield return subghe;
              break;
            case GitHubType.File:
              yield return ghe;
              break;
            case GitHubType.Unknown:
              break;
            default:
              throw new System.NotImplementedException();
          }
        }
      }
    }
  }
}
