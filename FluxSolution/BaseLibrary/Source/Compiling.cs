#if ROSLYIN_CORE_COMPILER_AVAILABLE

// Requires the package "Microsoft.CodeAnalysis.CSharp".

using System.Linq;
using System.Reflection;

namespace Flux.Compiling
{
  public static class Helper
  {
    public static object? ExecuteCsharp(string codeAssembly, object[] codeArguments, string entryNamespace = @"Inline", string entryClass = @"Program", string entryMethod = @"Main")
    {
      var syntaxTree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(codeAssembly);

      var referencePaths = new[] {
        typeof(System.Object).GetTypeInfo().Assembly.Location,
        typeof(System.Console).GetTypeInfo().Assembly.Location,
        System.IO.Path.Combine(System.IO.Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location) ?? throw new System.NullReferenceException($"{typeof(System.Runtime.GCSettings).FullName}.GetTypeInfo()"), "System.Runtime.dll"),
        typeof(Flux.Compiling.Helper).GetTypeInfo().Assembly.Location
      };

      var metadataReferences = referencePaths.Select(r => Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(r)).ToArray();

      var csharpCompilation = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create(System.IO.Path.GetRandomFileName(), new[] { syntaxTree }, metadataReferences, new Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary)) ?? throw new System.NullReferenceException($"Failed to create C# compilation.");

      using (var ms = new System.IO.MemoryStream())
      {
        var emitResult = csharpCompilation.Emit(ms);

        if (!emitResult.Success) throw new System.Exception(@"Failed to emit IL code.");

        ms.Seek(0, System.IO.SeekOrigin.Begin);

        var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromStream(ms);
        var type = assembly.GetType($"{entryNamespace}.{entryClass}") ?? throw new System.NullReferenceException($"Type not found. <{entryNamespace}.{entryClass}>.");
        var typeInstance = assembly.CreateInstance($"{entryNamespace}.{entryClass}") ?? throw new System.NullReferenceException($"Could not create instance of type. <{entryNamespace}.{entryClass}>");
        var methodInfo = (type.GetMember($"{entryMethod}").FirstOrDefault() as System.Reflection.MethodInfo) ?? throw new System.NullReferenceException($"Method not found. <{entryMethod}>");

        return methodInfo.Invoke(typeInstance, new object[] { codeArguments });
      }
    }
  }
}

#endif

/*
  // Console application sample code:

  string codeAssembly = @"
    namespace Inline
    {
        public class Program
        {
            public object Main( object[] args)
            {
                return string.Join('|', args);
            }
        }
    }
  ";

  var codeArguments = new object[] { "Hello", "World" };

  var codeResult = Flux.Compiling.Helper.ExecuteCsharp(codeAssembly, codeArguments);

  System.Console.WriteLine($"The compiled code returned: \"{codeResult}\"");
*/
