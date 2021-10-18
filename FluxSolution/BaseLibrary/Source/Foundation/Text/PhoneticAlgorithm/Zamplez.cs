#if ZAMPLEZ
using System.Linq;

namespace Flux
{
  public static partial class Zamplez
  {
    public static void RunPhoneticAlgorithms()
    {
      var ipaes = Flux.Reflect.GetTypesImplementingInterface<Flux.Text.IPhoneticAlgorithmEncoder>().Select(t => (Flux.Text.IPhoneticAlgorithmEncoder?)System.Activator.CreateInstance(t));
      var names = new string[] { "Dougal", "Glinde", "Plumridge", "Simak", "Webberley" };

      foreach (var ipae in ipaes)
        foreach (var name in names)
          System.Console.WriteLine($"{ipae?.GetType().Name ?? @"[untyped]"}.\"{name}\", \"{ipae?.EncodePhoneticAlgorithm(name) ?? @"[unnamed]"}\"");

      System.Console.WriteLine();
    }
  }
}
#endif
