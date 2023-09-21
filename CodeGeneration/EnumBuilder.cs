using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace Code.MySubmodule.CodeGeneration
{
    public static class EnumBuilder
    {
        /// <summary>
        /// Builds enum from Dictionary(int, string).
        /// </summary>
        [PublicAPI]
        public static void Build(string enumName, string namespaceName, string path, Dictionary<int, string> enumDictionary)
        {
            using (var streamWriter = new StreamWriter($"{path}{enumName}.cs"))
            {
                streamWriter.WriteLine($"namespace {namespaceName}");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine($"\tpublic enum {enumName}");
                streamWriter.WriteLine("\t// DON'T EDIT MANUALLY!!! CREATED WITH CODE GENERATOR.");
                streamWriter.WriteLine("\t{");
                var lastPair = enumDictionary.Last();
                foreach (var keyValuePair in enumDictionary)
                {
                    var name = keyValuePair.Value.Replace(" ", "");
                    var lineEnd = keyValuePair.Key == lastPair.Key ? "" : ",";
                    var line = $"\t\t{name} = {keyValuePair.Key}{lineEnd}";
                    streamWriter.WriteLine(line);
                }
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
            }
        }
    }
}