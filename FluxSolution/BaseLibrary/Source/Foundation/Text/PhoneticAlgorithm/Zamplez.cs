#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the phonetic algorithm zample.</summary>
    public static void RunPhoneticAlgorithms()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunPhoneticAlgorithms));
      System.Console.WriteLine();

      var ipaes = typeof(IPhoneticAlgorithmEncoder).GetDerivedTypes().Select(t => (IPhoneticAlgorithmEncoder?)System.Activator.CreateInstance(t));
      var names = new string[] { "Dougal", "Glinde", "Plumridge", "Simak", "Webberley" };

      foreach (var ipae in ipaes)
        foreach (var name in names)
          System.Console.WriteLine($"{ipae?.GetType().Name ?? @"[untyped]"}.\"{name}\", \"{ipae?.EncodePhoneticAlgorithm(name) ?? @"[unnamed]"}\"");

      System.Console.WriteLine();
    }
  }
}
#endif
